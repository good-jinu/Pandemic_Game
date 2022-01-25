using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamRelated
{
    public class CamInitiator : MonoBehaviour
    {
        public Vector2 bottomleft;
        public Vector2 topright;
        public float mov_intensity;
        [Header("Zoom in and out level")]
        public float zoom_lvl1;
        public float zoom_lvl2;
        public float zoom_lvl3;
        [Header("Components to initiate")]
        public CamRelated.SwipeMovement swimov;
        public CamRelated.ZoomAction zooma;

        public void init()
        {
            zooma.Size_lvl1 = zoom_lvl1;
            zooma.Size_lvl2 = zoom_lvl2;
            zooma.Size_lvl3 = zoom_lvl3;
            zooma.initZoomLVL();
            swimov.setEndOfWorld(bottomleft, topright);
            swimov.Movement_Intensity = mov_intensity;
        }
    }
}