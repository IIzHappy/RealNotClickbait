using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StoryController.Instance.StartGame();
            Destroy(gameObject);
        }
    }
}
