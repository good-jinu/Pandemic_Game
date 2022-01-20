using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityRelated
{
    public class CitySelection : MonoBehaviour
    {
        public Text cityname;
        public Image citycolor;
        public Button move_btn;
        public Button treat_btn;
        public Button build_btn;

        private CityRelated.CityObject selected_city;

        public CityRelated.CityObject Selected_city { get { return selected_city; } set { selected_city = value; } }

        private void OnEnable()
        {
            cityname.text = selected_city.textarea.GetComponent<TextMesh>().text;

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
        }
    }
}
