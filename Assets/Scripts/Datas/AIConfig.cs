using System;
using UnityEngine;

namespace Datas
{
    [Serializable]
    public class AIConfig
    {
        public float MinSqrDistanceToTarget;
        public float Speed;
        public Transform[] Waypoints;
    }
}