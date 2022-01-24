using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerCardSelector : MonoBehaviour
    {
        public GameObject selector_screen;
        public GameObject card_selection;
        public GameObject discard_selection;
        public Text selected_num_text;
        public Text max_selection_num_text;
        public CardRelaed.CardObj[] cards = new CardRelaed.CardObj[4];
        public CardRelaed.CardObj[] discards = new CardRelaed.CardObj[12];
        [Header("Managers")]
        public CityRelated.CityManager citymanager;
        public Player.PlayerCardsManager player_cards_manager;
        public InteractionManager interaction_manager;

        private int selected_num;
        private int max_selected_num;

        private bool[] is_card_selected = new bool[4];
        private bool[] is_discard_selected = new bool[12];

        public void initiateSelector()
        {
            int tmpcityind;
            int[] indlist = new int[4];
            interaction_manager.addWindow(1);

            for(int i=0; i<4; i++)
            {
                tmpcityind = Random.Range(0, citymanager.Citylist.Length - i);
                for(int j = 0; j<i; j++)
                {
                    if (indlist[j] == tmpcityind)
                        tmpcityind++;
                }
                indlist[i] = tmpcityind;
                cards[i].setCardCity(citymanager.Citylist[tmpcityind]);
            }
            selected_num = 0;
            max_selected_num = 2;
            selected_num_text.text = selected_num.ToString();
            max_selection_num_text.text = "/ " + max_selected_num.ToString();

            for(int i=0; i<4; i++)
            {
                is_card_selected[i] = false;
            }

            selector_screen.SetActive(true);
            card_selection.SetActive(true);
            discard_selection.SetActive(false);
        }

        public void selectCard(int ind)
        {
            if(selected_num<max_selected_num)
            {
                if(!is_card_selected[ind])
                {
                    is_card_selected[ind] = true;
                    selected_num++;
                    cards[ind].color_of_city.color = new Color(1, 1, 1);
                }
                else
                {
                    is_card_selected[ind] = false;
                    selected_num--;
                    cards[ind].setCardCity(cards[ind].City);
                }
            }
            else
            {
                if(is_card_selected[ind])
                {
                    is_card_selected[ind] = false;
                    selected_num--;
                    cards[ind].setCardCity(cards[ind].City);
                }
            }
            selected_num_text.text = selected_num.ToString();
        }

        public void pushSelectBtn()
        {
            CityRelated.CityObject[] selected = new CityRelated.CityObject[2];
            if(selected_num==max_selected_num)
            {
                for(int i=0; i<4; i++)
                {
                    if(is_card_selected[i])
                    {
                        if (selected[0] == null)
                            selected[0] = cards[i].City;
                        else
                            selected[1] = cards[i].City;
                    }
                }

                if((player_cards_manager.getCardSpaceLeft()-selected_num)<0)
                {
                    initiateDiscard(selected[0], selected[1]);
                }
                else
                {
                    player_cards_manager.insertCard(selected[0]);
                    player_cards_manager.insertCard(selected[1]);
                    selector_screen.SetActive(false);
                    interaction_manager.subWindow(1);
                }
            }
        }

        private void initiateDiscard(CityRelated.CityObject selected1, CityRelated.CityObject selected2)
        {
            card_selection.SetActive(false);

            for (int  i=0; i<discards.Length; i++)
            {
                discards[i].removeCardCity();
            }

            for(int i=0; i<player_cards_manager.Cards_count; i++)
            {
                discards[i].setCardCity(player_cards_manager.cards[i].City);
            }
            discards[player_cards_manager.Cards_count].setCardCity(selected1);
            discards[player_cards_manager.Cards_count+1].setCardCity(selected2);

            selected_num = 0;
            max_selected_num = 2 - player_cards_manager.getCardSpaceLeft();
            selected_num_text.text = selected_num.ToString();
            max_selection_num_text.text = "/ " + max_selected_num.ToString();

            for (int i = 0; i < 12; i++)
            {
                is_discard_selected[i] = false;
            }

            selector_screen.SetActive(true);
            discard_selection.SetActive(true);
        }

        public void selectDiscard(int ind)
        {
            if(!is_discard_selected[ind])
            {
                if(selected_num<max_selected_num)
                {
                    is_discard_selected[ind] = true;
                    selected_num++;
                    discards[ind].color_of_city.color = new Color(1, 1, 1);
                }
            }
            else
            {
                is_discard_selected[ind] = false;
                selected_num--;
                discards[ind].setCardCity(discards[ind].City);
            }
        }

        public void pushDiscardBtn()
        {
            int[] ind = new int[max_selected_num];
            int[] toinsert = new int[max_selected_num];
            if(selected_num==max_selected_num)
            {
                for(int i=0; i<max_selected_num; i++)
                {
                    for(int j=0; j<12; j++)
                    {
                        if(is_discard_selected[j])
                        {
                            ind[i] = j;
                            is_discard_selected[j] = false;
                            break;
                        }
                    }

                    toinsert[i] = player_cards_manager.Cards_count + i;
                }

                for(int i=0; i<max_selected_num; i++)
                {
                    if(ind[i]<player_cards_manager.Cards_count)
                    {
                        player_cards_manager.removeCard(ind[i]);
                    }
                    else
                    {
                        toinsert[ind[i] - player_cards_manager.Cards_count] = -1;
                    }
                }

                for(int i=0; i<max_selected_num; i++)
                {
                    if(toinsert[i]>=0)
                    {
                        player_cards_manager.insertCard(discards[toinsert[i]].City);
                    }
                }

                selector_screen.SetActive(false);
                interaction_manager.subWindow(1);
            }
        }
    }
}