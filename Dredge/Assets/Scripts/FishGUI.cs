using System.Collections;
using UnityEngine;

public class FishGUI : InputManager
{
    [SerializeField] private Transform canvasParent;
    [SerializeField] private GameObject fishInterfacePrefab;
    [SerializeField] private RectTransform inventory;
    [SerializeField] private float speed = 5f;

    private GameObject fishInterfaceInstance;
    private RectTransform fishInterfaceRect;
    private bool guiIsActive;

    private Coroutine guiTransitionRoutine;
    private Coroutine fishRoutine;
    private Coroutine inventoryRoutine;

    private void Update()
    {
        if (!fish.WasPressedThisFrame())
        {
            return;
        }

        ToggleGUI();
    }

    public void ToggleGUI()
    {
        SetGUIState(!guiIsActive);
    }

    public void SetGUIState(bool open)
    {
        if (guiIsActive == open)
        {
            return;
        }

        SwitchActionMap();
        guiIsActive = open;

        if (guiIsActive)
        {
            if (fishInterfaceInstance != null)
            {
                Destroy(fishInterfaceInstance);
            }

            fishInterfaceInstance = Instantiate(fishInterfacePrefab, canvasParent, false);
            fishInterfaceRect = fishInterfaceInstance.GetComponent<RectTransform>();
            StartTransition(fishInterfaceRect, 1, -1);
        }
        else if (fishInterfaceInstance != null)
        {
            StartTransition(fishInterfaceRect, -1, 1);
        }
    }

    private void StartTransition(RectTransform fishRect, int fishDir, int inventoryDir)
    {
        StopActiveCoroutines();
        guiTransitionRoutine = StartCoroutine(RunBothTransitions(fishRect, fishDir, inventoryDir));
    }

    private void StopActiveCoroutines()
    {
        if (guiTransitionRoutine != null) StopCoroutine(guiTransitionRoutine);
        if (fishRoutine != null) StopCoroutine(fishRoutine);
        if (inventoryRoutine != null) StopCoroutine(inventoryRoutine);
    }

    private IEnumerator RunBothTransitions(RectTransform fishRect, int fishDir, int inventoryDir)
    {
        fishRoutine = StartCoroutine(SwitchGUI(fishRect, fishDir));
        inventoryRoutine = StartCoroutine(SwitchGUI(inventory, inventoryDir));

        yield return inventoryRoutine;

        if (fishDir == -1 && fishInterfaceInstance != null)
        {
            Destroy(fishInterfaceInstance);
            fishInterfaceInstance = null;
            fishInterfaceRect = null;
        }
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

    private IEnumerator SwitchGUI(RectTransform uiElement, int richting)
    {
        if (uiElement == null) yield break;

        float startX = uiElement.anchoredPosition.x;
        float targetX = startX + (uiElement.rect.width * richting);

        while (uiElement != null && !Mathf.Approximately(uiElement.anchoredPosition.x, targetX))
        {
            float newX = Mathf.MoveTowards(uiElement.anchoredPosition.x, targetX, speed * Time.deltaTime);
            uiElement.anchoredPosition = new Vector2(newX, uiElement.anchoredPosition.y);
            yield return null;
        }

        if (uiElement != null)
        {
            uiElement.anchoredPosition = new Vector2(targetX, uiElement.anchoredPosition.y);
        }
    }
}