using UnityEngine;
using System.Collections;

public class FishGUI : InputManager
{
    [SerializeField] private RectTransform fishInterface;
    [SerializeField] private RectTransform inventory;

    [SerializeField] private float speed = 5;

    private bool GUIIsActive = false;

    private void Update()
    {
        if (fish.WasPressedThisFrame())
        {
            SwitchActionMap();
            switchGUI();
        }
    }

    public void SwitchActionMap()
    {
        if (GUIInputActionMap.enabled)
        {
            boatInputActionMap.Enable();
        }
        else
        {
            boatInputActionMap.Disable();
        }
    }

    public void switchGUI()
    {
        if (GUIIsActive == true)
        {
            GUIIsActive = false;
            StartCoroutine(FoldUp(fishInterface, 1));
            StartCoroutine(FoldUp(inventory, -1));
        }
        else
        {
            GUIIsActive = true;
            StartCoroutine(FoldUp(fishInterface, -1));
            StartCoroutine(FoldUp(inventory, 1));
        }
    }

    private IEnumerator FoldUp(RectTransform _ui, int richting)
    {
        float startX = _ui.anchoredPosition.x;
        float targetX = startX + (_ui.rect.width * richting);
        while (!Mathf.Approximately(_ui.anchoredPosition.x, targetX))
        {
            float newX = Mathf.MoveTowards(_ui.anchoredPosition.x, targetX, speed * Time.deltaTime);
            _ui.anchoredPosition = new Vector2(newX, _ui.anchoredPosition.y);
            yield return null;
        }
        _ui.anchoredPosition = new Vector2(targetX, _ui.anchoredPosition.y);
    }
}
