using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using Unity.Collections;
using UnityEditor;
using RPG.Movement;

namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        private GameObject _player;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private Transform _guardLocation;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            _health = GetComponent<Health>();
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
           // _guardLocation = gameObject.transform.position;
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (_health.IsDead()) return;


            if (inAttackRangeofPlayer() && _fighter.CanAttack(_player))
            {
                _timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
             //   PatrolBehaviour();
            }

            _timeSinceLastSawPlayer += Time.deltaTime;
        }
/*
        private void PatrolBehaviour()
        {
            Transform nextPosition = _guardLocation;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            _mover.StartMoveAction(_guardLocation);
        }*/

        private Vector3 GetCurrentWaypoint()
        {
            throw new NotImplementedException();
        }

        private void CycleWaypoint()
        {
            throw new NotImplementedException();
        }

        private bool AtWaypoint()
        {
            throw new NotImplementedException();
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _fighter.Attack(_player);
        }

        private bool inAttackRangeofPlayer()
        {
            float distanceofPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceofPlayer < chaseDistance;
        }

        //Called by Unity

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}