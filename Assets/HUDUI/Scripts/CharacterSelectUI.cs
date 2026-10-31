using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static PlayerCheckClass;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public class CharacterSelectUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
    private Canvas myCanvas;

    [SerializeField] private PuckColorOption thisOptionColor;

    void Start() {
        originalScale = transform.localScale;

        // Get or setup the local Canvas component
        myCanvas = GetComponent<Canvas>();
        myCanvas.overrideSorting = true;
        myCanvas.sortingOrder = 1; // Default background render order
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = targetScale;
        myCanvas.sortingOrder = 10;

    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = originalScale;
        myCanvas.sortingOrder = 1;

    }

    public static bool player1Selected = false;
    public static bool player2Selected = false;

    public void setPlayerChoice()
    {
        if (!player1Selected)
        {
            player1Selected = true;
            PlayerCheckClass.Player1CharChoice = thisOptionColor;

            print("Player 1 choice is " + Player1CharChoice);
        }

        else if (!player2Selected) 
        {
            player2Selected = true;
            PlayerCheckClass.Player2CharChoice = thisOptionColor;

            print("Player 2 choice is " + Player2CharChoice);
        }
    }

}