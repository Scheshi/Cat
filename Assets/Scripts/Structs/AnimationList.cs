using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;


namespace Assets.Scripts.Structs
{
    [Serializable]
    public struct AnimationList
    {
        public AnimState AnimState;
        public List<Sprite> AnimSprites;
    }
}