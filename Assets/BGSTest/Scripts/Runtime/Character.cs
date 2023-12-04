using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BGSTest
{
    public class Character : MonoBehaviour
    {
        public float moveSpeed = 0f;
        public Vector2 moveDirection;

        private Rigidbody2D rb;

        /// <summary>
        /// <see cref="CharacterGraphicLayer"/>
        /// </summary>
        [SerializeField]
        private Animator[] anims;

        [Header("TESTING")]
        public CharacterGraphic[] graphics;

        private Vector2 prevMoveDirection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            foreach (var graphic in graphics)
            {
                SetGraphic(graphic);
            }
        }

        public void ClearGraphic(CharacterGraphicLayer layer)
        {
            var anim = anims[(int)layer];
            anim.runtimeAnimatorController = null;
            anim.gameObject.SetActive(false);
        }

        public void SetGraphic(CharacterGraphic graphic)
        {
            var anim = anims[(int)graphic.layer];
            anim.runtimeAnimatorController = graphic.anim;
            anim.gameObject.SetActive(true);
            // sync animators
            var syncSource = anims[(int)CharacterGraphicLayer.Base];
            var syncSourceStateInfo = syncSource.GetCurrentAnimatorStateInfo(0);
            anim.Play(syncSourceStateInfo.fullPathHash, 0, syncSourceStateInfo.normalizedTime);
            for (int i = 0; i < anim.parameterCount; i++)
            {
                var param = anim.GetParameter(i);
                if (param.type == AnimatorControllerParameterType.Float)
                    anim.SetFloat(param.nameHash, syncSource.GetFloat(param.nameHash));
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDirection.normalized * moveSpeed;
            if (prevMoveDirection != moveDirection)
            {
                foreach (var anim in anims)
                {
                    if (anim.gameObject.activeInHierarchy == false || anim.runtimeAnimatorController == null)
                        continue;
                    if (moveDirection.x != 0 || moveDirection.y != 0)
                    {
                        anim.Play(AnimHash.States.walk);
                        anim.SetFloat(AnimHash.Vars.moveDirX, moveDirection.y != 0 ? 0f : moveDirection.x);
                        anim.SetFloat(AnimHash.Vars.moveDirY, moveDirection.y);
                    }
                    else
                    {
                        anim.Play(AnimHash.States.idle);
                    }
                }
            }
            prevMoveDirection = moveDirection;
        }

        private static class AnimHash
        {
            public static class Vars
            {
                public static readonly int moveDirX = Animator.StringToHash("moveDirX");
                public static readonly int moveDirY = Animator.StringToHash("moveDirY");
            }

            public static class States
            {
                public static readonly int idle = Animator.StringToHash("idle");
                public static readonly int walk = Animator.StringToHash("walk");
            }
        }
    }
}