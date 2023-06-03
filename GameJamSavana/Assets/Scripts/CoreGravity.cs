using UnityEngine;

public class CoreGravity : MonoBehaviour
{
    protected Transform player;
    Rigidbody2D playerBody;
    protected float influenceRange;
    protected float intensity;
    protected float distanceToPlayer;
    Vector2 pullForce;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player =  GameObject.Find("Player").gameObject.GetComponent<Transform>();
        playerBody = player.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer <= influenceRange)
        {
            pullForce = (transform.position - player.position).normalized / distanceToPlayer * intensity;
            playerBody.AddForce(pullForce, ForceMode2D.Force);
        }
    }


}
