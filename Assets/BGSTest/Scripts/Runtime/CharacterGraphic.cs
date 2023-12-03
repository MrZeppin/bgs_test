using System;
using Unity.VisualScripting;
using UnityEngine;

namespace BGSTest
{
    [CreateAssetMenu(fileName = "Visual", menuName = "BGSTest/Character/Visual", order = 0)]
    public class CharacterGraphic : ScriptableObject
    {
        public CharacterGraphicLayer layer;
        public RuntimeAnimatorController anim;
    }
}