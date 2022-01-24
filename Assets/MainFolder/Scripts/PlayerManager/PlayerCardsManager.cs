using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCardsManager : MonoBehaviour
    {
        public CardRelaed.CardObj[] cards = new CardRelaed.CardObj[10];

        private CityRelated.CityObject[] cities = new CityRelated.CityObject[10];
        private int cards_count = 0;

        public int Cards_count { get { return cards_count; } }

        private void Awake()
        {
            sortCards();
        }

        public void sortCards()
        {
            cards_count = 0;
            for(int i=cities.Length-1; i>0; i--)
            {
                if(cities[i]!=null)
                {
                    for(int j=0; j<i; j++)
                    {
                        if(cities[j]==null)
                        {
                            cities[j] = cities[i];
                            cities[i] = null;
                            break;
                        }
                    }
                }
            }

            for(int i=0; i<cards.Length; i++)
            {
                if (cities[i] == null)
                {
                    cards[i].removeCardCity();
                }
                else
                {
                    cards_count++;
                    cards[i].setCardCity(cities[i]);
                }
            }
        }

        //if it's full, it returns false, or else returns true
        public bool insertCard(CityRelated.CityObject inserted)
        {
            if (cards_count >= 8)
                return false;

            cities[cards_count] = inserted;
            cards_count++;
            sortCards();
            return true;
        }

        //if it's empty, it returns false, or else returns true
        public bool removeCard(int ind)
        {
            if (cities[ind] == null)
                return false;

            cities[ind] = null;
            sortCards();
            return true;
        }

        public int getCardSpaceLeft()
        {
            return 10 - cards_count;
        }
    }
}