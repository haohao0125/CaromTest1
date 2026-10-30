using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI P1Score;
    [SerializeField] TextMeshProUGUI P2Score;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();

        if (handler != null)
        {
            P1Score.text = handler.player1Score.ToString();
            P2Score.text = handler.player2Score.ToString();
        }
    }
}
