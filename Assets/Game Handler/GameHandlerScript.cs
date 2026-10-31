using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameHandlerScript : MonoBehaviour
{
    public bool Player1Turn;
    public bool Player2Turn;

    private List<Rigidbody2D> puckRigidbodies = new List<Rigidbody2D>();

    private bool isCheckingMovement = false;

    [Header("Turn Results")]
    public bool scoredOwnPuckThisTurn = false;
    public bool fouledThisTurn = false;


    [Header("Scores")]
    public int player1Score;
    public int player2Score;

    public bool P1hasQueen = false;
    public bool P2hasQueen = false;

    [Header("Turn Count")]
    [SerializeField] TextMeshProUGUI turnCounter;
    public int currentTurn;
    
    [Header("Nothing")]
    public GameObject bigPuckSpawn;
    private GameObject spawnedGroupInstance;

    [Header("Player Healths")]
    public GameObject P1Health;
    public GameObject P2Health;



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

        spawnedGroupInstance = GameObject.Find("PuckBigSpawn");

        currentTurn = 1;
        turnCounter.text = currentTurn.ToString();
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
        CheckWinOrNextTurn();

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
        CheckWinOrNextTurn();

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


    public bool AreAllPucksGone(PuckType targetType) {
        PuckData[] allPucks = FindObjectsByType<PuckData>(FindObjectsSortMode.None);

        foreach (PuckData puck in allPucks) {
            // If we find even one puck matching the target side, they are NOT all gone
            if (puck.puckType == targetType)
            {
                return false;
            }
        }

        return true;
    }



    public void CheckWinOrNextTurn() {
        QueenResetPerRound();

        // turns
        if (AreAllPucksGone(PuckType.White))
        {
            print("All White pucks are cleared! Restarting");

            HealthBarCode healthP2 = P2Health.GetComponent<HealthBarCode>();
            healthP2.takeDamage(1f * player2Score);

            ResetPucks();
        }
        else if (AreAllPucksGone(PuckType.Black))
        {
            print("All Black pucks are cleared! Restarting");
            
            HealthBarCode healthP1 = P1Health.GetComponent<HealthBarCode>();
            healthP1.takeDamage(1f * player1Score);

            ResetPucks();
        }
        else 
        {
            currentTurn += 1;
            turnCounter.text = currentTurn.ToString();
        }
    }


    public void ResetPucks() {

        P1hasQueen = false;
        P2hasQueen = false;

        if (spawnedGroupInstance != null) {
            Destroy(spawnedGroupInstance);
        }

        spawnedGroupInstance = Instantiate(bigPuckSpawn, new Vector3(-0.32638f, -0.06313f, 0f), Quaternion.identity);
    }


    public void QueenResetPerRound() {

        TableHoles holes = GetComponent<TableHoles>();
        if (holes != null && !scoredOwnPuckThisTurn)
        {
            P1hasQueen = false;
            P2hasQueen = false;

            holes.RespawnQueen();
        }

    }


}