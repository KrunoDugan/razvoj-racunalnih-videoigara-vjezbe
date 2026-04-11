using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int score = 0;
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bonus"))
        {
            score += 10;
            Destroy(collision.gameObject);
            Debug.Log("Bonus! Score: " + score);
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            speed *= 0.5f;
            Debug.Log("Trap! Speed reduced");
        }
    }
}
