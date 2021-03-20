using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IProtected
    {
        void StartProtection(GameObject invader);
        void FinishProtection(GameObject invader);
    }
}