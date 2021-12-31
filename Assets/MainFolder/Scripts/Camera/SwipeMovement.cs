using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamRelated
{
    public class SwipeMovement : MonoBehaviour
    {
        private bool is_swipe_activating = true;
        private Vector2 prevpos;
        private Vector2 movedvector;
        private bool fingerdown = false;

        //need to get set
        private Vector2 endofworld_leftbottom;
        private Vector2 endofworld_topright;
        private Vector2 endofimage_bottomleft;
        private Vector2 endofimage_topright;
        private float movement_intensity;
        private int zoomlvl = 1;

        public Transform objpos;

        public float Movement_Intensity { get { return movement_intensity; } set { movement_intensity = value; } }

        // Update is called once per frame
        void Update()
        {
            if(is_swipe_activating)
            {
#if UNITY_ANDROID || UNITY_IOS
                if(fingerdown==false&&Input.touchCount>0&&Input.touches[0].phase==TouchPhase.Began)
                {
                    prevpos = Input.touches[0].position;
                    fingerdown = true;
                }
                if(fingerdown&&Input.touches[0].phase==TouchPhase.Moved)
                {
                    movedvector = Input.touches[0].position - prevpos;
                    StartCoroutine(moveCam());
                    prevpos = Input.touches[0].position;

                }
                if(fingerdown&&Input.touches[0].phase == TouchPhase.Ended)
                {
                    fingerdown = false;
                }
#else
                if(fingerdown==false&&Input.GetMouseButtonDown(0))
                {
                    prevpos = Input.mousePosition;
                    fingerdown = true;
                }
                if(fingerdown&&((Vector2)Input.mousePosition!=prevpos))
                {
                    movedvector = (Vector2)Input.mousePosition - prevpos;
                    StartCoroutine(moveCam());
                    prevpos = Input.mousePosition;
                }
                if(fingerdown&&Input.GetMouseButtonUp(0))
                {
                    fingerdown = false;
                }
#endif
            }
        }

        public void increaseZoomLVL()
        {
            if (zoomlvl < 3)
                zoomlvl++;
            if (zoomlvl < 1)
                zoomlvl = 1;
        }

        public void decreaseZoomLVL()
        {
            if (zoomlvl > 1)
                zoomlvl--;
            if (zoomlvl > 3)
                zoomlvl = 3;
        }

        public void setEndOfWorld(Vector2 bottom_left, Vector2 Top_right)
        {
            Vector3 edgepoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 middlepoint = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0));

            endofimage_bottomleft = bottom_left;
            endofimage_topright = Top_right;

            endofworld_leftbottom = bottom_left + (Vector2)(edgepoint - middlepoint);
            endofworld_topright = Top_right - (Vector2)(edgepoint - middlepoint);
            if (endofworld_leftbottom.x > endofworld_topright.x)
            {
                endofworld_leftbottom.x = 0;
                endofworld_topright.x = 0;
            }
            if (endofworld_leftbottom.y > endofworld_topright.y)
            {
                endofworld_leftbottom.y = 0;
                endofworld_topright.y = 0;
            }
        }

        public void refreshEndOfWorld()
        {
            Vector3 edgepoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 middlepoint = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0));

            endofworld_leftbottom = endofimage_bottomleft + (Vector2)(edgepoint - middlepoint);
            endofworld_topright = endofimage_topright - (Vector2)(edgepoint - middlepoint);
            if (endofworld_leftbottom.x > endofworld_topright.x)
            {
                endofworld_leftbottom.x = 0;
                endofworld_topright.x = 0;
            }
            if (endofworld_leftbottom.y > endofworld_topright.y)
            {
                endofworld_leftbottom.y = 0;
                endofworld_topright.y = 0;
            }
        }

        IEnumerator moveCam()
        {
            Vector3 movedvector3 = movement_intensity * movedvector;
            Vector3 scbottomleft = Camera.main.WorldToScreenPoint((Vector3)endofworld_leftbottom);
            Vector3 sctopright = Camera.main.WorldToScreenPoint((Vector3)endofworld_topright);
            for( int i=1; i<zoomlvl; i++)
            {
                movedvector3 = movedvector3 / 2;
            }
            Vector3 tmp = objpos.position - movedvector3;

            if (tmp.x <endofworld_leftbottom.x)
            {
                tmp.x = endofworld_leftbottom.x;
            }
            if(tmp.y<endofworld_leftbottom.y)
            {
                tmp.y = endofworld_leftbottom.y;
            }
            if(tmp.x > endofworld_topright.x)
            {
                tmp.x = endofworld_topright.x;
            }
            if(tmp.y > endofworld_topright.y)
            {
                tmp.y = endofworld_topright.y;
            }

            objpos.position = tmp;

            yield return null;
        }
    }
}