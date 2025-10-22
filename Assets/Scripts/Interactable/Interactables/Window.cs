using UnityEngine;

public class Window : Interactable
{
    public Animator animator;
    public override void Interacted()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Open");
        _canInteract = false;
        StoryController.Instance.WindowUnlock();
    }
}
