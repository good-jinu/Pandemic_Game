using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionManager : MonoBehaviour
{
    public CityRelated.CityManager citymanager;
    public GameEnding game_ends;
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
        outbreak_level_marker.text = outbreak_level.ToString();
    }

    private bool outbreak(int cityind, CityRelated.DiseaseColor disease_color)
    {
        //It return false when game ends
        if ((++outbreak_level) >= 8)
        {
            game_ends.gameEnds(1);
            return false;
        }
        outbreak_level_marker.text = outbreak_level.ToString();

        outbreak_stack[cityind] = true;
        outbreak_stack_count++;
        for(int i=0; i < citymanager.Citylist[cityind].neighbor_city.Length; i++)
        {
            if(!outbreak_stack[i])
            {
                if (!citymanager.Citylist[i].addDisease(disease_color))
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
        if (infection_cards_count <= 0)
            initiate();

        if (topofdeck_count > 0)
        {
            int ind = Random.Range(0, topofdeck_count);
            while (ind < citymanager.Citylist.Length)
            {
                if (topofdeck[ind])
                {
                    infection_cards_used[ind] = true;
                    infection_cards_count--;
                    topofdeck[ind] = false;
                    topofdeck_count--;

                    if (citymanager.Citylist[ind].generateOriginDisease(cubes)==CityRelated.DiseaseGneratingState.Outbreak)
                    {
                        outbreak(ind, citymanager.Citylist[ind].city_color);
                    }
                    break;
                }
                ind++;
            }
        }
        else
        {
            int ind = Random.Range(0, infection_cards_count);
            while (ind < citymanager.Citylist.Length)
            {
                if (!infection_cards_used[ind])
                {
                    infection_cards_used[ind] = true;
                    infection_cards_count--;
                    topofdeck[ind] = false;
                    topofdeck_count--;

                    if (citymanager.Citylist[ind].generateOriginDisease(cubes) == CityRelated.DiseaseGneratingState.Outbreak)
                    {
                        outbreak(ind, citymanager.Citylist[ind].city_color);
                    }
                    break;
                }
                ind++;
            }
        }
    }
}
