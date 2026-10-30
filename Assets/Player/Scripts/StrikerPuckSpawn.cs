using UnityEngine;

public class StrikerPuckSpawn : MonoBehaviour
{
    private bool lastPlayer1TurnState;


    [Header("Striker Player Objects")]
    [SerializeField] private GameObject StrikerPlayer1;
    [SerializeField] private GameObject StrikerPlayer2;


    [Header("The Striker Spawn Loc")]
    [SerializeField] private Transform player2SpawnLoc;
    [SerializeField] private Transform player1SpawnLoc;



    void Start()
    {
        GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();
        lastPlayer1TurnState = handler.Player1Turn;   
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (GameHandlerScript.Player1Turn != lastPlayer1TurnState)
    //    {
    //        // Update our tracker so this block only runs once per change
    //        lastPlayer1TurnState = GameHandlerScript.Player1Turn;

    //        if (GameHandlerScript.Player1Turn)
    //        {

    //            SpawnPlayer1Striker();
    //        }
    //        else
    //        {
    //            print("Player 2's turn started! Spawning striker once.");
    //            SpawnPlayer2Striker();
    //        }
    //    }

    //}

    private GameObject spawnedStriker1;
    private GameObject spawnedStriker2;

    public void SpawnPlayer1Striker()
    {
        if (spawnedStriker1 != null) {
            destroyThisGuy(spawnedStriker1); }

        print("Player 1's turn");
        print("Spawning Striker on 1");
        
        spawnedStriker1 = Instantiate(StrikerPlayer1, player1SpawnLoc.position, Quaternion.identity);

        if (spawnedStriker2 != null) {
            destroyThisGuy(spawnedStriker2);
        }

        putPuckinList(spawnedStriker1);
    }


    public void SpawnPlayer2Striker()
    {
        if (spawnedStriker2 != null) {
            destroyThisGuy(spawnedStriker2); }

        print("Player 2's turn");
        print("Spawning Striker on 2");
        spawnedStriker2 = Instantiate(StrikerPlayer2, player2SpawnLoc.position, Quaternion.identity);

        if (spawnedStriker1 != null) {
            destroyThisGuy(spawnedStriker1);
        }

        StartCoroutine(HelperScripts.delay(1f, () =>  {
            FindFirstObjectByType<CPUScript>() ?.TriggerCPUTurn();
        }));

        putPuckinList(spawnedStriker2);
    }


    public void putPuckinList(GameObject strikerObj)
    {

        Rigidbody2D rb = StrikerPlayer1.GetComponent<Rigidbody2D>();
        GameHandlerScript handler = FindFirstObjectByType<GameHandlerScript>();
        if (handler != null && rb != null)
        {
            handler.RegisterNewPuck(rb); // GameHandlerScript
        }

    }



    private void destroyThisGuy(GameObject thisPuck) { 
        Destroy(thisPuck);
    }
}
