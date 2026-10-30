using UnityEngine;
using System.Collections.Generic;

public class GameHandlerScript : MonoBehaviour
{
    public bool Player1Turn;
    public bool Player2Turn;

    private List<Rigidbody2D> puckRigidbodies = new List<Rigidbody2D>();
    
    private bool isCheckingMovement = false;

    [Header("Turn Results")]
    public bool scoredOwnPuckThisTurn = false;
    public bool fouledThisTurn = false;

    public int player1Score;
    public int player2Score;

    void Start()
    {
        Player1Turn = true;
        Player2Turn = false;
        FindFirstObjectByType<StrikerPuckSpawn>()?.SpawnPlayer1Striker();


        GameObject[] puckObjects = GameObject.FindGameObjectsWithTag("Puck");
        
        foreach (GameObject puckObj in puckObjects) {
            Rigidbody2D rb = puckObj.GetComponent<Rigidbody2D>();
            if (rb != null) {
                puckRigidbodies.Add(rb);
            }
        }
    }


    void Update() {
        if (isCheckingMovement) {
            if (AreAllPucksStopped())
            {
                print("All pucks stopped. Switching player.");
                isCheckingMovement = false;
                if (scoredOwnPuckThisTurn && !fouledThisTurn) {
                    StillSamePlayer();
                }
                else {
                    NextPlayer();
                }
            }
        }
    }

    public void StartMovementCheck() {
        isCheckingMovement = true;
        scoredOwnPuckThisTurn = false;
        fouledThisTurn = false;
    }

    private bool AreAllPucksStopped() {

        Rigidbody2D[] allBodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);

        foreach (Rigidbody2D rb in allBodies) {
            if (rb != null) {
                if (rb.linearVelocity.magnitude > 0.08f) {
                    return false;
                }
            }
        } return true;
    }

    public void NextPlayer() {
        if (Player1Turn)
        {
            Player1Turn = false;
            Player2Turn = true;
            print("Turn switched to Player 2");
            FindFirstObjectByType<StrikerPuckSpawn>()?.SpawnPlayer2Striker();

        }
        else if (Player2Turn)
        {
            Player1Turn = true;
            Player2Turn = false;
            print("Turn switched to Player 1");
            FindFirstObjectByType<StrikerPuckSpawn>()?.SpawnPlayer1Striker();
        }
    }


    public void StillSamePlayer() {
        if (Player1Turn)
        {
            Player1Turn = true;
            Player2Turn = false;
            print("Player 1 landed his piece its still his turn!");
            FindFirstObjectByType<StrikerPuckSpawn>()?.SpawnPlayer1Striker();

        }
        else if (Player2Turn)
        {
            Player1Turn = false;
            Player2Turn = true;
            print("Player 2 landed his piece its still his turn!");
            FindFirstObjectByType<StrikerPuckSpawn>()?.SpawnPlayer2Striker();
        }
    }





    public void RegisterNewPuck(Rigidbody2D newRb) {
        if (!puckRigidbodies.Contains(newRb)) {
            puckRigidbodies.Add(newRb);
        }
    }

}