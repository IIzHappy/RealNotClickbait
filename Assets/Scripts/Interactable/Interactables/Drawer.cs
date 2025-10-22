using UnityEngine;

public class Drawer : Interactable
{
    public Animator animator;
    public override void Interacted()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Open");
        _canInteract = false;
    }
}
