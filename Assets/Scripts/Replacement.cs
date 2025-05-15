using UnityEngine;

public class Replacement : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject warpEnterClosed = GameObject.FindWithTag("WarpEnterClosed");
        GameObject warpEnterOpen = GameObject.FindWithTag("WarpEnterOpen");

        GameSetup collidingGameSetup = other.GetComponentInParent<GameSetup>();
        if (other.CompareTag("Player") && collidingGameSetup._keys >= 3)
        {
            if (warpEnterClosed)
            {
                warpEnterClosed.SetActive(false);
                warpEnterOpen.SetActive(true);
            }
        }
    }
}
