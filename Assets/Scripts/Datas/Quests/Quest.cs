using UnityEngine;


namespace Datas
{
    public class Quest : ScriptableObject
    {
        public int id;
        public string name;
        public bool isComplete;

        public virtual void OnIncrementProgress()
        {
            
        }
        
        public virtual void OnComplete()
        {
            
        }
    }
}