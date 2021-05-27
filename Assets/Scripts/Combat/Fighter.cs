using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Health _target;
        private Mover _mover;

        public float weaponRange = 2f;
        public float weaponDamage = 5f;
        private NavMeshAgent _agent;
        private ActionScheduler _scheduler;
        private Animator _anim;


        public float TimebetweenAttacks = 1f;
        private float TimeSinceLastAttack = Mathf.Infinity;
        private string defaultStopAttack = "stopAttack";
        private string defaultAttack = "attack";

        private string attack;
        private string stopAttack;
        private void Start()
        {
           _anim = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            _agent = GetComponent<NavMeshAgent>();
            _scheduler = GetComponent<ActionScheduler>();
            attack = defaultAttack;
            stopAttack = defaultStopAttack;
        }

        private void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;
            if (_target == null) return;
            if (_target.IsDead()) return;

            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.gameObject);
            }
            else
            {
                AttackBehaviour();
                _mover.Cancel();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (TimeSinceLastAttack > TimebetweenAttacks)
            {
                //Triggers Hit() Event
                TriggerAttack();
                TimeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        { 
            _anim.ResetTrigger(stopAttack);
             _anim.SetTrigger(attack);
        }

        // Animation Event
        private void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }

            Health targettoTest = combatTarget.GetComponent<Health>();
            return targettoTest != null && !targettoTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            _scheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            _anim.ResetTrigger(attack);
            _anim.SetTrigger(stopAttack);
        }
    }
}