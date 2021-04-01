using Assets.Scripts;
using UnityEngine;


namespace Datas
{
    [CreateAssetMenu(menuName = "Configs/Quests/Quest Item")]
    public class QuestItem : Quest
    {
        public override void OnIncrementProgress(ObjectView view)
        {
            Debug.LogWarningFormat("Take");
            if (view.gameObject.CompareTag("Player"))
            {
                base.OnIncrementProgress(view);
                view.gameObject.SetActive(false);
            }
        }
    }
}