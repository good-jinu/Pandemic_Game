using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoverCure : MonoBehaviour
{
    public Button redb;
    public Button greenb;
    public Button blueb;
    public Button yellowb;
    public Player.PlayerCardsManager player_card_manager;

    public void init()
    {
        redb.interactable = false;
        greenb.interactable = false;
        blueb.interactable = false;
        yellowb.interactable = false;

        for(int i=1; i<=4; i++)
        {
            int count = 0;
            for(int j = 0; j<player_card_manager.Cards_count; j++)
            {
                if ((CityRelated.DiseaseColor)i == player_card_manager.cards[j].City.city_color)
                    count++;
            }
            if(count>=5)
            {
                switch(i)
                {
                    case 1:
                        redb.interactable = true;
                        break;
                    case 2:
                        greenb.interactable = true;
                        break;
                    case 3:
                        blueb.interactable = true;
                        break;
                    case 4:
                        yellowb.interactable = true;
                        break;
                }
            }
        }

        if (CityRelated.CityObject.Red_cure)
            redb.interactable = false;
        if (CityRelated.CityObject.Green_cure)
            greenb.interactable = false;
        if (CityRelated.CityObject.Blue_cure)
            blueb.interactable = false;
        if (CityRelated.CityObject.Yellow_cure)
            yellowb.interactable = false;
    }

    public void pressCureButton(int dind)
    {
        switch(dind)
        {
            case 1:
                CityRelated.CityObject.Red_cure = true;
                break;
            case 2:
                CityRelated.CityObject.Green_cure = true;
                break;
            case 3:
                CityRelated.CityObject.Blue_cure = true;
                break;
            case 4:
                CityRelated.CityObject.Yellow_cure = true;
                break;
        }
    }
}
