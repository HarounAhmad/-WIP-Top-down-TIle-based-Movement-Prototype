using System;
using UnityEngine;
using RPG.Core;
using RPG.Loot;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        private Health _health;
        private LootTarget _lootTarget;

        private void Start()
        {
            _lootTarget = GetComponent<LootTarget>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.IsDead())
            {
                _lootTarget.enabled = true;
            }
        }
    }
}