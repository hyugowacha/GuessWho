using UnityEngine;


[CreateAssetMenu(fileName ="NewItemData", menuName = "Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int bulletAmount;
    public ItemType itemType;
}
