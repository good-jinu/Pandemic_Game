using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CityRelated
{
    public class CitySelection : MonoBehaviour
    {
        public Text cityname;
        public Image citycolor;
        public Button move_btn;
        public Button treat_btn;
        public Button build_btn;
        public Button discover_btn;
        public GameObject selection_UI;
        public GameObject main_selection;
        public GameObject moveto_selection;
        public GameObject treat_selection;
        public GameObject discover_selection;
        public TreatDisease treat_disease;
        public ResearchStation research_station;
        public DiscoverCure discover_cure;
        public InteractionManager interaction;
        public CityRelated.CityManager citymanager;
        public Player.PlayerCardsManager player_card_manager;
        public Player.PlayerManager player_manager;

        private CityRelated.CityObject selected_city;
        private bool is_card_needed;
        private int card_ind;

        public CityRelated.CityObject Selected_city { get { return selected_city; } set { selected_city = value; } }

        private void OnEnable()
        {
            interaction.addWindow(1);
            main_selection.SetActive(true);
            moveto_selection.SetActive(false);
            treat_selection.SetActive(false);
            discover_selection.SetActive(false);
            cityname.text = selected_city.textarea.GetComponent<TextMeshPro>().text;

            switch(selected_city.city_color)
            {
                case DiseaseColor.Red:
                    citycolor.color = new Color(1.0f, 0, 0);
                    break;
                case DiseaseColor.Green:
                    citycolor.color = new Color(0, 1.0f, 0);
                    break;
                case DiseaseColor.Blue:
                    citycolor.color = new Color(0, 0, 1.0f);
                    break;
                case DiseaseColor.Yellow:
                    citycolor.color = new Color(1.0f, 1.0f, 0);
                    break;
            }
            availableBtns();
        }

        private void availableBtns()
        {
            //Move initialize
            move_btn.interactable = false;
            is_card_needed = false;
            if(selected_city.City_id!=player_manager.Current_city_id)
            {
                for(int i=0; i<player_card_manager.Cards_count; i++)
                {
                    if(player_card_manager.cards[i].City==selected_city)
                    {
                        move_btn.interactable = true;
                        is_card_needed = true;
                        card_ind = i;
                        break;
                    }
                }

                for(int i=0; i<citymanager.Citylist[player_manager.Current_city_id].neighbor_city.Length; i++)
                {
                    if(selected_city == citymanager.Citylist[player_manager.Current_city_id].neighbor_city[i])
                    {
                        move_btn.interactable = true;
                        is_card_needed = false;
                        break;
                    }
                }

                if(research_station.Isbuilt[player_manager.Current_city_id])
                {
                    if(research_station.Isbuilt[selected_city.City_id])
                    {
                        move_btn.interactable = true;
                        is_card_needed = false;
                    }    
                }
            }

            //Treat initialize
            //Building initialize
            treat_btn.interactable = false;
            build_btn.interactable = false;
            discover_btn.interactable = false;
            if (selected_city.City_id == player_manager.Current_city_id)
            {
                treat_btn.interactable = true;
                for(int i=0; i<player_card_manager.Cards_count; i++)
                {
                    if(selected_city==player_card_manager.cards[i].City)
                    {
                        if(research_station.isAvailable(selected_city.City_id))
                        {
                            build_btn.interactable = true;
                            card_ind = i;
                        }
                    }
                }

                if(!research_station.isAvailable(selected_city.City_id))
                {
                    discover_btn.interactable = true;
                }
            }
        }

        public void pressMoveToBtn()
        {
            if(is_card_needed)
            {
                main_selection.SetActive(false);
                moveto_selection.SetActive(true);
            }
            else
            {
                player_manager.setLocatedCity(selected_city.City_id);
                ActionManager.Instance.consumeAction();
                interaction.subWindow(1);
                selection_UI.SetActive(false);
            }
        }

        public void pressMoveToBtnUsingCard()
        {
            player_manager.setLocatedCity(selected_city.City_id);
            player_card_manager.removeCard(card_ind);
            ActionManager.Instance.consumeAction();
            interaction.subWindow(1);
            selection_UI.SetActive(false);
        }

        public void pressTreatBtn()
        {
            treat_disease.init(selected_city);
            main_selection.SetActive(false);
            treat_selection.SetActive(true);
        }

        public void pressBuildStationBtn()
        {
            player_card_manager.removeCard(card_ind);
            research_station.buildStation(Selected_city.City_id);
            ActionManager.Instance.consumeAction();
            interaction.subWindow(1);
            selection_UI.SetActive(false);
        }

        public void pressDiscoverBtn()
        {
            discover_cure.init();
            main_selection.SetActive(false);
            discover_selection.SetActive(true);
        }
    }
}
