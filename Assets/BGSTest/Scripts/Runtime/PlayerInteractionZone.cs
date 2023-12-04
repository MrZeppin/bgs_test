using System;
using UnityEngine;

namespace BGSTest
{
    public class PlayerInteractionZone : MonoBehaviour
    {
        private Player player;

        public Action onInteract;

        private void Awake()
        {
            player = Player.Instance;
        }

        private void OnDisable()
        {
            if (Player.Instance.GetCurrentInteractionZone() == this)
            {
                player.SetCurrentInteractionZone(null);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var character = other.GetComponent<Character>();
            if (character && player.character == character)
            {
                player.SetCurrentInteractionZone(this);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var character = other.GetComponent<Character>();
            if (character && player.character == character)
            {
                player.SetCurrentInteractionZone(null);
            }
        }

        public void Interact()
        {
            if (PauseManager.IsPaused)
                return;
            onInteract?.Invoke();
        }
    }
}