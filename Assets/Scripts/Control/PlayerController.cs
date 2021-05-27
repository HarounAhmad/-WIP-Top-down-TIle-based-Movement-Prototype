using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Loot;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter _fighter;
        private Health _health;
        private Looter _looter;

        private void Start()
        {
            _health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _looter = GetComponent<Looter>();
        }

        private void Update()
        {
            if(_health.IsDead()) return;
            if (InteractwithCombat()) return;
            if(InteractwithLoot()) return;
            if (InteractwithMovement()) return;
        }

        private bool InteractwithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    _fighter.Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }
        
        private bool InteractwithLoot()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                LootTarget target = hit.transform.GetComponent<LootTarget>();
                if (target == null) continue;
                if (!GetComponent<Looter>().CanLoot(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    _looter.Loot(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractwithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    

                    mover.StartMoveAction(hit.transform.gameObject);
                }

                return true;
            }

            return false;
        }

        

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            
        }
    }
}