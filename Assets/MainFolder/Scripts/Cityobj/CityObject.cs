using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityRelated
{
    public enum DiseaseColor { None=0, Red, Green, Blue, Yellow};
    public enum DiseaseGneratingState { Succeed = 0, Cured, Outbreak};

    public class CityObject : MonoBehaviour
    {
        public CityObject[] neighbor_city;
        public DiseaseColor city_color;
        public SpriteRenderer[] disease_cube_renderer = new SpriteRenderer[3];
        public GameObject textarea;
        public GameObject station_area;

        private DiseaseColor[] disease_cubes = new DiseaseColor[3];
        private int city_id;
        private static bool red_cure = false;
        private static bool green_cure = false;
        private static bool blue_cure = false;
        private static bool yellow_cure = false;
        
        public DiseaseColor[] Disease_cubes { get { return disease_cubes; } }
        public int City_id { get { return city_id; } set { city_id = value; } }
        public static bool Red_cure { get { return red_cure; } set { red_cure = value; } }
        public static bool Green_cure { get { return green_cure; } set { green_cure = value; } }
        public static bool Blue_cure { get { return blue_cure; } set { blue_cure = value; } }
        public static bool Yellow_cure { get { return yellow_cure; } set { yellow_cure = value; } }

        private void Awake()
        {
            for(int i=0; i<3;i++)
            {
                disease_cubes[i] = DiseaseColor.None;
                disease_cube_renderer[i].color = new Color(0, 0, 0);
            }
        }

        //If it succeeds to insert cube, return true. if not, return false
        public bool addDisease(DiseaseColor newdisease)
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

            return inserted;
        }

        public DiseaseGneratingState generateOriginDisease(int count)
        {
            switch(city_color)
            {
                case DiseaseColor.Red:
                    if (Red_cure)
                        return DiseaseGneratingState.Cured;
                    break;
                case DiseaseColor.Green:
                    if (Green_cure)
                        return DiseaseGneratingState.Cured;
                    break;
                case DiseaseColor.Blue:
                    if (Blue_cure)
                        return DiseaseGneratingState.Cured;
                    break;
                case DiseaseColor.Yellow:
                    if (Yellow_cure)
                        return DiseaseGneratingState.Cured;
                    break;
            }

            for(int i=0; i<count; i++)
            {
                if(!addDisease(city_color))
                {
                    return DiseaseGneratingState.Outbreak;
                }
            }

            return DiseaseGneratingState.Succeed;
        }

        //return true if it succeeds to treat disease. otherwise return false
        public bool treatDisease(DiseaseColor diseasetotreat)
        {
            bool treated = false;

            for(int i=0; i<3; i++)
            {
                if(disease_cubes[i]==diseasetotreat)
                {
                    disease_cubes[i] = DiseaseColor.None;
                    disease_cube_renderer[i].color = new Color(0, 0, 0);
                    treated = true;
                    break;
                }
            }

            return treated;
        }

        public bool treatDiseaseWithCure(DiseaseColor diseasetocure)
        {
            bool cured = false;

            for(int i=0; i<3; i++)
            {
                if (disease_cubes[i] == diseasetocure)
                {
                    disease_cubes[i] = DiseaseColor.None;
                    disease_cube_renderer[i].color = new Color(0, 0, 0);
                    cured = true;
                    break;
                }
            }

            return cured;
        }

        public bool isCubeOn(DiseaseColor dcolor)
        {
            for(int i=0; i<3; i++)
            {
                if (disease_cubes[i] == dcolor)
                    return true;
            }
            return false;
        }

        public bool buildStation()
        {
            if (station_area.activeSelf == false)
            {
                station_area.SetActive(true);
                return true;
            }

            return false;
        }
    }
}
