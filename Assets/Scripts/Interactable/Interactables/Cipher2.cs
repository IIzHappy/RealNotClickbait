using UnityEngine;

public class Cipher2 : Interactable
{
    Item item;
    private void Start()
    {
        _isItem = true;
    }
    public override void Interacted()
    {
        item = gameObject.GetComponent<Interactable>()._item;
        StoryController.Instance.Cipher2();
        Inventory.Instance.AddItem(item);
        gameObject.SetActive(false);
    }
}
