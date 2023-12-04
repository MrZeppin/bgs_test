using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BGSTest
{
    public class ShopUI : MonoBehaviour
    {
        public static ShopUI Instance { get; private set; }

        [SerializeField]
        private ShopSlotUI slotPrefab;

        [Space]
        [SerializeField]
        private RectTransform buyItemsParent;

        [SerializeField]
        private RectTransform sellItemsParent;

        [Space]
        [SerializeField]
        private Button closeButton;

        [Space]
        [SerializeField]
        private TextMeshProUGUI playerMoneyText;

        [Space]
        [SerializeField]
        private TextMeshProUGUI shopkeeperDialogueText;

        [SerializeField]
        private Image shopkeeperPortraitImage;

        [Space]
        [SerializeField]
        private bool testShopEnabled;

        [SerializeField]
        private Shop testShop;

        public Shop CurrentShop { get; private set; }

        private void Awake()
        {
            Instance = this;
            Debug.Assert(buyItemsParent);
            Debug.Assert(sellItemsParent);
            Debug.Assert(slotPrefab);
            Debug.Assert(playerMoneyText);
            if (testShopEnabled && testShop)
            {
                Open(testShop);
            }
            else
            {
                gameObject.SetActive(false);
            }
            closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            Player.Instance.character.inventory.onItemAdded -= OnInventoryAdded;
        }

        public void Open(Shop shop)
        {
            Debug.Assert(shop);
            CurrentShop = shop;
            // todo: pooling
            foreach (Transform x in buyItemsParent)
            {
                Destroy(x.gameObject);
            }
            foreach (var shopSlot in shop.slots)
            {
                var slotUI = Instantiate(slotPrefab, buyItemsParent);
                slotUI.SetShopSlot(shopSlot);
                slotUI.gameObject.SetActive(true);
            }
            foreach (Transform x in sellItemsParent)
            {
                Destroy(x.gameObject);
            }
            foreach (var inventoryItem in Player.Instance.character.inventory.items)
            {
                var slotUI = Instantiate(slotPrefab, sellItemsParent);
                slotUI.SetInventoryItem(inventoryItem);
                slotUI.gameObject.SetActive(true);
            }
            shopkeeperPortraitImage.sprite = shop.shopkeeper.portrait;
            shopkeeperPortraitImage.preserveAspect = true;
            UpdatePlayerMoneyText();
            gameObject.SetActive(true);
            Player.Instance.character.inventory.onItemAdded += OnInventoryAdded;
            PauseManager.IsPaused = true;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            PauseManager.IsPaused = false;
        }

        public void UpdatePlayerMoneyText()
        {
            playerMoneyText.text = $"${Player.Instance.character.money}";
        }

        private void OnInventoryAdded(Inventory inventory, InventoryItem item, int count)
        {
            if (item.stack == 1) // first time added
            {
                var slotUI = Instantiate(slotPrefab, sellItemsParent);
                slotUI.SetInventoryItem(item);
                slotUI.gameObject.SetActive(true);
            }
        }
    }
}