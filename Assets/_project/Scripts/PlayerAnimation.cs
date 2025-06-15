using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    [SerializeField] private Animator playerAnimator;

    
    [SerializeField] private string idleFront = "Player_Idle_Front"; 
    [SerializeField] private string idleBack = "Player_Idle_Back";   
    [SerializeField] private string idleLeft = "Player_Idle_Left";   
    [SerializeField] private string idleRight = "Player_Idle_Right"; 

    [SerializeField] private string walkDown = "Player_Walk_Down";   
    [SerializeField] private string walkUp = "Player_Walk_Up";       
    [SerializeField] private string walkLeft = "Player_Walk_Left";   
    [SerializeField] private string walkRight = "Player_Walk_Right"; 

    private Vector2 lastDirection = new Vector2(0, -1); // Default: guarda in basso (Front)

    private void Awake()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }

        if (playerAnimator == null)
        {
            Debug.LogError("PlayerAnimation: L'Animator non è stato trovato sul GameObject. Assicurati che sia presente!");
        }
    }

    public void UpdateAnimations(bool isMoving, Vector2 moveDirection)
    {
        if (playerAnimator == null) return;

        string animationToPlay = ""; 

        if (isMoving)
        {
            moveDirection.Normalize();

            
            lastDirection = moveDirection;

            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
            {
                if (moveDirection.x > 0)
                {
                    animationToPlay = walkRight; 
                }
                else
                {
                    animationToPlay = walkLeft;  
                }
            }
            else
            {
                if (moveDirection.y > 0)
                {
                    animationToPlay = walkUp;
                }
                else
                {
                    animationToPlay = walkDown;
                }
            }
        }
        else 
        {
            if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
            {
                if (lastDirection.x > 0)
                {
                    animationToPlay = idleRight; 
                }
                else
                {
                    animationToPlay = idleLeft;  
                }
            }
            else
            {
                if (lastDirection.y > 0)
                {
                    animationToPlay = idleBack;
                }
                else
                {
                    animationToPlay = idleFront;
                }
            }
        }

        if (!string.IsNullOrEmpty(animationToPlay) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationToPlay))
        {
            playerAnimator.Play(animationToPlay);
        }
    }
}



