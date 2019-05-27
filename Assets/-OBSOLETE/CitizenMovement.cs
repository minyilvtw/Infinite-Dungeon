using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenMovement : MonoBehaviour {
    public bool alive = true;


    public Animator playerAnimator;
    public SpriteRenderer sprRenderer;

    private Vector3 newPosition;

    private float moveSpeed;
    private float nextUpdate;


    void Awake() {
        moveSpeed = 5f;// GetComponent<EnemyStatus>().moveSpeed;
        newPosition = this.transform.position;
    }

	void Update () {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + 1f;
            int willMove = Random.Range(-5, 5);
            if ((willMove * willMove) == 1)
            {
                newPosition = this.transform.position + new Vector3(willMove, 0, 0);
            }

        }

        var moveDir = (newPosition - this.transform.position).normalized;

        if (Vector3.Distance(this.transform.position, newPosition) <= 0.02f)
        {
            playerAnimator.SetBool("Running", false);

        }
        else
        {
            playerAnimator.SetBool("Running", true);

            if (moveDir.x < -0.1f)
            {
                sprRenderer.flipX = true;
            }
            else if (moveDir.x > 0.1f)
            {
                sprRenderer.flipX = false;
            }

        }

        this.transform.position += moveDir * moveSpeed * Time.deltaTime;    
	
	}
}
