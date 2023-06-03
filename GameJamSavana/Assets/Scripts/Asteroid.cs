using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject[] targetObjects;
    public float moveSpeed = 5f;

    private int currentTargetIndex = 0;
    private Transform currentTarget;
    public float damage = 30f;

    private void Start()
    {
        if (targetObjects.Length > 0)
        {
            currentTarget = targetObjects[currentTargetIndex].transform;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gameStates == GameStates.gameOver || GameManager.Instance.gameStates == GameStates.gameIdle) return;
        if (currentTarget != null)
        {
            // Move towards the current target
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

            // Check if the asteroid has reached the current target
            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                // Set the next target
                int nextTarget = Random.Range(0, targetObjects.Length);
                currentTarget = targetObjects[nextTarget].transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collisions with objects tagged as "Asteroid"
        if (other.CompareTag("Asteroid"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw raycasts between the target objects
        Gizmos.color = Color.red;

        for (int i = 0; i < targetObjects.Length - 1; i++)
        {
            Vector2 startPoint = targetObjects[i].transform.position;
            Vector2 endPoint = targetObjects[i + 1].transform.position;

            Gizmos.DrawLine(startPoint, endPoint);
        }

        // Draw raycast from asteroid to current target
        if (currentTarget != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }
    }
}