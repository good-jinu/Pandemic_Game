using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerCardForCureSelection : MonoBehaviour
    {
        public PlayerCardsManager playercardsmanager;
        public GameEnding game_ends;
        public CureManager curemanager;
        public GameObject selection_UI;
        public Text selected_num_text;
        public Button[] card_btn = new Button[10];

        private int[] card_ind = new int[10];
        private bool[] card_selected = new bool[10];
        private int[] selected_option = new int[5];
        private int selected_num;
        private const int max_select = 5;
        private Color btncolor;
        private CityRelated.DiseaseColor cure_color;

        public void init(CityRelated.DiseaseColor dcolor)
        {
            cure_color = dcolor;
            int ind = 0;
            btncolor = new Color(0, 0, 0, 0.75f);
            selected_num = 0;
            for(int i=0; i<10; i++)
            {
                card_btn[i].interactable = false;
                card_selected[i] = false;
                card_btn[i].GetComponent<Image>().color = btncolor;
            }

            switch(dcolor)
            {
                case CityRelated.DiseaseColor.Red:
                    btncolor = new Color(1, 0, 0, 1);
                    break;
                case CityRelated.DiseaseColor.Green:
                    btncolor = new Color(0, 1, 0, 1);
                    break;
                case CityRelated.DiseaseColor.Blue:
                    btncolor = new Color(0, 0, 1, 1);
                    break;
                case CityRelated.DiseaseColor.Yellow:
                    btncolor = new Color(1, 1, 0, 1);
                    break;
            }

            for(int i=0; i<playercardsmanager.Cards_count; i++)
            {
                if(playercardsmanager.cards[i].City.city_color == dcolor)
                {
                    card_ind[ind] = i;
                    card_btn[ind].interactable = true;
                    card_btn[ind].GetComponentInChildren<Text>().text = playercardsmanager.cards[i].City.textarea.GetComponent<TextMesh>().text;
                    card_btn[ind].GetComponent<Image>().color = btncolor;
                    ind++;
                }
            }

            selected_num_text.text = selected_num.ToString();
            selection_UI.SetActive(true);
        }

        public void pressCard(int ind)
        {
            if(selected_num<max_select)
            {
                selected_option[selected_num++] = ind;
                card_btn[ind].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                selected_num--;
                card_btn[ind].GetComponent<Image>().color = btncolor;
            }
        }

        public void pressCure()
        {
            if(selected_num==max_select)
            {
                for(int i=0; i<selected_num; i++)
                {
                    playercardsmanager.removeCard(card_ind[selected_option[i]]);
                }

                switch (cure_color)
                {
                    case CityRelated.DiseaseColor.Red:
                        CityRelated.CityObject.Red_cure = true;
                        break;
                    case CityRelated.DiseaseColor.Green:
                        CityRelated.CityObject.Green_cure = true;
                        break;
                    case CityRelated.DiseaseColor.Blue:
                        CityRelated.CityObject.Blue_cure = true;
                        break;
                    case CityRelated.DiseaseColor.Yellow:
                        CityRelated.CityObject.Yellow_cure = true;
                        break;
                }
                curemanager.init();
                selection_UI.SetActive(false);

                if (CityRelated.CityObject.isAllCured())
                    game_ends.gameVictory();
                else
                    ActionManager.Instance.consumeAction();
            }
        }
    }
}