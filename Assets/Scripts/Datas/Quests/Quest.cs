using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using Views;
using Object = UnityEngine.Object;


namespace Datas
{
    public abstract class Quest : ScriptableObject, IDisposable
    {
        [Serializable]
        protected struct ObjectQuest
        {
            public QuestObjectView ObjectViewPrefab;
            public int count;
        }
        public event Action OnCompleteEvent = delegate {  };
        [SerializeField] protected int id;
        [SerializeField] protected string _tooltip;
        [SerializeField] protected ObjectQuest _type;
        private List<QuestObjectView> _objects = new List<QuestObjectView>();
        private int _count;

        public string Tooltip => _tooltip;
        public int Count => _count;

        public int AllCount => _type.count;

        public void Init()
        {
            Debug.Log("Init");
            var objects = Object.FindObjectsOfType<QuestObjectView>();
            Debug.Log(objects.Length);
            foreach (var item in objects)
            {
                if (item.gameObject == PrefabUtility
                    .GetCorrespondingObjectFromSource(_type.ObjectViewPrefab.gameObject))
                {
                    Debug.Log("Find");
                    _objects.Add(item);
                    item.OnTargetAction += OnIncrementProgress;
                }
            }
        }

        public virtual void OnIncrementProgress(ObjectView view)
        {
            _count++;
            if (_count >= _type.count)
            {
                OnComplete();
            }
        }
        
        public virtual void OnComplete()
        {
            OnCompleteEvent.Invoke();
            Dispose();
        }

        public void Dispose()
        {
            foreach (var item in _objects)
            {
                item.OnTargetAction -= OnIncrementProgress;
            }
        }
    }
}