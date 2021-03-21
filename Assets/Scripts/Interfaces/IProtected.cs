using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IProtected
    {
        void StartProtection(Transform target);
        void FinishProtection();
    }
}