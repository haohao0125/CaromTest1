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
}
