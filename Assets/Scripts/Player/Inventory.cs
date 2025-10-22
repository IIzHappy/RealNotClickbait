using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<Item> items = new List<Item>();

    public GameObject _inventory;

    public GameObject _cipher11;
    public GameObject _cipher12;
    public GameObject _cipher2;
    public GameObject _card;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OpenInv(bool open)
    {
        if (!_inventory.activeSelf)
        {
            _inventory.SetActive(true);
        }
        else
        {
            _inventory.SetActive(false);
        }
    }

    public void AddCipher1()
    {
        _cipher11.SetActive(true);
        _cipher12.SetActive(true);
    }
    public void AddCipher2()
    {
        _cipher2.SetActive(true);
    }
    public void AddCard()
    {
        _card.SetActive(true);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item._itemName + " picked up");
    }

    public bool CheckItem(Item item)
    {
        if (items.Contains(item))
        {
            return true;
        }
        return false;
    }

    public void UseItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item._itemName + " used");
        }
    }
}
