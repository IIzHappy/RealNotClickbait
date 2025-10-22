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
        Inventory.Instance.AddCard();
        item = gameObject.GetComponent<Interactable>()._item;
        StoryController.Instance.Card();
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
