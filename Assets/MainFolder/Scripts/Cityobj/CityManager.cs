using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CityRelated
{
    public class CityManager : MonoBehaviour
    {
        private CityObject[] citylist;

        private void Awake()
        {
            citylist = transform.GetComponentsInChildren<CityObject>();
        }

    }
}