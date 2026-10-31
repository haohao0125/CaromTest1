using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI P1Score;
    [SerializeField] TextMeshProUGUI P2Score;

    [SerializeField] GameObject redQueenP1;
    [SerializeField] GameObject redQueenP2;

    void Start()
    {
        
    }


    void Update() {
        
        GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();

        if (handler != null)
        {
            P1Score.text = handler.player1Score.ToString();
            P2Score.text = handler.player2Score.ToString();

            if (handler.Player1Turn) {
                P1Score.color = Color.white;
                P2Score.color = Color.grey;
            }
        
            else if (handler.Player2Turn) {
                P2Score.color = Color.white;
                P1Score.color = Color.grey;
            }



            if (handler.P1hasQueen) {
                redQueenP1.SetActive(true);
            }
            else { 
                redQueenP1.SetActive(false);
            }

            if (handler.P2hasQueen) {
                redQueenP2.SetActive(true);
            }
            else {
                redQueenP2.SetActive(false);
            }





        }
    }


    
}
