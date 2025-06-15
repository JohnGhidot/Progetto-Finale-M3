using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    
    [SerializeField] private Animator playerAnimator; // prendo il riferimento all'Animator del Player

    [SerializeField] private string idleFront = "Player_Idle_Front";
    [SerializeField] private string idleBack = "Player_Idle_Back";
    [SerializeField] private string idleLeft = "Player_Idle_Left";
    [SerializeField] private string idleRight = "Player_Idle_Right";

    [SerializeField] private string walkDown = "Player_Walk_Down";
    [SerializeField] private string walkUp = "Player_Walk_Up";     
    [SerializeField] private string walkLeft = "Player_Walk_Left";
    [SerializeField] private string walkRight = "Player_Walk_Right";


    private Vector2 lastDirection = new Vector2(0, -1); //tengo traccia dell'ultima posizione (default = front) 
    private void Awake()
    {
        // Ottengo il riferimento all'Animator se non è già stato assegnato
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
        if (playerAnimator == null)
        {
            Debug.LogError("PlayerAnimation: Animator non trovato sul GameObject. Assicurati che ci sia un componente Animator.");
        }
    }

    public void UpdateAnimations(bool isMoving, Vector2 moveDirection)
    {
        // Se non c'è un Animator valido, non fare nulla.
        if (playerAnimator == null) return;

        string animationToPlay = ""; // Qui salveremo il nome dell'animazione da riprodurre.

        if (isMoving)
        {
            moveDirection.Normalize();
            lastDirection = moveDirection;

            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
            {
                // Movimento orizzontale
                if (moveDirection.x > 0)
                {
                    animationToPlay = walkRight; // Cammina a destra
                }
                else
                {
                    animationToPlay = walkLeft;  // Cammina a sinistra
                }
            }
            else
            {
                // Movimento verticale (o se X e Y sono uguali, diamo priorità a Y)
                if (moveDirection.y > 0)
                {
                    animationToPlay = walkUp;    // Cammina in alto (Back)
                }
                else
                {
                    animationToPlay = walkDown;  // Cammina in basso (Front)
                }
            }
        }
        else // Il player è fermo (isMoving è Falso)
        {
            // Il player è fermo, quindi dobbiamo scegliere un'animazione di "Idle".
            // Usiamo l'ultima direzione in cui si è mosso per far sì che guardi in quella direzione.
            if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
            {
                // Idle orizzontale
                if (lastDirection.x > 0)
                {
                    animationToPlay = idleRight; // Idle a destra
                }
                else
                {
                    animationToPlay = idleLeft;  // Idle a sinistra
                }
            }
            else
            {
                // Idle verticale (o se X e Y sono uguali, diamo priorità a Y)
                if (lastDirection.y > 0)
                {
                    animationToPlay = idleBack;  // Idle in alto (Back)
                }
                else
                {
                    animationToPlay = idleFront; // Idle in basso (Front)
                }
            }
        }

        // Questo controlla se l'animazione che vogliamo riprodurre è già in esecuzione.
        // Se lo è, non la riproduciamo di nuovo per evitare glitch o reset dell'animazione.
        if (!string.IsNullOrEmpty(animationToPlay) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationToPlay))
        {
            playerAnimator.Play(animationToPlay); // Fa partire l'animazione con il nome scelto.
        }
    }
}



