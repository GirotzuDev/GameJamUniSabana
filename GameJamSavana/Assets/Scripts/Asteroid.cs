using UnityEngine;
using System.Linq;

public class Asteroid : MonoBehaviour
{
    public GameObject pointContainer;
    private GameObject[] targetObjects;
    public float moveSpeed = 5f;

    private int currentTargetIndex = 0;
    private Transform currentTarget;
    public float damage = 30f;

    private void Start()
    {
        targetObjects = pointContainer.GetComponentsInChildren<Transform>().Where(obj => obj.CompareTag("point")).Select(obj => obj.gameObject).ToArray();

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
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                int nextTarget = Random.Range(0, targetObjects.Length);
                currentTarget = targetObjects[nextTarget].transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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

}