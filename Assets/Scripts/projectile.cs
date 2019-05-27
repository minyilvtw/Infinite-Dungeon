using UnityEngine;

public class projectile : MonoBehaviour {

    public bool moveDirectionLeft = false;
    public float damage;
    public float moveSpeed;
    public float destroyTime;
    public bool destoryOnHit;

    private Vector2 Direction;

    void Awake() {
        Destroy(this.gameObject, destroyTime);
    }

    void Update()
    {
        if (!moveDirectionLeft)
        {

            transform.position += new Vector3(moveSpeed * Time.deltaTime,0,0);
        }
        else
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!collision.GetComponent<PlayerAnimation>().playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dodge")) {
                collision.GetComponent<PlayerStatus>().TakeDamage(damage);
                if (destoryOnHit)
                {
                    Destroy(this.gameObject);
                }
            }
            
        }
        else if (collision.gameObject.tag == "Wall")
        {
            if (destoryOnHit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
