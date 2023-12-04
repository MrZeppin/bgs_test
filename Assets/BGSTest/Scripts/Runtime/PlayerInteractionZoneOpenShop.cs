using System;
using UnityEngine;

namespace BGSTest
{
    public class PlayerInteractionZoneOpenShop : MonoBehaviour
    {
        public Shop shop;

        private void Awake()
        {
            GetComponent<PlayerInteractionZone>().onInteract += () =>
            {
                ShopUI.Instance.Open(shop);
            };
        }
    }
}