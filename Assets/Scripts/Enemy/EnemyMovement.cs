using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float alertRange;
    public float attackRate;
    public float attackRange;
    public bool canMove = true;

    private bool playerInRange;

    public Animator animator;
    public SpriteRenderer sprRenderer;
    public GameObject attackZone;
    public GameObject feetPos;

    protected Vector3 targetPosition;
    protected Vector3 randomPosition;

    protected float moveSpeed;
    [HideInInspector]
    public float nextUpdate;
    protected EnemyStatus enemyStatus;


    public bool forceMove = false;


    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        moveSpeed = enemyStatus.moveSpeed;
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
  
        if (sprRenderer.flipX)
        {
            attackZone.transform.localPosition = new Vector3(-0.1f, 0, 0);
        }
        else
        {
            attackZone.transform.localPosition = new Vector3(0.1f, 0, 0);
        }
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        BehaviorControl();
    }


    public virtual void BehaviorControl()
    {

        if (enemyStatus.isAlive)
        {
            // player in range
            if (canMove
                && Vector3.Distance(this.transform.position, targetPosition) > attackRange
                && Vector3.Distance(this.transform.position, targetPosition) < alertRange)
            {
                //Case: player in detectable range, can move
                MoveToPosition(targetPosition);
            }
            else if (Vector3.Distance(this.transform.position, targetPosition) <= attackRange)
            {
                //Case: player in attack range
                animator.SetBool("Walk", false);

                if (Time.time >= nextUpdate)
                {
                    nextUpdate = Time.time + attackRate;
                    AttackPlayer();
                }
            }
            else if (canMove)
            {
                // else walk around, do nothing
                if (!forceMove && Physics2D.OverlapCircle(feetPos.transform.position, 0.1f, LayerMask.GetMask("Terrain")) == null)
                {
                    if (sprRenderer.flipX)
                    {
                        randomPosition = this.transform.position + new Vector3(1f, this.transform.position.y, 0);
                    }
                    else
                    {
                        randomPosition = this.transform.position + new Vector3(-1f, this.transform.position.y, 0);
                    }
                    forceMove = true;
                }
                else if (Physics2D.OverlapCircle(feetPos.transform.position, 0.1f, LayerMask.GetMask("Terrain")) != null)
                {
                    forceMove = false;
                    if (Time.time >= nextUpdate)
                    {
                        nextUpdate = Time.time + 1f;
                        int willMove = Random.Range(-2, 2);
                        if ((willMove * willMove) == 1)
                        {
                            randomPosition = this.transform.position + new Vector3(willMove * 4, this.transform.position.y, 0);
                        }
                    }
                }

                if (Vector3.Distance(this.transform.position, randomPosition) >= 0.1f)
                {
                    MoveToPosition(randomPosition);
                }
                else
                {
                    animator.SetBool("Walk", false);
                }
            }
        }
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        var moveDir = (targetPosition - this.transform.position).normalized;

        if (moveDir.x < 0)
        {
            sprRenderer.flipX = true;
        }
        else
        {
            sprRenderer.flipX = false;
        }
        animator.SetBool("Walk", true);
        this.transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public void AttackPlayer()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            return;
        }

        var moveDir = (targetPosition - this.transform.position).normalized;

        if (moveDir.x < 0)
        {
            sprRenderer.flipX = true;
        }
        else
        {
            sprRenderer.flipX = false;
        }

       // AudioSource.PlayClipAtPoint(GetComponent<EnemySFX>().attack, this.transform.position);
        animator.SetTrigger("Attack");
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(feetPos.transform.position, 0.2f);
    }


}
