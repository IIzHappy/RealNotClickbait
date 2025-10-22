using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public static StoryController Instance { get; private set; }

    public GameObject _door;
    public Interactable _shelfCabinet;
    public GameObject _rug;
    public GameObject _rugFlipped;
    public GameObject _keyDoor;
    public Interactable _box;
    public Interactable _keyWindow;
    public Interactable _deskDrawer;
    public Interactable _cabinetDrawer;

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
        //play player voiceline
        //make shelf interabctale
        _shelfCabinet._canInteract = true;
    }
    public void Cipher2()
    {
        //make rug interactable
        _rug.GetComponent<Interactable>()._canInteract = true;
    }
    public void RugUp()
    {
        //change rug
        _rug.SetActive(false);
        _rugFlipped.SetActive(true);
        //add key
        _keyDoor.gameObject.SetActive(true);
    }
    public void DoorUnlock()
    {
        //good ending
    }

    public void Card()
    {
        //card pickup
        //box already there
    }
    public void BoxUnlock()
    {
        //has window key
    }

    public void WindowUnlock()
    {
        //dumbass ending
    }

    public void FindBook()
    {
        //unlock cabinet drawer with journal
    }
    public void FindJournal()
    {
        //Bottom drawer unlocks that has book to summon
        _cabinetDrawer._canInteract = true;
    }

    public void SummonAlchemist()
    {
        //too ezed ending
    }

    public void LockedSound()
    {
        //need locked sound
    }
}
