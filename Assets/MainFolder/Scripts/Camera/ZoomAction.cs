using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamRelated
{
    public class ZoomAction : MonoBehaviour
    {
        public Camera maincam;
        public CamRelated.SwipeMovement swipemov;
        public GameObject cities;

        private float size_lvl1;//farthest
        private float size_lvl2;
        private float size_lvl3;//closest
        private int cur_size_lvl = 1;

        public float Size_lvl1 { get { return size_lvl1; } set { size_lvl1 = value; } }
        public float Size_lvl2 { get { return size_lvl2; } set { size_lvl2 = value; } }
        public float Size_lvl3 { get { return size_lvl3; } set { size_lvl3 = value; } }

        public void initZoomLVL()
        {
            float lvl_tmp = 1.0f;
            switch (cur_size_lvl)
            {
                case 1:
                    lvl_tmp = size_lvl1;
                    break;
                case 2:
                    lvl_tmp = size_lvl2;
                    break;
                case 3:
                    lvl_tmp = size_lvl3;
                    break;
                default:
                    lvl_tmp = 10.0f;
                    break;
            }

            if(cur_size_lvl>=2)
            {
                cities.SetActive(true);
            }
            else
            {
                cities.SetActive(false);
            }

            maincam.orthographicSize = lvl_tmp;
            swipemov.refreshEndOfWorld();
        }

        public void expandSize()
        {
            if (cur_size_lvl < 3)
                cur_size_lvl++;
            if (cur_size_lvl < 1)
                cur_size_lvl = 1;
            initZoomLVL();
        }

        public void reduceSize()
        {
            if (cur_size_lvl > 1)
                cur_size_lvl--;
            if (cur_size_lvl > 3)
                cur_size_lvl = 3;
            initZoomLVL();
        }
    }
}