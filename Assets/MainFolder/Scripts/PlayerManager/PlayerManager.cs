using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public CityRelated.CityManager citymanager;
        public GameObject locator;

        private int current_city_id;

        public int Current_city_id { get { return current_city_id; } }

        public void setLocatedCity(int cityid)
        {
            current_city_id = cityid;
            locator.transform.position = new Vector3
                (citymanager.Citylist[current_city_id].transform.position.x+0.66f, citymanager.Citylist[current_city_id].transform.position.y + 1f, 40);
        }
    }
}
