using UnityEngine;

public enum PuckType {
    White,
    Black,
    Queen,
    Striker
}

public class PuckData : MonoBehaviour {
    public PuckType puckType;
    public int scoreValue = 1;

    void Start()
    {
        if (puckType == PuckType.Queen)
        {
            foreach (PuckData puck in FindObjectsByType<PuckData>(FindObjectsSortMode.None))
            {
                if (puck != this && puck.puckType == PuckType.Queen)
                {
                    Destroy(puck.gameObject);
                    break;
                }
            }
        }


    }



}


