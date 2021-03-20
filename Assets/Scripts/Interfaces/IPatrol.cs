using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IPatrol
    {
        void StartProtection(GameObject invader);
        void FinishProtection(GameObject invader);
    }
}