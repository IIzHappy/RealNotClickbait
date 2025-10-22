using TMPro;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public TMP_Text _code;
    public string _answer;

    private void Update()
    {
        if (_code.text.Length >= 4)
        {
            if (_code.text == _answer)
            {

            }
            _code.text = "";
        }
    }
}
