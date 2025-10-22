using UnityEngine;

public class SafeInteract : Interactable
{
    GameObject _safePanel;
    public override void Interacted()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        _safePanel.SetActive(true);
    }
}
