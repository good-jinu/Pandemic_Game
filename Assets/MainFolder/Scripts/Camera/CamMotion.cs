using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

namespace CamRelated
{
    public enum MotionKind { Infection = 0, Outreak, NotInfected, Epidemic, End };

    public class CamMotion : MonoBehaviour
    {
        public GameObject motion_ui;
        public Text explain_situation;
        public ZoomAction zoom;
        public Transform camera_tr;
        public GameEnding game_ends;
        public InteractionManager interactionman;

        private Queue<CityRelated.CityObject> cityQ = new Queue<CityRelated.CityObject>();
        private Queue<MotionKind> motionQ = new Queue<MotionKind>();
        private Queue<int> numQ = new Queue<int>();

        private CamRelated.MotionIncluding endofmotion;

        public CamRelated.MotionIncluding Endofmotion { get { return endofmotion; } set { endofmotion = value; } }

        public void pressOK()
        {
            startQueueMotion();
        }

        private void startInfectionMotion(CityRelated.CityObject city)
        {
            explain_situation.text = city.textarea.GetComponent<TextMesh>().text + "\n"
                + LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Table", "is infected");
            camera_tr.position = new Vector3(city.transform.position.x, city.transform.position.y, camera_tr.position.z);
        }

        private void startOutbreaknMotion(CityRelated.CityObject city)
        {
            explain_situation.text = city.textarea.GetComponent<TextMesh>().text + "\n"
                + LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Table", "Outbreak");
            camera_tr.position = new Vector3(city.transform.position.x, city.transform.position.y, camera_tr.position.z);
        }

        private void startNotInfectedMotion(CityRelated.CityObject city)
        {
            explain_situation.text = city.textarea.GetComponent<TextMesh>().text + "\n"
                + LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Table", "Not infected");
            camera_tr.position = new Vector3(city.transform.position.x, city.transform.position.y, camera_tr.position.z);
        }
        private void startEpidemicMotion(CityRelated.CityObject city)
        {
            explain_situation.text = LocalizationSettings.StringDatabase.GetLocalizedString("InGame_Table", "Epidemic card");
            camera_tr.position = new Vector3(city.transform.position.x, city.transform.position.y, camera_tr.position.z);
        }

        private void startGameEndMotion(int ind)
        {
            game_ends.gameEnds(ind);
        }

        public void startQueueMotion()
        {
            interactionman.setActiveAllInteraction(false);
            if(motionQ.Count>0)
            {
                motion_ui.SetActive(true);
                zoom.setSizeLVL(3);
                switch(motionQ.Dequeue())
                {
                    case MotionKind.Infection:
                        startInfectionMotion(cityQ.Dequeue());
                        break;
                    case MotionKind.Outreak:
                        startOutbreaknMotion(cityQ.Dequeue());
                        break;
                    case MotionKind.NotInfected:
                        startNotInfectedMotion(cityQ.Dequeue());
                        break;
                    case MotionKind.Epidemic:
                        startEpidemicMotion(cityQ.Dequeue());
                        break;
                    case MotionKind.End:
                        startGameEndMotion(numQ.Dequeue());
                        break;
                }
            }
            else
            {
                motion_ui.SetActive(false);
                interactionman.setActiveAllInteraction(true);
                endofmotion.EndOfMotion();
            }
        }

        public void enqueueMotion(MotionKind mk, CityRelated.CityObject city)
        {
            switch (mk)
            {
                case MotionKind.Infection:
                case MotionKind.Outreak:
                case MotionKind.NotInfected:
                case MotionKind.Epidemic:
                    motionQ.Enqueue(mk);
                    cityQ.Enqueue(city);
                    break;
                default:
                    Debug.LogError("Wrong MotionKind");
                    break;
            }
        }

        public void enqueueMotion(MotionKind mk, int ind)
        {
            switch(mk)
            {
                case MotionKind.End:
                    motionQ.Enqueue(mk);
                    numQ.Enqueue(ind);
                    break;
                default:
                    Debug.LogError("Wrong MotionKind");
                    break;
            }
        }
    }
}