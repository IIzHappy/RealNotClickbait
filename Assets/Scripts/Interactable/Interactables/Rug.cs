using UnityEngine;

public class Rug : Interactable
{
    public override void Interacted()
    {
        StoryController.Instance.RugUp();
    }
}
