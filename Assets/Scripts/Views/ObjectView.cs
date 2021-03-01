using Assets.Scripts.Interfaces;
using UnityEngine;


namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class ObjectView : MonoBehaviour, IView
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            TryGetComponent(out _rigidbody);
            TryGetComponent(out _spriteRenderer);
        }

        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform Transform => transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}