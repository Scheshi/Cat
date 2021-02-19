using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class ObjectView : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        public SpriteRenderer SpriteRenderer;

        private void Awake()
        {
            TryGetComponent(out Rigidbody2D);
            TryGetComponent(out SpriteRenderer);
        }
    }
}