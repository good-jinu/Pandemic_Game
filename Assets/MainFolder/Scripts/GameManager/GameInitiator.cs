using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiator : CamRelated.MotionIncluding
{
    public CityRelated.CityManager citymanager;
    public InfectionManager infectionmanager;
    public Player.PlayerManager player;
    public Player.PlayerCardSelector player_cards_selector;
    public CamRelated.CamInitiator cam_init;
    public CamRelated.CamMotion cam_motion;

    private int init_ind;

    private void Awake()
    {
        citymanager.initiate();
        infectionmanager.initiate();
        player.setLocatedCity(Random.Range(0, citymanager.Citylist.Length));
    }

    private void Start()
    {
        StartCoroutine(init());
    }

    IEnumerator init()
    {
        yield return new WaitForSeconds(1f);
        cam_init.init();

        init_ind = 0;
        cam_motion.Endofmotion = this;
        infectionmanager.infectCity(3);
    }

    public override void EndOfMotion()
    {
        init_ind++;

        if(init_ind<3)
        {
            infectionmanager.infectCity(3);
        }
        else if(init_ind<6)
        {
            infectionmanager.infectCity(2);
        }
        else if(init_ind==6)
        {
            player_cards_selector.initiateSelector();
        }
    }
}
