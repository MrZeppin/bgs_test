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
        private RectTransform content;

        [SerializeField]
        private ShopSlotUI slotPrefab;

        [SerializeField]
        private Button closeButton;

        [Space]
        [SerializeField]
        private bool testShopEnabled;

        [SerializeField]
        private Shop testShop;

        private void Awake()
        {
            Instance = this;
            Debug.Assert(content);
            Debug.Assert(slotPrefab);
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

        public void Open(Shop shop)
        {
            Debug.Assert(shop);
            // todo: pooling
            for (int i = content.childCount - 1; i >= 0; i--)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            foreach (var slot in shop.slots)
            {
                var slotUI = Instantiate(slotPrefab, content);
                slotUI.SetSlot(slot);
                slotUI.gameObject.SetActive(true);
            }
            gameObject.SetActive(true);
            PauseManager.IsPaused = true;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            PauseManager.IsPaused = false;
        }
    }
}