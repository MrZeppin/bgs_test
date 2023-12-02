using UnityEngine;

namespace BGSTest
{
    public class Character : MonoBehaviour
    {
        public float moveSpeed = 0f;
        public Vector2 moveDirection;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            moveDirection.x = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
            moveDirection.y = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);
            rb.velocity = moveDirection.normalized * moveSpeed;
        }
    }
}