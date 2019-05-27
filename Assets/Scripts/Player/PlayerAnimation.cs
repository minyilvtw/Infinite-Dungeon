using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    float cameraShakeDuration = 0.5f;
    float slowDownAmount = 1f;
    private bool isShakingCamera;
    public float cameraShakeMagnitude;

    public GameObject playerCam;
    private Vector3 cameraOriginalPosition;
    private GameObject sceneCam;

    public Animator playerAnimator; 
    public SpriteRenderer sprite;

    private PlayerBase pb;

    void Start() {
        pb = GetComponent<PlayerBase>();

        sceneCam = GameObject.Find("Main Camera");
        sceneCam.SetActive(false);
        playerCam.SetActive(true);

    }

    void Update() {

        if (isShakingCamera) {
            if (cameraShakeDuration > 0)
            {
                playerCam.transform.position = cameraOriginalPosition + (Random.insideUnitSphere * (cameraShakeMagnitude*0.01f));
                cameraShakeDuration -= Time.deltaTime * slowDownAmount;
            }
            else {
                isShakingCamera = false;
                cameraShakeDuration = 0.5f;
                playerCam.transform.position = cameraOriginalPosition;
            }
        }

        cameraOriginalPosition = this.gameObject.transform.position + new Vector3(0,1f,0);
        cameraOriginalPosition.z = -6.5f;

    }


    public void setAnimation(string animationString)
    {
        playerAnimator.SetTrigger(animationString);
    }

    public void setAnimation(string animationString, bool state)
    {
        playerAnimator.SetBool(animationString, state);
    }

    public void setAnimation(string animationString, int state)
    {
        playerAnimator.SetInteger(animationString, state);
    }

    public void flipRight()
    {
        sprite.flipX = false;
    }

    public void flipLeft()
    {
        sprite.flipX = true;
    }

    public void ShakeCamera() {
        isShakingCamera = true;
    }

}
