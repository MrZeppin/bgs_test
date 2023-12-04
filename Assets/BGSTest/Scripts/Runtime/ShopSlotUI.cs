using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGSTest
{
    public class ShopSlotUI : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private TextMeshProUGUI price;

        [SerializeField]
        private TextMeshProUGUI stock;

        [SerializeField]
        private Button button;

        private ShopSlot shopSlot;
        private InventoryItem inventoryItem;
        private CanvasGroup canvasGroup;

        private bool isInitialized;

        private void Awake()
        {
            if (isInitialized)
                return;
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            Player.Instance.character.inventory.onItemAdded -= OnInventoryAdded;
        }

        private void OnInventoryAdded(Inventory inventory, InventoryItem item, int count)
        {
            if (inventoryItem != this.inventoryItem)
                return;
            UpdateUI();
        }

        public void SetInventoryItem(InventoryItem item)
        {
            this.shopSlot = null;
            this.inventoryItem = item;
            Player.Instance.character.inventory.onItemAdded += OnInventoryAdded;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                if (inventoryItem.stack <= 0)
                    return;
                Player.Instance.character.inventory.Remove(inventoryItem.item);
                Player.Instance.character.money += ShopUI.Instance.CurrentShop.GetSellingPrince(inventoryItem.item);
                if (inventoryItem.stack <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    UpdateUI();
                }
            });
            UpdateUI();
        }

        public void SetShopSlot(ShopSlot slot)
        {
            this.shopSlot = slot;
            this.inventoryItem = null;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                if (shopSlot.stock <= 0)
                    return;
                var itemPrice = shopSlot.GetItemPrice();
                if (Player.Instance.character.money < itemPrice)
                    return;
                shopSlot.stock--;
                if (shopSlot.item is ItemEquippable equippable)
                {
                    Player.Instance.character.SetGraphic(equippable.characterGraphic);
                }
                Player.Instance.character.money -= itemPrice;
                Player.Instance.character.inventory.Add(shopSlot.item);
                UpdateUI();
            });
            UpdateUI();
        }

        private void UpdateUI()
        {
            Awake();

            if (shopSlot != null)
            {
                icon.sprite = shopSlot.item.icon;
                icon.preserveAspect = true;
                price.text = $"${shopSlot.GetItemPrice()}";
                stock.text = $"x{shopSlot.stock}";
                canvasGroup.interactable = shopSlot.stock > 0;
            }
            else
            {
                // todo: don't hardcode, the price reduction for selling should be dependent of the shop
                icon.sprite = inventoryItem.item.icon;
                icon.preserveAspect = true;
                price.text = $"${ShopUI.Instance.CurrentShop.GetSellingPrince(inventoryItem.item)}";
                stock.text = $"x{inventoryItem.stack}";
                canvasGroup.interactable = inventoryItem.stack > 0;
            }

            ShopUI.Instance.UpdatePlayerMoneyText();
        }
    }
}