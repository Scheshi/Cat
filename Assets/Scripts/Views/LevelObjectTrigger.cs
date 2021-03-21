using System;
using UnityEngine;

namespace Views
{
    public class LevelObjectTrigger : MonoBehaviour
    {
        #region Events

        public event EventHandler<Transform> TriggerEnter;
        public event EventHandler<Transform> TriggerExit;

        #endregion
  

        #region Unity methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(this, other.transform);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit?.Invoke(this, other.transform);
        }

        #endregion

    }
}