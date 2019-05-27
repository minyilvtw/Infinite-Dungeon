using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour {

    public PlayerBase pb;
    public Collider2D detector;

    private bool ContactDamage;

    public void EnableMovement() {
        pb.PlayerMovement.canMove = true;
        pb.PlayerMovement.rb2d.velocity = new Vector2(0, pb.PlayerMovement.rb2d.velocity.y);

    }

    public void DisableMovement() {

        pb.PlayerAnimation.setAnimation("Run", false);
        pb.PlayerMovement.canMove = false;
    }

    public void EnableDamageDetection()
    {
        pb.PlayerStatus.isInvincible = false;
    }
    public void DisableDamageDetection()
    {
        pb.PlayerStatus.isInvincible = true;
    }

    
    public void AttackOne() {
        float damage = pb.PlayerStatus.currentAttackDamage;

        AudioSource.PlayClipAtPoint(pb.playerSFX.attack1, this.transform.position);

        Collider2D[] cols = new Collider2D[50];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);

        foreach (Collider2D x in cols)
        {
            if (x != null) {

                if (x.tag == "Enemy")
                {
                    
                    x.GetComponent<EnemyStatus>().TakeDamage(damage, 150f, pb.PlayerAnimation.sprite.flipX, this.gameObject);
                }
            }    
        }
       
    }

    public void AttackTwo() {
        float damage = pb.PlayerStatus.currentAttackDamage;

        AudioSource.PlayClipAtPoint(pb.playerSFX.attack2, this.transform.position);


        Collider2D[] cols = new Collider2D[50];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);

        foreach (Collider2D x in cols)
        {

            if (x != null)
            {
                if (x.tag == "Enemy")
                {

                    x.GetComponent<EnemyStatus>().TakeDamage(damage, 150f, pb.PlayerAnimation.sprite.flipX, this.gameObject);
                }
            }

        }
    }

    public void AttackThree() {
        float damage = pb.PlayerStatus.currentAttackDamage * 2f;

        AudioSource.PlayClipAtPoint(pb.playerSFX.attack3, this.transform.position);


        Collider2D[] cols = new Collider2D[50];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);

        foreach (Collider2D x in cols)
        {

            if (x != null)
            {
                if (x.tag == "Enemy")
                {
                    x.GetComponent<EnemyStatus>().TakeDamage(damage, 300f, pb.PlayerAnimation.sprite.flipX, this.gameObject);
                }
            }

        }
    }

    public void AttackZeroSpecial() {
        float damage = pb.PlayerStatus.currentAttackDamage * 1.2f;

        AudioSource.PlayClipAtPoint(pb.playerSFX.attack1, this.transform.position, 10f);

        Collider2D[] cols = new Collider2D[120];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);

        foreach (Collider2D x in cols)
        {

            if (x != null)
            {
                if (x.tag == "Enemy")
                {
                    x.GetComponent<EnemyStatus>().TakeDamage(damage, 0f, pb.PlayerAnimation.sprite.flipX, this.gameObject);

                }
            }

        }
    }

    public void AttackOneSpecial()
    {
        float damage = pb.PlayerStatus.currentAttackDamage;

        AudioSource.PlayClipAtPoint(pb.playerSFX.attack2, this.transform.position, 2f);

        pb.PlayerMovement.rb2d.velocity = new Vector2(0, pb.PlayerMovement.rb2d.velocity.y);

        if (pb.PlayerAnimation.sprite.flipX)
        {
            pb.PlayerMovement.rb2d.AddForce(new Vector2(-600f, -1f));

        }
        else
        {
            pb.PlayerMovement.rb2d.AddForce(new Vector2(600f, -1f));
        }

        Collider2D[] cols = new Collider2D[100];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);

        foreach (Collider2D x in cols)
        {

            if (x != null)
            {
                if (x.tag == "Enemy")
                {
                    x.GetComponent<EnemyStatus>().TakeDamage(damage, 0f, pb.PlayerAnimation.sprite.flipX, this.gameObject);

                }
            }

        }
    }

    public void AttackTwoSpecial() {
        float damage = pb.PlayerStatus.attackDamage * 2f;

        // TEMP
        AudioSource.PlayClipAtPoint(pb.playerSFX.attack3, this.transform.position,2f);


        Collider2D[] cols = new Collider2D[145];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);

        foreach (Collider2D x in cols)
        {

            if (x != null)
            {
                if (x.tag == "Enemy")
                {
                    x.GetComponent<EnemyStatus>().TakeDamage(damage, 500f, pb.PlayerAnimation.sprite.flipX, this.gameObject);

                }
            }

        }
    }
    
    
}
