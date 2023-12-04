using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BGSTest
{
    [Serializable]
    public class Inventory
    {
        [FormerlySerializedAs("slots")] public List<InventoryItem> items = new();
        public Action<Inventory, InventoryItem, int> onItemAdded;

        public void Add(Item item, int count = 1)
        {
            count = Mathf.Max(count, 1);
            foreach (var inventoryItem in items)
            {
                if (inventoryItem.item == item)
                {
                    inventoryItem.stack += count;
                    onItemAdded?.Invoke(this, inventoryItem, count);
                    return;
                }
            }
            var newInventoryItem = new InventoryItem() { item = item, stack = count };
            items.Add(newInventoryItem);
            onItemAdded?.Invoke(this, newInventoryItem, count);
        }

        public void Remove(Item item, int count = 1)
        {
            count = Mathf.Max(count, 1);
            foreach (var inventoryItem in items)
            {
                if (inventoryItem.item == item)
                {
                    inventoryItem.stack -= count;
                    if (inventoryItem.stack <= 0)
                    {
                        items.Remove(inventoryItem);
                    }
                    return;
                }
            }
        }
    }
}