using UnityEngine;
using UnityEngine.UI;

public class SlideOnSelection : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField] private Vector3 hiddenPosition;
    [SerializeField] private Vector3 targetPosition;


    [SerializeField] private float slideSpeed = 10f;
    [SerializeField] private bool player1;

    [SerializeField] private Button returnButton;



    private void Start() {
        rectTransform = GetComponent<RectTransform>();

        if (returnButton != null)
        {
            returnButton.onClick.AddListener(UndoSelection);
        }
    }


    private void Update() {
        bool isSelected = player1 ? CharacterSelectUI.player1Selected : CharacterSelectUI.player2Selected;

        Vector3 destination = isSelected ? targetPosition : hiddenPosition;

        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            destination,
            Time.deltaTime * slideSpeed
        );
    }


    public void UndoSelection() {
        if (player1)
        {
            CharacterSelectUI.player1Selected = false;
        }
        else
        {
            CharacterSelectUI.player2Selected = false;
        }
    }

}