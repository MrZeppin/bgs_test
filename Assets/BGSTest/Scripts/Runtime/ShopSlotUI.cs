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

        private ShopSlot slot;
        private CanvasGroup canvasGroup;

        private bool isInitialized;

        private void Awake()
        {
            if (isInitialized)
                return;
            canvasGroup = GetComponent<CanvasGroup>();
            button.onClick.AddListener(() =>
            {
                if (slot.stock <= 0)
                    return;
                slot.stock--;
                if (slot.item is ItemEquippable equippable)
                    FindObjectOfType<Character>().SetGraphic(equippable.characterGraphic);
                UpdateUI();
            });
        }

        public void SetSlot(ShopSlot slot)
        {
            this.slot = slot;
            UpdateUI();
        }

        private void UpdateUI()
        {
            Debug.Assert(slot != null);
            Awake();
            icon.sprite = slot.item.icon;
            icon.preserveAspect = true;
            price.text = $"${slot.GetItemPrice()}";
            stock.text = $"x{slot.stock}";
            canvasGroup.interactable = slot.stock > 0;
        }
    }
}