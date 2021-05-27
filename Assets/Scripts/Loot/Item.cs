using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Loot  
{
    public class Item : MonoBehaviour
    {
        public enum ItemType
        {
            WEAPON,
            HEALTH,
            MATERIAL,
            TOOL,
            MISC
        }

        public ItemType _itemType;
    }
}