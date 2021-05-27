using System;
using UnityEngine;

namespace RPG.Loot
{
    public class Weapon : Item
    {
        private void Start()
        {
            Debug.Log("this is of type: " + this);
        }
    }
}