using UnityEngine;

[CreateAssetMenu(fileName = "New Bonus Item", menuName = "ForgingGame/BonusItem")]
public class BonusItem : Item, IBonusEffect
{
    [SerializeField] private float _successRateBonus = 0.1f;
    [SerializeField] private float _craftingTimeReduction = 2f;
    [SerializeField] private float _minimumCraftingTime = 1f;

    public BonusType BonusEffectType;

    public void ApplyEffect(ref float successRate, ref float craftingTime)
    {
        switch (BonusEffectType)
        {
            case BonusType.LuckyCharm:
                successRate += _successRateBonus;
                break;
            case BonusType.TimeAmulet:
                craftingTime = Mathf.Max(_minimumCraftingTime, craftingTime - _craftingTimeReduction);
                break;
        }
    }
}

public enum BonusType
{
    LuckyCharm,
    TimeAmulet
}