using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CureManager : MonoBehaviour
{
    public Image redcure;
    public Image greencure;
    public Image bluecure;
    public Image yellowcure;
    public Player.PlayerCardForCureSelection cardforcure;

    public void init()
    {
        if (CityRelated.CityObject.Red_cure)
            redcure.color = new Color(1, 0, 0, 1);
        else
            redcure.color = new Color(1, 0, 0, 0.25f);

        if (CityRelated.CityObject.Green_cure)
            greencure.color = new Color(0, 1, 0, 1);
        else
            greencure.color = new Color(0, 1, 0, 0.25f);

        if (CityRelated.CityObject.Blue_cure)
            bluecure.color = new Color(0, 0, 1, 1);
        else
            bluecure.color = new Color(0, 0, 1, 0.25f);

        if (CityRelated.CityObject.Yellow_cure)
            yellowcure.color = new Color(1, 1, 0, 1);
        else
            yellowcure.color = new Color(1, 1, 0, 0.25f);
    }

    public void cureDisease(CityRelated.DiseaseColor dcolor)
    {
        cardforcure.init(dcolor);
    }
}
