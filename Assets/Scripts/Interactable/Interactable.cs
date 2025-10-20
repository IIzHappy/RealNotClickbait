using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] Item _item;
    public bool _isItem;
    public List<Item> _requirements;
    public bool _canInteract = true;

    private void Start()
    {
        if (_item != null)
        {
            _isItem = true;
        }
    }

    public bool Interact()
    {
        if (_isItem)
        {
            Inventory.Instance.AddItem(_item);
            Destroy(gameObject);
            return true;
        }

        if (_requirements.Count == 0)
        {
            Interacted();
            return true;
        }

        bool canInteract = true;
        foreach (Item item in _requirements)
        {
            if (!Inventory.Instance.CheckItem(item)) canInteract = false;
        }
        if (canInteract)
        {
            foreach (Item item in _requirements)
            {
                Inventory.Instance.UseItem(item);
            }
            Interacted();
            return true;
        }
        return false;
    }

    public abstract void Interacted();
}
