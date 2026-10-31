using UnityEngine;

public class UIIntroSlideStretchToggle : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool isLeftPanel = true; // Check for Player 1, uncheck for Player 2
    [SerializeField] private float startOffsetValue = 500f; // Where it starts hidden (off-screen offset)
    [SerializeField] private float targetValue = 0f;          // Final value when animation finishes
    [SerializeField] private float slideSpeed = 10f;
    [SerializeField] private float startDelay = 0.5f;

    private RectTransform rectTransform;
    private bool canSlide = false;
    private float targetFloat;


    void Start() {


        rectTransform = GetComponent<RectTransform>();
        targetFloat = targetValue;


        if (isLeftPanel) {
            Vector2 currentOffset = rectTransform.offsetMin;
            rectTransform.offsetMin = new Vector2(startOffsetValue, currentOffset.y);
        }
        else {
            Vector2 currentOffset = rectTransform.offsetMax;
            rectTransform.offsetMax = new Vector2(-startOffsetValue, currentOffset.y);
        }

        Invoke(nameof(StartSliding), startDelay);
    }

    void StartSliding()
    {
        canSlide = true;
    }

    void Update()
    {
        if (!canSlide || rectTransform == null) return;

        if (isLeftPanel)
        {
            Vector2 currentOffset = rectTransform.offsetMin;
            float newLeft = Mathf.MoveTowards(currentOffset.x, targetFloat, slideSpeed * Time.deltaTime * 100f);
            rectTransform.offsetMin = new Vector2(newLeft, currentOffset.y);

            if (Mathf.Abs(rectTransform.offsetMin.x - targetFloat) < 0.1f)
            {
                rectTransform.offsetMin = new Vector2(targetFloat, currentOffset.y);
                Destroy(this);
            }
        }
        else
        {
            Vector2 currentOffset = rectTransform.offsetMax;
            float targetRightOffset = -targetFloat;
            float newRight = Mathf.MoveTowards(currentOffset.x, targetRightOffset, slideSpeed * Time.deltaTime * 100f);
            rectTransform.offsetMax = new Vector2(newRight, currentOffset.y);

            if (Mathf.Abs(rectTransform.offsetMax.x - targetRightOffset) < 0.1f)
            {
                rectTransform.offsetMax = new Vector2(targetRightOffset, currentOffset.y);
                Destroy(this);
            }
        }
    }
}