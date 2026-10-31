using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinnerMenu : MonoBehaviour
{

    [SerializeField] private GameObject winMenu;

    [SerializeField] private RectTransform winnerBar;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private float scaleSpeed = 5f;
    [SerializeField] private float returnToMenuDelay = 2f;

    private bool shouldScaleUp = false;
    private GameHandlerScript handler;


    private void Start()
    {
        handler = FindFirstObjectByType<GameHandlerScript>();
    }
    public void showWinnerBar() {

        winMenu.gameObject.SetActive(true);

        if (winnerBar != null) {
            winnerBar.localScale = Vector3.zero;

            shouldScaleUp = true;
        }



        if (HealthBarCode.player1Dead == true)
            winnerText.text = "Ego Destroyed Player 2 Wins";

        else if (HealthBarCode.player2Dead == true)
            winnerText.text = "Ego Destroyed Player 1 Wins";

        else if (HealthBarCode.player2Dead == false && HealthBarCode.player1Dead == false)
        {
            if (handler.Player1Turn)
                winnerText.text = "Player 1 Has Forfeited";

            else if (handler.Player2Turn)
                winnerText.text = "Player 1 Has Forfeited";
        }

        else
            winnerText.text = "wtf how did you end the game early?";


        StartCoroutine(HelperScripts.delay(returnToMenuDelay, () =>
        {
            SceneManager.LoadScene(0);
        }));


    }


    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            print("testing im winning");
            showWinnerBar();
        }

        
            if (!shouldScaleUp || winnerBar == null) return;

            float newY = Mathf.Lerp(winnerBar.localScale.y, 1f, scaleSpeed * Time.deltaTime);

            winnerBar.localScale = new Vector3(1f, newY, 1f);

            if (Mathf.Abs(winnerBar.localScale.y - 1f) < 0.001f)
            {
                winnerBar.localScale = new Vector3(1f, 1f, 1f);
                shouldScaleUp = false;
            }
    }





}
