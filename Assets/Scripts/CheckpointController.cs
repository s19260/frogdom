using UnityEngine;
using UnityEngine.Splines;

public class CheckpointController : MonoBehaviour
{
    [SerializeField]
    public Vector3 _startingPosition = new Vector3(-10f, 3f, 0); // extract variable into LevelManager-like script
    
    
    
    public Vector3 StartingPosition
    {
        get
        {
            return _startingPosition;
        }
        set
        {
            _startingPosition = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint") || collision.gameObject.CompareTag("SecretLevel1Teleport"))
        {
            Debug.Log("checkpoint " + _startingPosition);
            _startingPosition = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);
        }
    }

    private void ReturnToCheckpoint()
    {
        
    }
    private void Update()
    {
        if (CompareTag("Player"))
        {
            GameSetup gameSetup =  GetComponentInParent<GameSetup>();
            if (!gameSetup.IsAlive)
            {
                transform.position = _startingPosition;
                gameSetup.IsAlive = true;
                gameSetup.Health = 3;
                gameSetup.heartsContainer[0].SetActive(true);
                gameSetup.heartsContainer[1].SetActive(true);
                gameSetup.heartsContainer[2].SetActive(true);
            }
        }
    }
    
}
