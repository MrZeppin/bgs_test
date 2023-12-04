using System;
using UnityEngine.Serialization;

namespace BGSTest
{
    [Serializable]
    public class ShopSlot
    {
        public Item item;
        public int stock;
        public int priceOverride = -1;

        public int GetItemPrice() => priceOverride >= 0 ? priceOverride : item.price;
    }
}