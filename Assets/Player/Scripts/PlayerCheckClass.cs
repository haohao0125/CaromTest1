using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerCheckClass : MonoBehaviour
{
    public enum PuckColorOption {
        Red,
        Purple,
        Teal,
        Gold,
        Midnight,
    }

    public static PuckColorOption Player1CharChoice;
    public static PuckColorOption Player2CharChoice;

    [SerializeField] private SpriteRenderer puckSpriteRenderer;
    [SerializeField] private bool player1;


    public void Start()
    {
        PuckColorOption activeColor = player1 ? Player1CharChoice : Player2CharChoice;
        InitializePuck(activeColor);
    }


    public void InitializePuck(PuckColorOption colorType)
    {

        // Automatically map the enum to your desired actual colors
        if (puckSpriteRenderer != null)
        {
            switch (colorType)
            {
                case PuckColorOption.Red:
                    puckSpriteRenderer.color = Color.darkRed;
                    break;
                case PuckColorOption.Purple:
                    puckSpriteRenderer.color = new Color(0.5f, 0f, 0.5f);
                    break;
                case PuckColorOption.Teal:
                    puckSpriteRenderer.color = new Color(0f, 0.5f, 0.5f);
                    break;
                case PuckColorOption.Gold:
                    puckSpriteRenderer.color = new Color(1f, 0.9f, 0.2f);
                    break;
                case PuckColorOption.Midnight:
                    puckSpriteRenderer.color = new Color(0.2f, 0.2f, 0.4f);
                    break;
            }
        }
    }

}
