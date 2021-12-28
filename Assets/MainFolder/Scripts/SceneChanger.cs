using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void changeScene(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    public void quitApp()
    {
        Application.Quit();
    }

    public void changeView(GameObject opening)
    {
        opening.SetActive(true);
    }

    public void closeView(GameObject closing)
    {
        closing.SetActive(false);
    }    
}
