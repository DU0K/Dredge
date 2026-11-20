using UnityEngine;

public class CatchBar : MonoBehaviour
{
    [SerializeField] private RectTransform hook;
    [SerializeField] private float speed = 5f;

    private QuickTimeEvent quickTimeEvent;

    private FishGUI fishGUI;

    private float maxY;

    private void OnDisable()
    {
        quickTimeEvent.onHook -= boostHook;
    }

    private void Start()
    {
        fishGUI = FindAnyObjectByType<FishGUI>();
        quickTimeEvent = FindAnyObjectByType<QuickTimeEvent>();
        quickTimeEvent.onHook += boostHook;
        RectTransform parentRect = hook.parent as RectTransform;
        maxY = parentRect.rect.height - hook.rect.height;
    }
    private void Update()
    {
        if (hook.anchoredPosition.y >= maxY)
        {
            //Instantiate fish item to place in inventoryWorld

            fishGUI.SetGUIState(false);
        }
        else
        {
            float newY = Mathf.MoveTowards(hook.anchoredPosition.y, maxY, speed * Time.deltaTime);
            hook.anchoredPosition = new Vector2(hook.anchoredPosition.x, newY);
        }
    }

    private void boostHook()
    {
        hook.anchoredPosition = new Vector2(hook.anchoredPosition.x, hook.anchoredPosition.y + 10f);
    }
}