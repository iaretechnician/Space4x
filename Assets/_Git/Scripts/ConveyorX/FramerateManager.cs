using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConveyorX.Misc
{
    public class FramerateManager : MonoBehaviour
    {
        [Range(10, 60)] public int maxFramerate = 30;

        private void Start()
        {
            Application.targetFrameRate = maxFramerate;
        }
    }
}