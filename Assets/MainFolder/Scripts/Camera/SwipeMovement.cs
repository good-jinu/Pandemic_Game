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
        private Vector2 endofworld_leftbottom = new Vector2(-38.4f, -38.5f);
        private Vector2 endofworld_topright = new Vector2(38.4f, 38.5f);

        public float movement_intensity = 0.125f;
        public Transform objpos;

        private void Start()
        {
            Vector3 edgepoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector3 middlepoint = Camera.main.ScreenToWorldPoint(new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0));

            endofworld_leftbottom = endofworld_leftbottom + (Vector2)(edgepoint - middlepoint);
            endofworld_topright = endofworld_topright - (Vector2)(edgepoint - middlepoint);
            if(endofworld_leftbottom.x>endofworld_topright.x)
            {
                endofworld_leftbottom.x = 0;
                endofworld_topright.x = 0;
            }
            if(endofworld_leftbottom.y>endofworld_topright.y)
            {
                endofworld_leftbottom.y = 0;
                endofworld_topright.y = 0;
            }
        }

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

        IEnumerator moveCam()
        {
            Vector3 movedvector3 = movement_intensity * movedvector;
            Vector3 scbottomleft = Camera.main.WorldToScreenPoint((Vector3)endofworld_leftbottom);
            Vector3 sctopright = Camera.main.WorldToScreenPoint((Vector3)endofworld_topright);
            Vector3 tmp = objpos.position - movedvector3;

            if(tmp.x <endofworld_leftbottom.x)
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