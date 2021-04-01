using System;
using Assets.Scripts;
using UnityEngine;

namespace Views
{
    public class QuestObjectView : ObjectView
    {
        public event Action<ObjectView> OnTargetAction = delegate {  };

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTargetAction.Invoke(other.GetComponent<ObjectView>());
        }
    }
}