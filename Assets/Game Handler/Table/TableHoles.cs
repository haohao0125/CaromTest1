using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHoles : MonoBehaviour
{
    private Dictionary<Collider2D, Coroutine> activeTimers = new Dictionary<Collider2D, Coroutine>();

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
                    if (handler.Player1Turn) {
                        handler.scoredOwnPuckThisTurn = true;
                    }
                }
                else if (puckInfo.puckType == PuckType.Black) {
                    if (handler.Player2Turn) {
                        handler.scoredOwnPuckThisTurn = true;
                    }
                }



                else if (puckInfo.puckType == PuckType.Striker) {
                    handler.fouledThisTurn = true; // Striker went in
                }
            }


            if (puckInfo.puckType == PuckType.White) {
                handler.player1Score += 1;
            }
            else if (puckInfo.puckType == PuckType.Black) {
                handler.player2Score += 1;
            }
            
            Destroy(puckCollider.gameObject);
        }

        activeTimers.Remove(puckCollider);
    }
}