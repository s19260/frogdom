using UnityEngine;
using UnityEngine.Splines;

public class CheckpointController : MonoBehaviour
{
    [SerializeField]
    public Vector3 _startingPosition = new Vector3(-10f, 3f, 0);
    private Rigidbody2D playerRigidBody;
    private Animator animator;
    
    private void Start()
    {
        playerRigidBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
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
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("checkpoint " + _startingPosition);
            Animator checkpointAnimator = collision.gameObject.GetComponent<Animator>();
            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetTrigger("openBook");
            }
            _startingPosition = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);
        }
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
