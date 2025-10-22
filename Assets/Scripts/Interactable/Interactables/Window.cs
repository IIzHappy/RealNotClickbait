using UnityEngine;

public class Window : Interactable
{
    public override void Interacted()
    {
        StoryController.Instance.WindowUnlock();
    }
}
