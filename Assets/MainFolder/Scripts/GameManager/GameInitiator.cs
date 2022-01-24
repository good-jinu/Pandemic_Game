using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    public CityRelated.CityManager citymanager;
    public InfectionManager infectionmanager;
    public Player.PlayerManager player;
    public Player.PlayerCardSelector player_cards_selector;

    private void Awake()
    {
        citymanager.gameObject.SetActive(true);
        citymanager.initiate();
        citymanager.gameObject.SetActive(false);
        infectionmanager.initiate();
        player.setLocatedCity(Random.Range(0, citymanager.Citylist.Length));
    }

    private void Start()
    {
        StartCoroutine(init());
    }

    IEnumerator init()
    {
        infectionmanager.infectCity(3);
        infectionmanager.infectCity(3);
        infectionmanager.infectCity(3);
        infectionmanager.infectCity(2);
        infectionmanager.infectCity(2);
        infectionmanager.infectCity(2);
        player_cards_selector.initiateSelector();
        yield return null;
    }
}
