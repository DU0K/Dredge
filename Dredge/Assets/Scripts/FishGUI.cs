using System.Collections;
using UnityEngine;

public class FishGUI : InputManager
{
    [SerializeField] private RectTransform fishInterfaceRect;
    [SerializeField] private RectTransform inventory;
    [SerializeField] private float speed = 500f;

    private bool guiIsActive;
    private Coroutine guiTransitionRoutine;

    private float inventoryOpenX;
    private float inventoryClosedX;
    private float fishOpenX;
    private float fishClosedX;

    private void Awake()
    {
        inventoryOpenX = inventory.anchoredPosition.x;
        inventoryClosedX = inventoryOpenX + inventory.rect.width;

        fishOpenX = fishInterfaceRect.anchoredPosition.x;
        fishClosedX = fishOpenX - fishInterfaceRect.rect.width;

        fishInterfaceRect.anchoredPosition = new Vector2(fishClosedX, fishInterfaceRect.anchoredPosition.y);
        inventory.anchoredPosition = new Vector2(inventoryClosedX, inventory.anchoredPosition.y);

        SetPanelsActive(false);
    }

    private void Update()
    {
        if (!fish.WasPressedThisFrame()) return;
        ToggleGUI();
    }

    public void ToggleGUI()
    {
        SetGUIState(!guiIsActive);
    }

    public void SetGUIState(bool open)
    {
        if (guiIsActive == open) return;

        SwitchActionMap();
        guiIsActive = open;

        if (guiIsActive)
        {
            SetPanelsActive(true);
            StartTransition(fishOpenX, inventoryOpenX, false);
        }
        else
        {
            StartTransition(fishClosedX, inventoryClosedX, true);
        }
    }

    private void StartTransition(float targetFishX, float targetInventoryX, bool disableAfter)
    {
        if (guiTransitionRoutine != null)
        {
            StopCoroutine(guiTransitionRoutine);
        }
        guiTransitionRoutine = StartCoroutine(AnimatePanels(targetFishX, targetInventoryX, disableAfter));
    }

    private IEnumerator AnimatePanels(float targetFishX, float targetInventoryX, bool disableAfter)
    {
        while (!Mathf.Approximately(fishInterfaceRect.anchoredPosition.x, targetFishX) ||
               !Mathf.Approximately(inventory.anchoredPosition.x, targetInventoryX))
        {
            MovePanelX(fishInterfaceRect, targetFishX);
            MovePanelX(inventory, targetInventoryX);
            yield return null;
        }

        fishInterfaceRect.anchoredPosition = new Vector2(targetFishX, fishInterfaceRect.anchoredPosition.y);
        inventory.anchoredPosition = new Vector2(targetInventoryX, inventory.anchoredPosition.y);

        if (disableAfter)
        {
            SetPanelsActive(false);
        }

        guiTransitionRoutine = null;
    }

    private void MovePanelX(RectTransform panel, float targetX)
    {
        float newX = Mathf.MoveTowards(panel.anchoredPosition.x, targetX, speed * Time.deltaTime);
        panel.anchoredPosition = new Vector2(newX, panel.anchoredPosition.y);
    }

    private void SetPanelsActive(bool isActive)
    {
        fishInterfaceRect.gameObject.SetActive(isActive);
        inventory.gameObject.SetActive(isActive);
    }

    public void SwitchActionMap()
    {
        if (GUIInputActionMap.enabled)
        {
            GUIInputActionMap.Disable();
        }
        else
        {
            GUIInputActionMap.Enable();
        }
    }
}
