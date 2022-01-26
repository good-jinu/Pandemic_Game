﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreatDisease : MonoBehaviour
{
    public Button red_treat;
    public Button green_treat;
    public Button blue_treat;
    public Button yellow_treat;

    private CityRelated.CityObject city;

    public void init(CityRelated.CityObject cityp)
    {
        city = cityp;
        red_treat.interactable = false;
        green_treat.interactable = false;
        blue_treat.interactable = false;
        yellow_treat.interactable = false;

        if(city.isCubeOn(CityRelated.DiseaseColor.Red))
            red_treat.interactable = true;

        if (city.isCubeOn(CityRelated.DiseaseColor.Green))
            green_treat.interactable = true;

        if (city.isCubeOn(CityRelated.DiseaseColor.Blue))
            blue_treat.interactable = true;

        if (city.isCubeOn(CityRelated.DiseaseColor.Yellow))
            yellow_treat.interactable = true;
    }

    public void pressTreatButton(int dcolor)
    {
        bool cured = false;
        switch(dcolor)
        {
            case 1:
                cured = CityRelated.CityObject.Red_cure;
                break;
            case 2:
                cured = CityRelated.CityObject.Green_cure;
                break;
            case 3:
                cured = CityRelated.CityObject.Blue_cure;
                break;
            case 4:
                cured = CityRelated.CityObject.Yellow_cure;
                break;
        }

        if(cured)
        {
            city.treatDiseaseWithCure((CityRelated.DiseaseColor)dcolor);
        }
        else
        {
            city.treatDisease((CityRelated.DiseaseColor)dcolor);
        }
    }
}