using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;
using RPG.Core;
using TMPro;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _nav;
        private Animator _anim;
        private ActionScheduler _scheduler;
        private Health _health;


        void Start()
        {
            _health = GetComponent<Health>();
            _nav = GetComponent<NavMeshAgent>();
            _anim = GetComponent<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
        }

       
        void Update()
        {
            _nav.enabled = !_health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(GameObject destination)
        {
            _scheduler.StartAction(this);

            MoveTo(destination);
        }


        public void MoveTo(GameObject destination)
        {

            _nav.destination = destination.transform.position;
            _nav.isStopped = false;
        }

        public void Cancel()
        {
            _nav.isStopped = true;
        }

    

        private void UpdateAnimator()
        {
            Vector3 vel = _nav.velocity;
            Vector3 localVel = transform.InverseTransformDirection(vel);
            float speed = localVel.z;
           _anim.SetFloat("Blend", speed);
        }
    }
}