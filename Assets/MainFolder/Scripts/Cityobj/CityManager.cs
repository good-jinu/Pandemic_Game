using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CityRelated
{
    public class CityManager : MonoBehaviour
    {
        private CityObject[] citylist = null;

        public CityObject[] Citylist { get { return citylist; } }

        public void initiate()
        {
            citylist = transform.GetComponentsInChildren<CityObject>();
            for(int i=0; i<citylist.Length; i++)
            {
                citylist[i].City_id = i;
            }
        }
    }
}