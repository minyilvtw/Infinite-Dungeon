using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMovement : EnemyMovement {

    float nextDashTime = 0f;
    public bool isDashing = false;

    public override void BehaviorControl()
    {
        if (enemyStatus.isAlive)
        {
            if (canMove)
            {
                if (!isDashing)
                {
                    this.sprRenderer.color = Color.white;
                    if (Vector3.Distance(transform.position, targetPosition) > attackRange)
                    {
                        // CASE: Not in attack range
                        if (nextDashTime <= Time.time)
                        {
                            Invoke("StopDashing", 1.5f);
                            isDashing = true;
                        }
                        else
                        {
                            MoveToPosition(targetPosition);
                        }
                    }
                    else
                    {
                        animator.SetBool("Walk", false);

                        if (Time.time >= nextUpdate)
                        {
                            nextUpdate = Time.time + attackRate;
                            AttackPlayer();
                        }
                    }
                }
                else
                {
                    this.sprRenderer.color = Color.red;
                    if (sprRenderer.flipX)
                    {
                        transform.position += new Vector3(moveSpeed * -5f * Time.deltaTime, 0f, 0f);
                    }
                    else
                    {
                        transform.position += new Vector3(moveSpeed * 5f * Time.deltaTime, 0f, 0f);
                    }

                    //animator.SetBool("Dash", true);
                    // Change condition
                    if (targetPosition.x <= transform.position.x + attackRange && targetPosition.x >= transform.position.x - attackRange && targetPosition.y <= transform.position.y)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().TakeDamage(1 + (int)enemyStatus.attackDamage/20);
                    }

                }
            }
        }
    }

    void StopDashing()
    {
        isDashing = false;
        nextDashTime = Time.time + 8f;
    }
    
}
