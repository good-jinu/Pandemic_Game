using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionManager : MonoBehaviour
{
    public CityRelated.CityManager citymanager;
    public CamRelated.CamMotion cam_motion;
    public Text outbreak_level_marker;

    //if card is not used, it is false, or else true
    private List<int> discard_pile = new List<int>();
    private Stack<int> card_deck = new Stack<int>();
    private Stack<int> outbreak_stack = new Stack<int>();
    private int outbreak_level = 0;

    public void initiate()
    {
        bool[] card_tmp = new bool[citymanager.Citylist.Length];
        int tmp;
        discard_pile.Clear();
        card_deck.Clear();

        for(int i=citymanager.Citylist.Length; i>0; i--)
        {
            tmp = Random.Range(0, i);
            for(int j=0; j<=tmp; j++)
            {
                if (card_tmp[j])
                    tmp++;
            }
            card_tmp[tmp] = true;
            card_deck.Push(tmp);
        }

        outbreak_stack.Clear();
        outbreak_level_marker.text = outbreak_level.ToString()+"/8";
    }

    private bool outbreak(int cityind, CityRelated.DiseaseColor disease_color)
    {
        cam_motion.enqueueMotion(CamRelated.MotionKind.Outreak, citymanager.Citylist[cityind]);
        //It return false when game ends
        if ((++outbreak_level) >= 8)
        {
            cam_motion.enqueueMotion(CamRelated.MotionKind.End, 1);
            return false;
        }
        outbreak_level_marker.text = outbreak_level.ToString()+"/8";

        outbreak_stack.Push(cityind);

        bool dstate;
        for(int i=0; i < citymanager.Citylist[cityind].neighbor_city.Length; i++)
        {
            if(!outbreak_stack.Contains(citymanager.Citylist[cityind].neighbor_city[i].City_id))
            {
                outbreak_stack.Push(citymanager.Citylist[cityind].neighbor_city[i].City_id);

                dstate = citymanager.Citylist[citymanager.Citylist[cityind].neighbor_city[i].City_id].addDisease(disease_color);
                cam_motion.enqueueMotion(CamRelated.MotionKind.Infection, citymanager.Citylist[citymanager.Citylist[cityind].neighbor_city[i].City_id]);
                if(CityRelated.CityObject.isCubeOver())
                {
                    cam_motion.enqueueMotion(CamRelated.MotionKind.End, 2);
                }
                if (!dstate)
                {
                    if (!outbreak(citymanager.Citylist[cityind].neighbor_city[i].City_id, disease_color))
                        return false;
                }
            }
        }
        return true;
    }

    public void infectCity(int cubes)
    {
        int ind;
        CityRelated.DiseaseGneratingState dstate;
        if (card_deck.Count==0)
            initiate();

        ind = card_deck.Pop();
        discard_pile.Add(ind);

        dstate = citymanager.Citylist[ind].generateOriginDisease(cubes);
        if (dstate == CityRelated.DiseaseGneratingState.Cured)
            cam_motion.enqueueMotion(CamRelated.MotionKind.NotInfected, citymanager.Citylist[ind]);
        else
            cam_motion.enqueueMotion(CamRelated.MotionKind.Infection, citymanager.Citylist[ind]);

        if (CityRelated.CityObject.isCubeOver())
        {
            cam_motion.enqueueMotion(CamRelated.MotionKind.End, 2);
        }

        if (dstate == CityRelated.DiseaseGneratingState.Outbreak)
        {
            outbreak_stack.Clear();
            outbreak(ind, citymanager.Citylist[ind].city_color);
        }

        cam_motion.startQueueMotion();
    }

    public void epidemicOccure()
    {
        CityRelated.DiseaseGneratingState dstate;
        Stack<int> tmp = new Stack<int>();
        int discard_tmp;
        int ind;

        if (card_deck.Count == 0)
            initiate();

        for(int i=0; i<card_deck.Count; i++)
            tmp.Push(card_deck.Pop());

        ind = tmp.Pop();

        for (int i = 0; i < tmp.Count; i++)
            card_deck.Push(tmp.Pop());

        for(int i=discard_pile.Count; i>0; i--)
        {
            card_deck.Push(discard_pile[discard_tmp=Random.Range(0, i)]);
            discard_pile.RemoveAt(discard_tmp);
        }

        discard_pile.Add(ind);

        dstate = citymanager.Citylist[ind].generateOriginDisease(3);
        if (dstate == CityRelated.DiseaseGneratingState.Cured)
            cam_motion.enqueueMotion(CamRelated.MotionKind.NotInfected, citymanager.Citylist[ind]);
        else
        {
            cam_motion.enqueueMotion(CamRelated.MotionKind.Epidemic, citymanager.Citylist[ind]);
            cam_motion.enqueueMotion(CamRelated.MotionKind.Infection, citymanager.Citylist[ind]);
        }
        
        if (CityRelated.CityObject.isCubeOver())
        {
            cam_motion.enqueueMotion(CamRelated.MotionKind.End, 2);
        }

        if (dstate == CityRelated.DiseaseGneratingState.Outbreak)
        {
            outbreak_stack.Clear();
            outbreak(ind, citymanager.Citylist[ind].city_color);
        }

        cam_motion.startQueueMotion();
    }
}
