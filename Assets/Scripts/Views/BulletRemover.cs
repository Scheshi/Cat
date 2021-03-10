using UnityEngine;


namespace Views
{
    public class BulletRemover : MonoBehaviour
    {
        private void Remove()
        {
            Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            Remove();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Remove();
        }
    }
}