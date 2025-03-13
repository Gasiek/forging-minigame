using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ForgingGame/Item")]
public class Item : ScriptableObject
{
    public int ID;
    public string ItemName;
    public ItemType ItemType;
    [TextArea(1, 2)]
    public string Description;
    public Sprite Icon;
    public bool IsStackable;
}

public enum ItemType
{
    Resource,
    Crafted,
    Bonus
}
