using UnityEngine;

public class SafeInteract : Interactable
{
    public GameObject _safePanel;
    public PlayerController _playerController;
    public override void Interacted()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        _safePanel.SetActive(true);
        _playerController._canControl = false;
    }
}
