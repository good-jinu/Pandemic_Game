using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityRelated
{
    public enum DiseaseColor { None=0, Red, Green, Blue, Yellow};
    public class CityObject : MonoBehaviour
    {
        public CityObject[] neighbor_city;
        public DiseaseColor city_color;
        public SpriteRenderer[] disease_cube_renderer = new SpriteRenderer[3];
        public GameObject textarea;

        private DiseaseColor[] disease_cubes = new DiseaseColor[3];
        
        public void addDisease(DiseaseColor newdisease)
        {
            bool inserted = false;

            for(int i=0; i<3; i++)
            {
                if(disease_cubes[i]==DiseaseColor.None)
                {
                    disease_cubes[i] = newdisease;
                    switch(newdisease)
                    {
                        case DiseaseColor.Red:
                            disease_cube_renderer[i].color = new Color(1, 0, 0);
                            break;
                        case DiseaseColor.Green:
                            disease_cube_renderer[i].color = new Color(0, 1, 0);
                            break;
                        case DiseaseColor.Blue:
                            disease_cube_renderer[i].color = new Color(0, 0, 1);
                            break;
                        case DiseaseColor.Yellow:
                            disease_cube_renderer[i].color = new Color(0.97f, 0.97f, 0.0f);
                            break;
                    }
                    inserted = true;
                    break;
                }
            }

            if(!inserted)
            {

            }
        }
    }
}
