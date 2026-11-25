using UnityEngine;

public class CatchBar : MonoBehaviour
{
    [SerializeField] private RectTransform hook;
    [SerializeField] private float speed = 5f;
    [SerializeField] private RectTransform line;

    private QuickTimeEvent quickTimeEvent;
    private FishGUI fishGUI;

    private float maxY;
    private float minY;

    private void OnDisable()
    {
        resetHook();
        if (quickTimeEvent != null)
        {
            quickTimeEvent.onHook -= boostHook;
        }
    }

    private void Start()
    {
        fishGUI = FindAnyObjectByType<FishGUI>();
        quickTimeEvent = FindAnyObjectByType<QuickTimeEvent>();

        if (quickTimeEvent != null)
        {
            quickTimeEvent.onHook += boostHook;
        }

        RectTransform parentRect = hook.parent as RectTransform;
        maxY = parentRect.rect.yMax - (hook.rect.height * (1f - hook.pivot.y));
        minY = parentRect.rect.yMin + (hook.rect.height * hook.pivot.y);
    }

    private void Update()
    {
        if (hook.anchoredPosition.y >= maxY)
        {
            fishGUI.SetGUIState(false);
        }
        else
        {
            float newY = Mathf.MoveTowards(hook.anchoredPosition.y, maxY, speed * Time.deltaTime);
            hook.anchoredPosition = new Vector2(hook.anchoredPosition.x, newY);
        }

        float distance = line.position.y - hook.position.y;
        line.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Max(0f, distance / line.lossyScale.y));
    }

    private void boostHook()
    {
        float boostedY = Mathf.Min(hook.anchoredPosition.y + 10f, maxY);
        hook.anchoredPosition = new Vector2(hook.anchoredPosition.x, boostedY);
    }

    public void resetHook()
    {
        hook.anchoredPosition = new Vector2(hook.anchoredPosition.x, minY);
    }
}
