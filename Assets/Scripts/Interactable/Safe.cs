using TMPro;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public TMP_InputField _code;
    public int _answer;
    public GameObject _safePanel;
    public PlayerController _playerController;

    private void Update()
    {
        if (_safePanel.activeSelf)
        {
            if (_code.text.Length >= 4)
            {
                if (_code.text.Trim() == _answer.ToString())
                {
                    StoryController.Instance.BoxUnlock();
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    _safePanel.SetActive(false);
                    gameObject.SetActive(false);
                    _playerController._canControl = true;
                }
                _code.text = "";
            }
        }
    }
}
