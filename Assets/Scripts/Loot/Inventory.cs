using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace RPG.Loot
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> items;
        [SerializeField] private Text inventorytext;
        private Inventory _playerInventory;
        private Inventory _targetInventory;
      
        private Item _item;


        // Start is called before the first frame update
        void Start()
        {
            inventorytext.text = gameObject.name + "\n";

            if (gameObject.CompareTag("Player"))
            {
                _playerInventory = this;
            }
            else
            {
                _playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
            }

            foreach (var item in items)
            {
                Debug.Log(gameObject.name + "\n" + item._itemType);
                _item = item.GetComponent<Item>();
                inventorytext.text += _item._itemType.ToString() + "\n";
            }
            
        }

        public void ShowInventory(Inventory inventory)
        {
            inventorytext.text = gameObject.name + "\n";
            
            foreach (var item in inventory.items)
            {
                _item = item.GetComponent<Item>();
                inventorytext.text += _item._itemType.ToString() + "\n";
            }
        }


        public void TransferToPlayer()
        {
            Debug.Log(_targetInventory.name + " is the target inventory");

            Debug.Log("Transferring to player");
            foreach (var item in _targetInventory.items)
            {
                Debug.Log(item);

                _playerInventory.AddItem(item);
                _targetInventory.RemoveItem(item);
            }

            //ShowInventory();
        }

        private void TransferToLootable()
        {
            foreach (var item in items)
            {
                _targetInventory.AddItem(item);
                _playerInventory.RemoveItem(item);
            }

            //ShowInventory();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
        }

        public bool IsEmpty()
        {
            return items.Count <= 0;
        }

        public void GETTargetInventory(GameObject target)
        {
            _targetInventory = target.GetComponent<Inventory>();
            Debug.Log(this.gameObject.name + " Got Inventory of " + _targetInventory.name);
        }
    }
}