using Assets.Scripts;

namespace Datas
{
    public class QuestItem : Quest
    {
        public int count;
        public ObjectView _itemPrefab;
        private int currentCount;

        public override void OnIncrementProgress()
        {
            currentCount++;
            if (currentCount == count)
            {
                isComplete = true;
            }
        }
    }
}