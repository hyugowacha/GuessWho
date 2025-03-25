using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


[CreateAssetMenu(fileName ="NewItemData", menuName = "Item Data", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int bulletAmount;
    public ItemType itemType;
}

//[System.Serializable]
//public struct ItemDataSerializable
//{
//    public string itemName;
//    public int bulletAmount;
//    public ItemType itemType;

//    public ItemDataSerializable(string name, int amount, ItemType type)
//    {
//        itemName = name;
//        bulletAmount = amount;
//        itemType = type;
//    }
//}

//public static class ItemDataSerilization
//{
//    public static byte[] SerializeItemData(object obj)
//    {
//        ItemDataSerializable item = (ItemDataSerializable)obj;
//        MemoryStream stream = new MemoryStream();
//        BinaryWriter writer = new BinaryWriter(stream);

//        writer.Write(item.itemName);
//        writer.Write(item.bulletAmount);  
//        writer.Write((int)item.itemType);

//        return stream.ToArray();    
//    }

//    public static object DeserializeItemData(byte[] data)
//    {
//        MemoryStream stream = new MemoryStream(data);
//        BinaryReader reader = new BinaryReader(stream);

//        string name = reader.ReadString();
//        int amount = reader.ReadInt32();
//        ItemType type = (ItemType)reader.ReadInt32();

//        return new ItemDataSerializable(name, amount, type);
//    }
//}

//public class PhotonCustomTypes
//{
//    public static void Register()
//    {
//        PhotonPeer.RegisterType(typeof(ItemDataSerializable), (byte)'I',
//            new SerializeMethod(ItemDataSerilization.SerializeItemData),
//            new DeserializeMethod(ItemDataSerilization.DeserializeItemData));
//    }
//}

//public class PhotonManager : MonoBehaviourPunCallbacks
//{
//    private void Awake()
//    {
//        PhotonCustomTypes.Register();
//    }
//}
