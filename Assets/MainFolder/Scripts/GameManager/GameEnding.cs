using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public GameObject ending_ui;
    public GameObject ending_reason_text;

    public void gameEnds(int reason_text_index)
    {
        //0: You didn't end the pandemic in 25rounds
        //1: You let the outbreak level reach 8
        GameObject[] all_text = ending_reason_text.transform.GetComponentsInChildren<GameObject>();
        for (int i = 0; i < all_text.Length; i++)
        {
            all_text[i].SetActive(false);
        }

        all_text[reason_text_index].SetActive(true);

        ending_ui.SetActive(true);
    }
}
