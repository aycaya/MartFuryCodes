using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    PlayerController playerController;
    Animator playerAnimator;
    bool isMoving = false;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (playerController.PlayerSpeed >= 0.1f)
        {
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving && playerController.PlayerSpeed < 0.1f)
        {
            isMoving = false;
            playerAnimator.SetBool("isRunning", false);
        }
        if (!isMoving && playerController.PlayerSpeed >= 0.1f)
        {
            isMoving = true;
            playerAnimator.SetBool("isRunning", true);
        }
    }

}
