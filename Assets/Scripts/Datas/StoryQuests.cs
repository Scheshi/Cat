using UnityEngine;
using UnityEngine.UI;


namespace Datas
{
    public class StoryQuests : MonoBehaviour
    {
        [SerializeField] private Quest[] _quests;
        [SerializeField] private Text _text;
        private int currentId;

        public void StartStory()
        {
            currentId = 0;
            StartQuest();
        }
        
        private void NextQuest()
        {
            _quests[currentId].OnCompleteEvent -= NextQuest;
            currentId++;
            StartQuest();
        }

        private void StartQuest()
        {
            _quests[currentId].Init();
            _quests[currentId].OnCompleteEvent += NextQuest;
            _text.text = _quests[currentId].Tooltip + " " + _quests[currentId].Count + "/" + _quests[currentId].AllCount;
        }
    }
}