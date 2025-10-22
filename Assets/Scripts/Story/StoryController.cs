using System.Collections;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public static StoryController Instance { get; private set; }

    public PlayerController _player;
    public GameObject _endCamera;
    public Animator _animator;

    public GameObject _gameEnd;
    public GameObject _windowEnd;
    public GameObject _doorEnd;
    public GameObject _summonEnd;
    public GameObject _window;

    public GameObject _door;
    public Interactable _shelfCabinet;
    public GameObject _rug;
    public GameObject _rugFlipped;
    public GameObject _keyDoor;
    public Interactable _box;
    public Interactable _keyWindow;
    public Interactable _deskDrawer;
    public Interactable _cabinetDrawer;

    public AudioSource _audioSource;
    public AudioClip _locked;
    public AudioClip _openDrawer;
    public AudioClip _cipher1Solve;
    public AudioClip _cipher2Solve;
    public AudioClip FindDoorKey;
    public AudioClip _card;
    public AudioClip FindWindowKey;
    public AudioClip _riddle1;
    public AudioClip _riddle2;
    public AudioClip _endDoor;
    public AudioClip _endWindow;
    public AudioClip _endSummon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartGame()
    {
        //door closes and player is stuck
        _door.GetComponent<Interactable>()._canInteract = true;
        Debug.Log("Game started");
    }
    public void Cipher1()
    {
        _audioSource.clip = _cipher1Solve;
        _audioSource.Play();
        _shelfCabinet._canInteract = true;
    }
    public void Cipher2()
    {
        _audioSource.clip = _cipher2Solve;
        _audioSource.Play();
        _rug.GetComponent<Interactable>()._canInteract = true;
    }
    public void RugUp()
    {
        //change rug
        _rug.SetActive(false);
        _rugFlipped.SetActive(true);
        _audioSource.clip = FindDoorKey;
        _audioSource.Play();
        _keyDoor.gameObject.SetActive(true);
    }
    public void DoorUnlock()
    {
        _player.gameObject.SetActive(false);
        _endCamera.SetActive(true);
        _animator.SetTrigger("Door");
        _audioSource.clip = _endDoor;
        _audioSource.Play();
        StartCoroutine(DoorOpen(0.1f));
        StartCoroutine(DoorEnd(1.7f));
    }
    IEnumerator DoorOpen(float delay)
    {
        yield return new WaitForSeconds(delay);
        _door.GetComponent<Animator>().SetTrigger("Open");
    }
    IEnumerator DoorEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        _gameEnd.gameObject.SetActive(true);
        _doorEnd.gameObject.SetActive(true);
    }

    public void Card()
    {
        _audioSource.clip = _card;
        _audioSource.Play();
    }
    public void BoxUnlock()
    {
        _audioSource.clip = FindWindowKey;
        _audioSource.Play();
    }

    public void WindowUnlock()
    {
        _player.gameObject.SetActive(false);
        _endCamera.SetActive(true);
        _animator.SetTrigger("Window");
        _audioSource.clip = _endWindow;
        _audioSource.Play();
        StartCoroutine(WindowOpen(0.2f));
        StartCoroutine(WindowEnd(1.5f));
    }
    IEnumerator WindowOpen(float delay)
    {
        yield return new WaitForSeconds(delay);
        _window.GetComponent<Animator>().SetTrigger("Open");
    }
    IEnumerator WindowEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        _gameEnd.gameObject.SetActive(true);
        _windowEnd.gameObject.SetActive(true);
    }

    public void FindBook()
    {
        _audioSource.clip = _riddle1;
        _audioSource.Play();
    }
    public void FindJournal()
    {
        _audioSource.clip = _riddle2;
        _audioSource.Play();
        _cabinetDrawer._canInteract = true;
    }

    public void SummonAlchemist()
    {
        _gameEnd.gameObject.SetActive(true);
        _summonEnd.gameObject.SetActive(true);
        _audioSource.clip = _endSummon;
        _audioSource.Play();
    }

    public void LockedSound()
    {
        _audioSource.clip = _locked;
        _audioSource.Play();
    }
    public void OpenSound()
    {
        _audioSource.clip = _openDrawer;
        _audioSource.Play();
    }
}
