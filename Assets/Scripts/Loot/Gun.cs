using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RPG.Loot
{
    public class Gun : Weapon
    {
        [SerializeField] private string weaponName;
        [SerializeField] private float range;
        [SerializeField] private float damage;
        [SerializeField] private float fireRate;
    }
}