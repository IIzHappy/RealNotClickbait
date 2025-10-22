using UnityEngine;

public class Clue : Interactable
{
    Item item;
    private void Start()
    {
        _isItem = true;
    }
    public override void Interacted()
    {
        item = gameObject.GetComponent<Item>();
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
