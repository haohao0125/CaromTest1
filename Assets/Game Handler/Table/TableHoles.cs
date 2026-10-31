using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHoles : MonoBehaviour
{
    private Dictionary<Collider2D, Coroutine> activeTimers = new Dictionary<Collider2D, Coroutine>();

    private bool didHeScoreAgain = false;
    [SerializeField] private GameObject QueenPuck;
    [SerializeField] private GameObject BigPuckSpawnTrans;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Puck") || other.CompareTag("Striker")) {
            if (!activeTimers.ContainsKey(other)) {
                Coroutine timer = StartCoroutine(SwallowCountdown(other));
                activeTimers.Add(other, timer);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (activeTimers.ContainsKey(other)) {
            if (activeTimers[other] != null) {
                StopCoroutine(activeTimers[other]);
            }
            
            activeTimers.Remove(other);
        }
    }


    private void getBigPuck() {
        BigPuckSpawnTrans = GameObject.Find("PuckBigSpawn");
    }


    private IEnumerator SwallowCountdown(Collider2D puckCollider)
    {
        yield return new WaitForSeconds(0.3f);

        if (puckCollider != null)
        {
            PuckData puckInfo = puckCollider.GetComponent<PuckData>();
            GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();

            if (puckInfo != null && handler != null)
            {

                // Check who is currently playing and what they potted
                if (puckInfo.puckType == PuckType.White) {
                    handler.player1Score += 1;

                    if (handler.Player1Turn) {
                        handler.scoredOwnPuckThisTurn = true;
                    }
                    HandleQueen();
                }
                else if (puckInfo.puckType == PuckType.Black) {
                    handler.player2Score += 1;
                    
                    if (handler.Player2Turn) {
                        handler.scoredOwnPuckThisTurn = true;
                    }
                    HandleQueen();
                }


                else if (puckInfo.puckType == PuckType.Queen) {
                    handler.scoredOwnPuckThisTurn = true;

                    if (handler.Player1Turn) {
                        handler.P1hasQueen = true;
                        print("Player 1 has the Queen");
                    }
                    else if (handler.Player2Turn) {
                        handler.P2hasQueen = true;
                        print("Player 2 has the Queen");
                    }
                }

                else if (puckInfo.puckType == PuckType.Striker) {
                    handler.fouledThisTurn = true; // Striker went in
                }

            }


            puckInfo.enabled = false;
            Destroy(puckCollider.gameObject);
        }
        activeTimers.Remove(puckCollider);
    }


    public void HandleQueen()
    {
        getBigPuck();

        GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();
        if (handler == null) return;

        if (handler.P1hasQueen && handler.Player1Turn) {
            if (handler.scoredOwnPuckThisTurn)
            {
                print("Player 1 covered the Queen");
                handler.player1Score += 3;
            }

            handler.P2hasQueen = false;
            handler.P1hasQueen = false;
            RespawnQueen();
        }


        if (handler.P2hasQueen && handler.Player2Turn) {
            if (handler.scoredOwnPuckThisTurn)
            {
                print("Player 2 covered the Queen");
                handler.player2Score += 3;
            }

            handler.P2hasQueen = false;
            handler.P1hasQueen = false;
            RespawnQueen();
        }
    }

    public void RespawnQueen()
    {
        getBigPuck();

        if (QueenPuck != null && BigPuckSpawnTrans != null)
        {
            GameObject newQueen = Instantiate(QueenPuck, BigPuckSpawnTrans.transform);

            Vector3 localPos = Vector3.zero;
            localPos.z = 0f;
            localPos.y = localPos.y + 0.06313f;
            localPos.x = localPos.x + 0.32638f;


            newQueen.transform.localPosition = localPos;
        }

    }

}