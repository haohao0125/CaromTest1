using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetsGOUI : MonoBehaviour
{
    private RectTransform rectTransform;

    [Header("Button Positions")]
    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float slideSpeed = 10f;

    [Header("Black Cover Panel")]
    [SerializeField] private RectTransform blackCoverPanel;
    [SerializeField] private Vector3 coverTargetPosition;
    [SerializeField] private float transitionSpeed = 10f;

    private bool isTransitioning = false;
    private bool buttonClicked = false;
    private float delayTimer = 0f;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (buttonClicked && !isTransitioning) {
            delayTimer += Time.deltaTime;

            if (delayTimer >= 0.5f) {
                isTransitioning = true;
            }
            return;
        }

        if (isTransitioning) {
            blackCoverPanel.anchoredPosition = Vector3.Lerp(
                blackCoverPanel.anchoredPosition,
                coverTargetPosition,
                Time.deltaTime * slideSpeed
            );

            if (Vector3.Distance(blackCoverPanel.anchoredPosition, coverTargetPosition) < 0.1f) {
                blackCoverPanel.anchoredPosition = coverTargetPosition;
                SceneManager.LoadScene(1);
            }
            return;
        }


        bool bothReady = CharacterSelectUI.player1Selected && CharacterSelectUI.player2Selected;
        Vector3 destination = bothReady ? targetPosition : hiddenPosition;

        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            destination,
            Time.deltaTime * slideSpeed
        );
    }

    public void lesgo() {

        if (!buttonClicked) {
            buttonClicked = true;
            delayTimer = 0f;
        }
    }
}
