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
    private bool[] infection_cards_used;
    private int infection_cards_count;
    private bool[] topofdeck;
    private int topofdeck_count;
    private bool[] outbreak_stack;
    private int outbreak_stack_count;
    private int outbreak_level = 0;

    public void initiate()
    {
        infection_cards_count = citymanager.Citylist.Length;
        infection_cards_used = new bool[infection_cards_count];
        topofdeck = new bool[infection_cards_count];
        topofdeck_count = 0;
        outbreak_stack = new bool[infection_cards_count];
        outbreak_stack_count = 0;
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

        outbreak_stack[cityind] = true;
        outbreak_stack_count++;

        bool dstate;
        for(int i=0; i < citymanager.Citylist[cityind].neighbor_city.Length; i++)
        {
            if(!outbreak_stack[i])
            {
                dstate = citymanager.Citylist[i].addDisease(disease_color);
                cam_motion.enqueueMotion(CamRelated.MotionKind.Infection, citymanager.Citylist[i]);
                if (!dstate)
                {
                    if (!outbreak(i, disease_color))
                        return false;
                }
                else
                {
                    outbreak_stack[i] = true;
                    outbreak_stack_count++;
                }
            }
        }
        return true;
    }

    public void infectCity(int cubes)
    {
        int ind;
        CityRelated.DiseaseGneratingState dstate;
        if (infection_cards_count <= 0)
            initiate();

        if (topofdeck_count > 0)
        {
            ind = Random.Range(0, topofdeck_count);
            while (ind < citymanager.Citylist.Length)
            {
                if (topofdeck[ind])
                {
                    infection_cards_used[ind] = true;
                    infection_cards_count--;
                    topofdeck[ind] = false;
                    topofdeck_count--;

                    dstate = citymanager.Citylist[ind].generateOriginDisease(cubes);
                    if (dstate == CityRelated.DiseaseGneratingState.Cured)
                        cam_motion.enqueueMotion(CamRelated.MotionKind.NotInfected, citymanager.Citylist[ind]);
                    else
                        cam_motion.enqueueMotion(CamRelated.MotionKind.Infection, citymanager.Citylist[ind]);

                    if (dstate==CityRelated.DiseaseGneratingState.Outbreak)
                    {
                        outbreak_stack = new bool[infection_cards_count];
                        outbreak_stack_count = 0;
                        outbreak(ind, citymanager.Citylist[ind].city_color);
                    }
                    break;
                }
                ind++;
            }
        }
        else
        {
            ind = Random.Range(0, infection_cards_count);
            while (ind < citymanager.Citylist.Length)
            {
                if (!infection_cards_used[ind])
                {
                    infection_cards_used[ind] = true;
                    infection_cards_count--;
                    topofdeck[ind] = false;
                    topofdeck_count--;

                    dstate = citymanager.Citylist[ind].generateOriginDisease(cubes);
                    if (dstate == CityRelated.DiseaseGneratingState.Cured)
                        cam_motion.enqueueMotion(CamRelated.MotionKind.NotInfected, citymanager.Citylist[ind]);
                    else
                        cam_motion.enqueueMotion(CamRelated.MotionKind.Infection, citymanager.Citylist[ind]);

                    if (dstate == CityRelated.DiseaseGneratingState.Outbreak)
                    {
                        outbreak_stack = new bool[infection_cards_count];
                        outbreak_stack_count = 0;
                        outbreak(ind, citymanager.Citylist[ind].city_color);
                    }
                    break;
                }
                ind++;
            }
        }

        cam_motion.startQueueMotion();
    }
}
