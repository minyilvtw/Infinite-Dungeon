using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
 
   private PlayerBase pb;

   public bool canAction;

    void Awake(){
        pb = GetComponent<PlayerBase>();
    }

	void Update () {
        CheckInput();

        if (!pb.LevelManager.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (pb.LevelManager.willAdvance)
                {
                    pb.LevelManager.AdvanceLevel();
                }
                else
                {
                    pb.LevelManager.ResetLevel();
                }
            }

        }
    }

    void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            pb.PlayerMovement.Dodge();
        } else if (pb.PlayerMovement.isGrounded && !pb.PlayerAnimation.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dodge")) {
            if (Input.GetKeyDown(KeyCode.V))
            {
                pb.PlayerMovement.Attack();
            } else if (Input.GetKeyDown(KeyCode.C))
            {
                pb.PlayerMovement.HeavyAttack();
            }
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            pb.InventoryController.UseItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            pb.InventoryController.UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            pb.InventoryController.UseItem(2);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pb.PlayerMovement.Jump();
        }

    }

}
