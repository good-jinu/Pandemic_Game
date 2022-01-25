using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public CamRelated.SwipeMovement swipe_obj;
    public CityRelated.CitySelection selected_city_ui;
    public GameObject basic_UI;

    private Vector3 touchposworld;
    private bool interaction_activate;
    private CityRelated.CityObject touchedobj;
    private int window_count = 0;
    private bool set_swipe;

    private void Awake()
    {
        interaction_activate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction_activate)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                touchposworld = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Vector2 touchposworld2 = new Vector2(touchposworld.x, touchposworld.y);

                RaycastHit2D hit = Physics2D.Raycast(touchposworld2, Vector2.zero);

                if(hit.collider != null)
                {
                    touchedobj = hit.collider.GetComponent<CityRelated.CityObject>();
                    selected_city_ui.Selected_city = touchedobj;
                    selected_city_ui.gameObject.SetActive(true);
                    addWindow(1);
                }
            }
#else
            if (Input.GetMouseButtonUp(0))
            {
                touchposworld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchposworld2 = new Vector2(touchposworld.x, touchposworld.y);

                RaycastHit2D hit = Physics2D.Raycast(touchposworld2, Vector2.zero);

                if (hit.collider != null)
                {
                    touchedobj = hit.collider.GetComponent<CityRelated.CityObject>();
                    selected_city_ui.Selected_city = touchedobj;
                    selected_city_ui.gameObject.SetActive(true);
                    addWindow(1);
                }
            }
#endif
        }
    }

    public void setActiveAllInteraction(bool value)
    {
        swipe_obj.setActiveSwipe(value);
        basic_UI.SetActive(value);
        interaction_activate = value;
    }

    public void setSwipe(bool value)
    {
        swipe_obj.setActiveSwipe(value);
    }

    public void setInteractionByWindow()
    {
        if (window_count > 0)
        {
            setActiveAllInteraction(false);
            if(set_swipe)
            {
                setSwipe(true);
                set_swipe = false;
            }
        }
        else
        {
            setActiveAllInteraction(true);
        }
    }

    public void addWindow(int count)
    {
        window_count += count;
        setInteractionByWindow();
    }

    public void subWindow(int count)
    {
        window_count -= count;
        setInteractionByWindow();
    }

    public void addWindowStayingSwipe()
    {
        window_count++;
        setInteractionByWindow();
        set_swipe = true;
    }
}
