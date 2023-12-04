using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGSTest
{
    public class Shop : MonoBehaviour
    {
        public Character shopkeeper;
        public List<ShopSlot> slots;

        // todo: selling price depending on item type
        public int GetSellingPrince(Item item)
        {
            return (int)(item.price * .5f);
        }
    }
}