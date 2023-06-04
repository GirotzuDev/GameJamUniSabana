using UnityEngine;

public class CoreGravity : MonoBehaviour
{
    protected Transform player;
    Rigidbody2D playerBody;
    protected float influenceRange;
    protected float distanceToPlayer;

    protected float minForce;
    protected float maxForce;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.Find("Player").transform;
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer <= influenceRange)
        {
            float normalizedDistance = Mathf.InverseLerp(0f, influenceRange, distanceToPlayer);
            float forceMagnitude = Mathf.Lerp(minForce, maxForce, normalizedDistance);

            Vector2 pullDirection = (transform.position - player.position).normalized;
            Vector2 pullForce = pullDirection * forceMagnitude;

            playerBody.AddForce(pullForce, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, influenceRange);
    }
}