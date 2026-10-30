using UnityEngine;
using System.Collections;

public class HelperScripts : MonoBehaviour
{
    public static IEnumerator delay(float seconds, System.Action action) { 
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }

}
