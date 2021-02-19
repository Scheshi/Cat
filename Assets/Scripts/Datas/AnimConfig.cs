using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Structs;
using UnityEngine;


[CreateAssetMenu(menuName = "Datas/Animation Config")]
public class AnimConfig : ScriptableObject
{
    public AnimationList[] Animations;
}
