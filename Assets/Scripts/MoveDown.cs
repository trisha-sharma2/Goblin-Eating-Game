using UnityEngine;

public class MoveDown : MonoBehaviour
{
    Animator anim;

    private float speed = 5000.0f;
    private float zDestroy = 12.0f;

    private Rigidbody objectRb;

    void Awake()
    {
        objectRb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // Move the humans and goblins down
        objectRb.AddForce(Vector3.forward * -speed);

        // Destroy the gameobject (Put back into queue)
        if (transform.position.z < -zDestroy)
        {
            if (gameObject.CompareTag("Goblin"))
            {
                ObjectPooler.Instance.ReturnToPool("Goblin", gameObject);
            }

            else if (gameObject.CompareTag("Human 1"))
            {
                ObjectPooler.Instance.ReturnToPool("Human 1", gameObject);
            }

            else if (gameObject.CompareTag("Human 2"))
            {
                ObjectPooler.Instance.ReturnToPool("Human 2", gameObject);
            }
        }
    }
}
