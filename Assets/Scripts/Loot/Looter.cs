using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.AI;

namespace RPG.Loot
{
    [RequireComponent(typeof(Inventory))]
    public class Looter : MonoBehaviour, IAction
    {
        private LootTarget _target;
        private Mover _mover;

        [SerializeField] private float lootRange = 1f;
        private NavMeshAgent _agent;
        private ActionScheduler _scheduler;
        private Animator _anim;

        [SerializeField] private float TimebetweenGrabs = 3f;
        private float TimeSinceLastGrab = Mathf.Infinity;
        private Inventory _inventory;
        private Inventory _targetInventory;

        [SerializeField] private GameObject inventoryCanvas;


        private void Start()
        {
            _anim = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            _agent = GetComponent<NavMeshAgent>();
            _scheduler = GetComponent<ActionScheduler>();
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Tab)) inventoryCanvas.SetActive(true);
            if(Input.GetKeyUp(KeyCode.Tab)) inventoryCanvas.SetActive(false);
            TimeSinceLastGrab += Time.deltaTime;
            if (_target == null) return;

            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.gameObject);
            }
            else
            {
                LootBehaviour();
                _mover.Cancel();
            }


            if (Input.GetKeyDown(KeyCode.K))
            {
                _inventory.TransferToPlayer();
            }
            
        }

        private void LootBehaviour()
        {
            transform.LookAt(_target.transform);
            if (TimeSinceLastGrab > TimebetweenGrabs)
            {
                //Triggers Hit() Event
                TriggerLoot();
                TimeSinceLastGrab = 0f;
            }
        }

        private void TriggerLoot()
        {
            _anim.ResetTrigger("stopAttack");
            _anim.SetTrigger("attack");
        }

        // Animation Event
        private void Hit()
        {
            if (_target == null) return;

            //TODO Open Inventory of target and own inventory for transfer
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < lootRange;
        }


        public bool CanLoot(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }

            Inventory targettoTest = combatTarget.GetComponent<Inventory>();
            return targettoTest != null && !targettoTest.IsEmpty();
        }

        public void Loot(GameObject lootTarget)
        {
            _scheduler.StartAction(this);
            _target = lootTarget.GetComponent<LootTarget>();
            inventoryCanvas.SetActive(true);
            lootTarget.GetComponent<Inventory>().GETTargetInventory(lootTarget);
            _inventory.ShowInventory(_inventory);
            lootTarget.GetComponent<Inventory>().ShowInventory(lootTarget.GetComponent<Inventory>());
            
            Debug.Log("Looting");
        }

        public void Cancel()
        {
            StopLoot();
            _target = null;
            inventoryCanvas.SetActive(false);
        }

        private void StopLoot()
        {
            _anim.ResetTrigger("attack");
            _anim.SetTrigger("stopAttack");
        }
    }
}