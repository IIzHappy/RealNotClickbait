using UnityEngine;

public class Key : Interactable
{
    Item item;
    public override void Interacted()
    {
        item = gameObject.GetComponent<Item>();
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
