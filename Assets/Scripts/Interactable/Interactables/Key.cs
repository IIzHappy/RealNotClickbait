using UnityEngine;

public class Key : Interactable
{
    Item item;
    private void Start()
    {
        _isItem = true;
    }
    public override void Interacted()
    {
        item = gameObject.GetComponent<Interactable>()._item;
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
