using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class GameEnding : MonoBehaviour
{
    public GameObject ending_ui;
    public Text ending_reason_text;
    public GameObject victory_UI;

    public void gameEnds(int reason_text_index)
    {
        //0: You didn't end the pandemic in a game
        //1: You didn't stop the outbreak level reaching 8
        //2: You didn't stop the one of disease gettig over 20
        switch(reason_text_index)
        {
            case 0:
                ending_reason_text.text = LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Over", "0");
                break;
            case 1:
                ending_reason_text.text = LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Over", "1");
                break;
            case 2:
                ending_reason_text.text = LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Over", "2");
                break;
        }

        ending_ui.SetActive(true);
    }

    public void gameVictory()
    {
        victory_UI.SetActive(true);
    }
}
