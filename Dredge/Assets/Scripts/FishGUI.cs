using UnityEngine;
using System.Collections;

public class FishGUI : InputManager
{
    [SerializeField] private Transform canvasParent;
    [SerializeField] private GameObject fishInterfacePreFab;
    GameObject fishInterfaceIntstance;
    [SerializeField] private RectTransform inventory;

    [SerializeField] private float speed = 5;

    private bool GUIIsActive = false;

    private Coroutine guiTransitionRoutine;

    private void Update()
    {
        if (fish.WasPressedThisFrame())
        {
            SwitchActionMap();

            if (!GUIIsActive)
            {
                if (fishInterfaceIntstance != null)
                {
                    Destroy(fishInterfaceIntstance);
                }
                fishInterfaceIntstance = Instantiate(fishInterfacePreFab, canvasParent, false);
                StartTransition(fishInterfaceIntstance.GetComponent<RectTransform>(), 1, -1);
                GUIIsActive = true;
            }
            else
            {
                GUIIsActive = false;
                if (fishInterfaceIntstance != null)
                {
                    StartTransition(fishInterfaceIntstance.GetComponent<RectTransform>(), -1, 1);
                }
            }
        }
    }

    private void StartTransition(RectTransform fishRect, int fishDir, int inventoryDir)
    {
        if (guiTransitionRoutine != null)
        {
            StopCoroutine(guiTransitionRoutine);
        }
        guiTransitionRoutine = StartCoroutine(RunBothTransitions(fishRect, fishDir, inventoryDir));
    }

    private IEnumerator RunBothTransitions(RectTransform fishRect, int fishDir, int inventoryDir)
    {
        StartCoroutine(switchGUI(fishRect, fishDir));
        yield return StartCoroutine(switchGUI(inventory, inventoryDir));
        if (fishDir == -1 && fishInterfaceIntstance != null)
        {
            Destroy(fishInterfaceIntstance);
            fishInterfaceIntstance = null;
        }
    }


    public void SwitchActionMap()
    {
        if (GUIInputActionMap.enabled)
        {
            GUIInputActionMap.Enable();
        }
        else
        {
            GUIInputActionMap.Disable();
        }
    }
    private IEnumerator switchGUI(RectTransform _ui, int richting)
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