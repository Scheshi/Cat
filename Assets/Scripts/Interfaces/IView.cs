using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IView
    {
        Rigidbody2D Rigidbody { get; }
        Transform Transform { get; }
        SpriteRenderer SpriteRenderer { get; }
    }
}