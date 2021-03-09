using System;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IView
    {
        event Action<IView> OnCollision;
        Rigidbody2D Rigidbody { get; }
        Transform Transform { get; }
        SpriteRenderer SpriteRenderer { get; }
    }
}