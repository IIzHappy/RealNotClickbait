using UnityEngine;

public class Door : Interactable
{
    public override void Interacted()
    {
        StoryController.Instance.DoorUnlock();
    }
}
