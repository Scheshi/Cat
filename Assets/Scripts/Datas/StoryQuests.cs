using System.Linq;

namespace Datas
{
    public class StoryQuests
    {
        public Quest[] Quests;
        public int currentId;

        public void StartStory()
        {
            currentId = 0;
        }
        
        public void NextQuest()
        {
            Quests[currentId].isComplete = true;
            currentId++;
        }
    }
}