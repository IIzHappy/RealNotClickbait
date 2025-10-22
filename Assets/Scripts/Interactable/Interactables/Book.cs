using UnityEngine;

public class Book : Interactable
{
    Item item;
    private void Start()
    {
        _isItem = true;
    }
    public override void Interacted()
    {
        item = gameObject.GetComponent<Interactable>()._item;
        StoryController.Instance.FindJournal();
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
