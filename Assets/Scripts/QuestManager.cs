using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _quests;
    [SerializeField] private MachineController _runeCarver;
    [SerializeField] private MachineController _dragonForge;
    [SerializeField] private List<QuestUIElement> _questUIElements;
    private Dictionary<Quest, QuestUIElement> _questUIMap;
    
    private void OnEnable()
    {
        CraftingEvents.OnItemCrafted += UpdateQuestProgress;
    }

    private void OnDisable()
    {
        CraftingEvents.OnItemCrafted -= UpdateQuestProgress;
    }
    
    private void Awake()
    {
        _questUIMap = new Dictionary<Quest, QuestUIElement>();

        for (int i = 0; i < _quests.Count; i++)
        {
            if (i < _questUIElements.Count)
            {
                _questUIMap[_quests[i]] = _questUIElements[i];
                _quests[i].CurrentProgress = 0;
                _questUIElements[i].UpdateUI(_quests[i].QuestName, _quests[i].QuestDescription, _quests[i].CurrentProgress, _quests[i].RequiredAmount);
            }
        }
    }

    private void UpdateQuestProgress(Item craftedItem)
    {
        foreach (var quest in _quests)
        {
            if (quest.RequiredItem == craftedItem && quest.CurrentProgress < quest.RequiredAmount)
            {
                quest.CurrentProgress++;

                if (_questUIMap.ContainsKey(quest))
                {
                    _questUIMap[quest].UpdateUI(quest.QuestName, quest.QuestDescription, quest.CurrentProgress, quest.RequiredAmount);
                }
                
                if (quest.CurrentProgress >= quest.RequiredAmount)
                {
                    CompleteQuest(quest);
                }
            }
        }
    }

    private void CompleteQuest(Quest quest)
    {
        ToastNotificationManager.Instance.ShowNotification($"You have completed {quest.QuestName} quest, and unlocked {quest.MachineToUnlock}");
        switch (quest.MachineToUnlock)
        {
            case MachineType.RuneCarver:
                if (_runeCarver != null) _runeCarver.UnlockThisNewMachine();
                break;
            case MachineType.DragonForge:
                if (_dragonForge != null) _dragonForge.UnlockThisNewMachine();
                break;
        }
    }
}