using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchStation : MonoBehaviour
{
    public CityRelated.CityManager citymanager;
    public int maxnum;

    private bool[] isbuilt;
    private int station_num;

    public bool[] Isbuilt { get { return isbuilt; } }

    public void init()
    {
        isbuilt = new bool[citymanager.Citylist.Length];
        station_num = 0;
    }

    public void buildStation(int ind)
    {
        if (station_num < maxnum)
        {
            isbuilt[ind] = true;
            citymanager.Citylist[ind].buildStation();
            station_num++;
        }
    }

    public bool isAvailable(int cityind)
    {
        if (station_num < maxnum)
            return !isbuilt[cityind];
        else
            return false;
    }
}
