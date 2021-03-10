using System;
using Assets.Scripts.Interfaces;
using UnityEngine;


namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class ObjectView : MonoBehaviour, IView
    {
        public event Action<IView> OnCollision = delegate (IView view) {  };
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            TryGetComponent(out _rigidbody);
            TryGetComponent(out _spriteRenderer);
        }

        private void OnDestroy()
        {
            _rigidbody = null;
            _spriteRenderer = null;
        }

        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform Transform => transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollision?.Invoke(other.gameObject.GetComponent<IView>());
        }
    }
}