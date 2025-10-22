using TMPro;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public TMP_Text _code;
    public string _answer;
    GameObject _safePanel;

    private void Update()
    {
        if (_code.text.Length >= 4)
        {
            if (_code.text == _answer)
            {
                StoryController.Instance.BoxUnlock();
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                _safePanel.SetActive(false);
            }
            _code.text = "";
        }
    }
}
