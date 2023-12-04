using System;
using UnityEngine.Serialization;

namespace BGSTest
{
    [Serializable]
    public class InventoryItem
    {
        public Item item;
        [FormerlySerializedAs("stock")] public int stack; // todo: not every item should be stackable
    }
}