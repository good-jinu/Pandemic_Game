using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : CamRelated.MotionIncluding
{
    public InfectionManager infectionmanager;
    public Player.PlayerCardSelector card_selector;
    public CamRelated.CamMotion cam_motion;
    public Text action_text;
    public Text round_text;
    public Text infection_rate_text;
    [Header("Game Option")]
    public int max_action;
    public int max_round;

    private int action_num;
    private int round_num;
    private int[] infection_rate;
    private int infection_lvl;
    private bool[] epidemic_round;
    private int motion_ind;
    private static ActionManager instance = null;

    public static ActionManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    public void init()
    {
        action_num = 0;
        round_num = 1;
        infection_rate = new int[6] { 2, 2, 2, 3, 3, 4 };
        infection_lvl = 0;
        epidemic_round = new bool[max_round];

        for(int i=0; i<5; i++)
        {
            epidemic_round[i * (max_round / 5) + Random.Range(0, max_round / 5)] = true;
        }

        showOnUI();
    }

    public void showOnUI()
    {
        action_text.text = action_num.ToString() + "/" + max_action.ToString();
        round_text.text = round_num.ToString() + "/" + max_round.ToString();
        infection_rate_text.text = infection_rate[infection_lvl].ToString();
    }

    public void consumeAction()
    {
        action_num++;
        action_text.text = action_num.ToString() + "/" + max_action.ToString();
        if (action_num >= max_action)
            endTheRound();
    }

    public void endTheRound()
    {
        round_num++;
        action_num = 0;
        cam_motion.Endofmotion = this;
        if(round_num>max_round)
        {
            cam_motion.enqueueMotion(CamRelated.MotionKind.End, 0);
            cam_motion.startQueueMotion();
        }
        else
        {
            motion_ind = 0;
            card_selector.Endofm = this;
            card_selector.initiateSelector();
        }
    }

    public override void EndOfMotion()
    {
        if(epidemic_round[round_num-2])
        {
            epidemic_round[round_num - 2] = false;
            infection_lvl++;
            infectionmanager.epidemicOccure();
        }
        else
        {
            if(motion_ind<infection_rate[infection_lvl])
            {
                infectionmanager.infectCity(1);
                motion_ind++;
            }
            else
            {
                showOnUI();
            }
        }
    }
}
