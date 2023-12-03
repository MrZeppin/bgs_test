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
        [SerializeField]
        private RectTransform content;

        [SerializeField]
        private ShopSlotUI slotPrefab;

        [Space]
        [SerializeField]
        private bool testShopEnabled;

        [SerializeField]
        private Shop testShop;

        private void Awake()
        {
            Debug.Assert(content);
            Debug.Assert(slotPrefab);
            slotPrefab.gameObject.SetActive(false);
            if (testShopEnabled && testShop)
            {
                Open(testShop);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void Open(Shop shop)
        {
            foreach (var slot in shop.slots)
            {
                var slotUI = Instantiate(slotPrefab, content);
                slotUI.SetSlot(slot);
                slotUI.gameObject.SetActive(true);
            }
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}