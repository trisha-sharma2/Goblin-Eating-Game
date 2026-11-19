using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MainUIHandler mainUIHandler;

    public InputAction moveAction;

    private Rigidbody playerRb;
    private Vector2 moveInput;
    
    private AudioSource audioSource;

    private float speed = 8.0f;
    private float zBound = 8.0f;
    private float xBound = 15.0f;
    private bool hasPowerup;

    public GameObject indicator;
    public ParticleSystem[] eatingVFX;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();

        mainUIHandler = FindAnyObjectByType<MainUIHandler>();

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();

        indicator.transform.localPosition = new Vector3(0, 1, 1);
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        ConstrainPlayerPosition();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    // Move player based on WASD and Arrow key input
    void MovePlayer()
    {
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        playerRb.linearVelocity = new Vector3(
        moveDirection.x * speed,
        playerRb.linearVelocity.y,
        moveDirection.z * speed);
    }


    // Prevent the player from leaving the top/bottom and left/right of the screen
    void ConstrainPlayerPosition()
    {
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }

        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collided with humans
        if (collision.gameObject.CompareTag("Human 1"))
        {
            // If has powerup
            if (hasPowerup)
            {
                // Return human back in the queue
                ObjectPooler.Instance.ReturnToPool("Human 1", collision.gameObject);

                // Play the VFX 
                eatingVFX[1].Play();
            }

            // Game will over if collided with a human with sound
            else
            {
                audioSource.Play();
                mainUIHandler.GameOver();
            }
        }

        if (collision.gameObject.CompareTag("Human 2"))
        {
            // If has powerup
            if (hasPowerup)
            {
                // Return human back in the queue
                ObjectPooler.Instance.ReturnToPool("Human 2", collision.gameObject);

                // Play the VFX 
                eatingVFX[1].Play();
            }

            // Game will over if collided with a human with sound
            else
            {
                audioSource.Play();
                mainUIHandler.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If collided with goblin
        if (other.transform.parent.CompareTag("Goblin"))
        {
            // Update score
            mainUIHandler.UpdateScore(5);

            // Return goblin back in the queue
            ObjectPooler.Instance.ReturnToPool("Goblin", other.transform.parent.gameObject);

            // Play the VFX
            eatingVFX[0].Play();
        }

        // If collided with powerup
        if (other.transform.parent.CompareTag("Powerup"))
        {
            // Return powerup back in the queue
            ObjectPooler.Instance.ReturnToPool("Powerup", other.transform.root.gameObject);

            // Effect lasts for some seconds
            StartCoroutine(Powerup());
        }
    }

    IEnumerator Powerup()
    {
        hasPowerup = true;

        indicator.SetActive(true);
        yield return new WaitForSeconds(7);

        hasPowerup = false;
        indicator.SetActive(false);
    }
}
