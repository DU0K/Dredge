using UnityEngine;

public class CatchBar : MonoBehaviour
{
    [SerializeField] private RectTransform hook;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        RectTransform parentRect = hook.parent as RectTransform;
        float maxY = parentRect.rect.height - hook.rect.height;
        float newY = Mathf.MoveTowards(hook.anchoredPosition.y, maxY, speed * Time.deltaTime);
        hook.anchoredPosition = new Vector2(hook.anchoredPosition.x, newY);
    }
}
