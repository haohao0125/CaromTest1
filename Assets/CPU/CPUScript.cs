using UnityEngine;

public class CPUScript : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float minPower = 3f;
    [SerializeField] private float maxPower = 8f;

    private Rigidbody2D strikerRb;

    private void Start()
    {
        strikerRb = GetComponent<Rigidbody2D>();
    }

    public void ExecuteAIShot()
    {
        if (strikerRb == null) return;
        Vector2 baseDirection = Vector2.down;
        
        float randomAngle = Random.Range(-60f, 60f);
        float shotPower = Random.Range(minPower, maxPower);
        
        Vector2 shotDirection = (Vector2)(Quaternion.Euler(0, 0, randomAngle) * baseDirection);
        strikerRb.AddForce(shotDirection * shotPower, ForceMode2D.Impulse);
        
        // set object's layer to collide
        gameObject.layer = LayerMask.NameToLayer("Puck");

        //print("AI Fired shot at angle: " + randomAngle + " with power: " + shotPower);
        FindFirstObjectByType<GameHandlerScript>()?.StartMovementCheck();


    }

    public void TriggerCPUTurn() {

        StartCoroutine(HelperScripts.delay(1f, () =>
        {
            ExecuteAIShot();
        }));
    
    }

}