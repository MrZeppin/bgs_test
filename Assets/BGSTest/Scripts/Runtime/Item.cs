using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BGSTest
{
    public abstract class Item : ScriptableObject
    {
        public string displayName;
        public Sprite icon;
        public int price;
        [TextArea] public string description;
    }
}