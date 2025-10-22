using UnityEngine;

public class Card : Interactable
{
    Item item;
    private void Start()
    {
        _isItem = true;
    }
    public override void Interacted()
    {
        item = gameObject.GetComponent<Interactable>()._item;
        StoryController.Instance.Card();
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
