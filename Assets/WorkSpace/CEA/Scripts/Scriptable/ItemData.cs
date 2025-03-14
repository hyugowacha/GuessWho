using UnityEngine;


[CreateAssetMenu(fileName ="NewItemData", menuName = "Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemType;
    public int bulletAmount;
}
