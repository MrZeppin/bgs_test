using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace BGSTest
{
    [DefaultExecutionOrder(-1)] // note: hack for awake being called before others
    public class Player : MonoBehaviour
    {
        public static readonly KeyCode interactionKeyCode = KeyCode.Space;

        public static Player Instance { get; private set; }

        public Character character;

        public Transform interactionUI;
        public Vector3 interactionUIOffset;

        private PlayerInteractionZone currentInteractionZone;

        private void Awake()
        {
            Instance = this;
            interactionUI.gameObject.SetActive(false);
        }

        private void Update()
        {
            // note: for now hard coding controls, cus of time constraints
            character.moveDirection.x = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
            character.moveDirection.y = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);
            if (currentInteractionZone)
            {
                // todo: don't hardcode offset
                interactionUI.position = character.transform.position + interactionUIOffset;
                if (Input.GetKeyDown(interactionKeyCode))
                {
                    currentInteractionZone.Interact();
                }
            }
        }

        public PlayerInteractionZone GetCurrentInteractionZone() => currentInteractionZone;

        public void SetCurrentInteractionZone(PlayerInteractionZone zone)
        {
            currentInteractionZone = zone;
            if (interactionUI)
            {
                interactionUI.gameObject.SetActive(currentInteractionZone);
                interactionUI.GetComponentInChildren<TextMeshProUGUI>().text = $"Press \"{interactionKeyCode}\" to interact";
            }
        }
    }
}