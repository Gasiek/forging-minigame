using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _quests;
    [SerializeField] private MachineController _runeCarver;
    [SerializeField] private MachineController _dragonForge;
    [SerializeField] private GameObject _questUIPrefab;
    [SerializeField] private Transform _questUIContainer;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private List<BonusItem> _possibleBonuses;
    private Dictionary<Quest, QuestUIElement> _questUIMap = new();
    private bool _allQuestsAlreadyCompleted;

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
        foreach (var quest in _quests)
        {
            quest.CurrentProgress = 0;
            GameObject questUIObject = Instantiate(_questUIPrefab, _questUIContainer);
            QuestUIElement questUI = questUIObject.GetComponent<QuestUIElement>();
            
            questUI.Initialize(quest.QuestName, quest.QuestDescription, quest.CurrentProgress, quest.RequiredAmount);
            _questUIMap[quest] = questUI;
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
                    _questUIMap[quest].UpdateUI(quest.CurrentProgress, quest.RequiredAmount);
                }

                if (quest.CurrentProgress >= quest.RequiredAmount)
                {
                    CompleteQuest(quest);
                }
            }
        }
        if (AllQuestsCompleted() && !_allQuestsAlreadyCompleted)
        {
            _allQuestsAlreadyCompleted = true;
            GiveRandomBonus();
        }
    }

    private void CompleteQuest(Quest quest)
    {
        ToastNotificationManager.Instance.ShowNotification($"You completed the '{quest.QuestName}' quest and unlocked {quest.MachineToUnlock}!");

        switch (quest.MachineToUnlock)
        {
            case MachineType.RuneCarver:
                _runeCarver?.UnlockThisNewMachine();
                break;
            case MachineType.DragonForge:
                _dragonForge?.UnlockThisNewMachine();
                break;
        }
    }
    
    private bool AllQuestsCompleted()
    {
        foreach (var quest in _quests)
        {
            if (quest.CurrentProgress < quest.RequiredAmount)
            {
                return false;
            }
        }
        return true;
    }
    
    private void GiveRandomBonus()
    {
        if (_possibleBonuses.Count == 0) return;

        BonusItem randomBonus = _possibleBonuses[Random.Range(0, _possibleBonuses.Count)];
        _inventoryManager.AddBonusItem(randomBonus);

        ToastNotificationManager.Instance.ShowNotification($"You received a bonus: {randomBonus.ItemName}!");
    }
}