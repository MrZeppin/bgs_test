using System;
using UnityEngine;

namespace BGSTest
{

    [CreateAssetMenu(fileName = "Equippable", menuName = "BGSTest/Items/Equippable", order = 0)]
    public class ItemEquippable : Item
    {
        public CharacterGraphic characterGraphic;
    }
}