using UnityEngine;
using UnityEngine.UI;
using static PlayerCheckClass;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Image ImageComponent;
    [SerializeField] private bool player1;

    // Keep track of the last applied color to avoid redundant updates
    private PuckColorOption lastCheckedColor;

    private void Update()
    {
        if (ImageComponent == null) return;

        // Get the active choice live
        PuckColorOption activeColor = player1 ? Player1CharChoice : Player2CharChoice;

        // Apply color whenever the choice updates
        ApplyColor(activeColor);
    }

    private void ApplyColor(PuckColorOption colorChoice)
    {
        switch (colorChoice)
        {
            case PuckColorOption.Red:
                ImageComponent.color = Color.darkRed;
                break;
            case PuckColorOption.Purple:
                ImageComponent.color = new Color(0.5f, 0f, 0.5f);
                break;
            case PuckColorOption.Teal:
                ImageComponent.color = new Color(0f, 0.5f, 0.5f);
                break;
            case PuckColorOption.Gold:
                ImageComponent.color = new Color(0.75f, 0.6f, 0f);
                break;
            case PuckColorOption.Midnight:
                ImageComponent.color = new Color(0.15f, 0.15f, 0.2f);
                break;
        }
    }
}
