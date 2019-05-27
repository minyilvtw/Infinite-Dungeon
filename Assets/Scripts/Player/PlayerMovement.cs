using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour {

    [Header("General Booleans")]
    public bool inAction;
    public bool canMove = true;

    public bool isGrounded;
    public int jumpAvailable = 2;

    public bool isGrabbingWall = false;

    public int attackCount = 0;

    [Space]
    public GameObject attackZone;

    [Space]
    private float moveSpeed;
    private float jumpForce;

    public Transform feetPos;

    [HideInInspector]
    public Rigidbody2D rb2d;
    private PlayerBase pb;


    void Start()
    {
        pb = GetComponent<PlayerBase>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Update()       
    {

        DetectTerrain();
        GrabWall();

        moveSpeed = pb.PlayerStatus.currentMoveSpeed;
        jumpForce = pb.PlayerStatus.currentJumpForce;

        if (pb.PlayerAnimation.sprite.flipX)
        {
            attackZone.transform.localRotation = new Quaternion(0, 180f, 0f, 0);
        }
        else
        {
            attackZone.transform.localRotation = new Quaternion(0, 0, 0f, 0);
        }
 
        inAction = pb.PlayerAnimation.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Action");

        if (Time.time > lastAttack + comboGap)
        {
            attackCount = 0;
            pb.PlayerAnimation.setAnimation("Attack", 0);
        }

        if (rb2d.velocity.y < -0.1f && !isGrounded && !isGrabbingWall && !inAction)
        {
            pb.PlayerAnimation.setAnimation("Falling", true);
        }
        else 
        {
            pb.PlayerAnimation.setAnimation("Falling", false);
        }

        UpdateSelfPosition();
    }

    private void UpdateSelfPosition()
    {
        if (canMove && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {

            pb.PlayerAnimation.setAnimation("Attack", 0);

            var move = new Vector2(Input.GetAxis("Horizontal"), 0);

            if (move.x == 0f || !isGrounded)
            {
                pb.PlayerAnimation.setAnimation("Run", false);
            }
            else
            {
                pb.PlayerAnimation.setAnimation("Run", true);
            }

            if (move.x > 0.01f)
            {
                pb.PlayerAnimation.flipRight();
            }
            else if (move.x < -0.01f)
            {
                pb.PlayerAnimation.flipLeft();
            }

            rb2d.position += move * moveSpeed * Time.deltaTime;
        }
        else {
            pb.PlayerAnimation.setAnimation("Run", false);
        }
    }

    public void Jump()
    {
        if (canMove && isGrabbingWall && !isGrounded && jumpAvailable!=0)
        {
            // CASE: on wall
            isGrabbingWall = false;

            pb.PlayerAnimation.setAnimation("Run", false);
            pb.PlayerAnimation.setAnimation("Jump");

            rb2d.velocity = new Vector2(0, 0);
            jumpAvailable--;  // jumpAvailable = 1; optional extra jump

            
            if (pb.PlayerAnimation.sprite.flipX)
            {
                Vector2 direction = new Vector2(300f, 700f);
                rb2d.AddForce(direction);

            }
            else {
                Vector2 direction = new Vector2(-300f, 700f);
                rb2d.AddForce(direction);
            }
            pb.PlayerAnimation.sprite.flipX = !pb.PlayerAnimation.sprite.flipX;

        } else if (jumpAvailable != 0 && !inAction)
        {
            if (jumpAvailable == 2 && isGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(Vector2.up * jumpForce);
                jumpAvailable--;
            }
            else
            {
                rb2d.velocity= new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(Vector2.up * jumpForce);
                jumpAvailable --;
            }

            pb.PlayerAnimation.setAnimation("Run", false);
            pb.PlayerAnimation.setAnimation("Jump");
           
        }

    }

    private float lastAttack;
    private float attackCooldown = 0.35f;
    private float comboGap = 0.9f;
    
    public void Attack() {
            if (Time.time >= lastAttack + attackCooldown)
            {
                //canMove = false;
                pb.PlayerAnimation.setAnimation("Run", false);

                lastAttack = Time.time;
                attackCount++;

                pb.PlayerAnimation.setAnimation("Attack", attackCount);

                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                if (pb.PlayerAnimation.sprite.flipX)
                {
                    rb2d.AddForce(new Vector2(-100f, -1f));

                }
                else
                {
                    rb2d.AddForce(new Vector2(100f, -1f));
                }

                // Third hit charges forward
                if (attackCount == 3)
                {
                    if (pb.PlayerAnimation.sprite.flipX)
                    {
                        rb2d.AddForce(new Vector2(-200f, -1f));

                    }
                    else
                    {
                        rb2d.AddForce(new Vector2(200f, -1f));
                    }
                    lastAttack = Time.time + 0.2f;
                    attackCount = 0;

                }


            }

    }

    public void HeavyAttack() {

        if (Time.time >= lastAttack + attackCooldown && pb.PlayerStatus.mana >= 3f)
        {
            pb.PlayerStatus.mana -= 3f;
            if (attackCount == 0 )
            {
                lastAttack = Time.time;

                pb.PlayerAnimation.setAnimation("Attack", 4);

                attackCount = 0;


            }
            else if (attackCount == 1)
            {
                lastAttack = Time.time;


                pb.PlayerAnimation.setAnimation("Attack", 5);
                attackCount = 0;

            }
            else if (attackCount == 2)
            {
                lastAttack = Time.time + 0.5f;


                pb.PlayerAnimation.setAnimation("Attack", 6);
               
                attackCount = 0;

            }


        }


    }

    private float lastDodge;
    private float dodgeCoolDown = 1.5f;
    public void Dodge() {

        if (Time.time >= lastDodge + dodgeCoolDown && !isGrabbingWall)
        {
            lastDodge = Time.time;

            attackCount = 0;
            pb.PlayerAnimation.setAnimation("Attack", attackCount);
            pb.PlayerAnimation.setAnimation("Run", false);

            pb.PlayerAnimation.setAnimation("Dodge");

            float dodgeDistance = 500f;

            if(!isGrounded || isGrabbingWall){
                dodgeDistance = 300f;
            }

            rb2d.velocity.Set(0f, rb2d.velocity.y);
            if (pb.PlayerAnimation.sprite.flipX)
            {
                rb2d.AddForce(new Vector2(-dodgeDistance, 0));

            }
            else
            {
                rb2d.AddForce(new Vector2(dodgeDistance, 0));
            }

        }
 

    }


    private void DetectTerrain() {
        Collider2D groundCollider = Physics2D.OverlapCircle(feetPos.position, 0.2f, LayerMask.GetMask("Terrain"));

        if (groundCollider != null) {
            if (groundCollider.gameObject.tag == "Ground")
            {
                pb.PlayerAnimation.setAnimation("Falling", false);
                isGrounded = true;
                jumpAvailable = 2;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            jumpAvailable = 1;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isGrabbingWall = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Wall")
        {
            isGrabbingWall = false;
            pb.PlayerAnimation.setAnimation("WallSlide", false);
        }
    }


    public void GrabWall()
    {
        if (isGrabbingWall && !isGrounded
            && !pb.PlayerAnimation.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
            && !pb.PlayerAnimation.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Action"))
        {
            float maxDownSpeed = -0.05f;
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxDownSpeed);
            pb.PlayerAnimation.setAnimation("WallSlide", true);
        }
        else {
            pb.PlayerAnimation.setAnimation("WallSlide", false);
        }
       

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(feetPos.position, 0.2f);
    }

}
