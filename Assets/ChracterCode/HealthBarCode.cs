using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class HealthBarCode : MonoBehaviour
{

    private bool dead = false;
    private float maxHealth = 10;
    private float currentHealth = 10;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeBarSlider;
    [SerializeField] private bool player1;


    [Header("Player Deaths")]
    public static bool player1Dead;
    public static bool player2Dead;


    // animation part
    private RectTransform mainRectTransform;
    private RectTransform easeBarRectTransform;
    private Vector2 originalPosition;
    private Coroutine shakeCoroutine;
    [SerializeField] private float easeDelayTimer = 1f;
    private float saveDelaytime;

    void Awake()
    {
        mainRectTransform = healthSlider.GetComponent<RectTransform>();
        if (easeBarSlider != null)
        {
            easeBarRectTransform = easeBarSlider.GetComponent<RectTransform>();
        }

        originalPosition = mainRectTransform.anchoredPosition;
        saveDelaytime = easeDelayTimer;
    }
    void Start()
    {
        if (healthSlider == null) {
            healthSlider = GetComponent<Slider>();
        }

        currentHealth = maxHealth;

        if (healthSlider != null) {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (easeBarSlider != null) {
            easeBarSlider.maxValue = maxHealth;
            easeBarSlider.value = currentHealth;
        }

        updateHP();
    }

    void Update()
    {
        // testing
        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame) {
            print("I key was pressed via New Input System!");
            takeDamage(1f);
        }


        // ease bar
        if (easeDelayTimer > 0f) {
            easeDelayTimer -= Time.deltaTime;
        }
        else {
            if (easeBarSlider != null && easeBarSlider.value > currentHealth) {
                easeBarSlider.value = Mathf.MoveTowards(easeBarSlider.value, currentHealth, Time.deltaTime * 15f);
            }
        }
    }

    
    public void takeDamage(float dmgAmt) {

        if (dead) return;

        currentHealth -= dmgAmt;
        currentHealth = Mathf.Max(currentHealth, 0);

        easeDelayTimer = saveDelaytime;

        updateHP();

        if (shakeCoroutine != null) {
            StopCoroutine(shakeCoroutine);
        }
        
        shakeCoroutine = StartCoroutine(ShakeRoutine(0.15f, 8f));

        if (currentHealth <= 0) {
            dead = true;

            if (player1) {
                player1Dead = true;
            }
            else if (!player1) {
                player2Dead = true;
            }

                currentHealth = 0;
            updateHP();
        }
    }

    public void healHealth(float healAmt) { 
        
        if (dead) return;

        currentHealth += healAmt;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        updateHP();

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            updateHP();
        }

    }

    private float targetHealth;
    private void updateHP() {

        if (healthSlider != null) {
            healthSlider.value = currentHealth;
        }


        if (easeBarSlider != null && currentHealth >= easeBarSlider.value) {
            easeBarSlider.value = currentHealth;
        }
    }


    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float xOffset = Random.Range(-1f, 1f) * magnitude;
            float yOffset = Random.Range(-1f, 1f) * magnitude;
            Vector2 shakeOffset = new Vector2(xOffset, yOffset);

            mainRectTransform.anchoredPosition = originalPosition + shakeOffset;

            // Make the ease bar directly copy the main health bar's exact position plus the shake offset
            if (easeBarRectTransform != null) {
                easeBarRectTransform.anchoredPosition = originalPosition + shakeOffset;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap both back
        mainRectTransform.anchoredPosition = originalPosition;
        if (easeBarRectTransform != null) {
            easeBarRectTransform.anchoredPosition = originalPosition;
        }
    }

}
