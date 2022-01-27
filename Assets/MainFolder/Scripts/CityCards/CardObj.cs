using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CardRelaed
{
    public class CardObj : MonoBehaviour
    {
        public Image color_of_city;
        public Text cityname;

        private CityRelated.CityObject city;

        public CityRelated.CityObject City { get { return city; } }

        public void setCardCity(CityRelated.CityObject cityset)
        {
            city = cityset;
            switch(city.city_color)
            {
                case CityRelated.DiseaseColor.Red:
                    color_of_city.color = new Color(1.0f, 0, 0);
                    break;
                case CityRelated.DiseaseColor.Green:
                    color_of_city.color = new Color(0, 1.0f, 0);
                    break;
                case CityRelated.DiseaseColor.Blue:
                    color_of_city.color = new Color(0, 0, 1.0f);
                    break;
                case CityRelated.DiseaseColor.Yellow:
                    color_of_city.color = new Color(1.0f, 1.0f, 0);
                    break;
            }
            cityname.text = city.textarea.GetComponent<TextMeshPro>().text;
        }

        public void removeCardCity()
        {
            city = null;
            color_of_city.color = new Color(0.25f, 0.25f, 0.25f, 0.25f);
            cityname.text = "";
        }
    }
}
