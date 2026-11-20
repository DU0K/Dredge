using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : InputManager
{
    [SerializeField] private RectTransform trigger;
    [SerializeField] private GameObject target;

    private FishManager fishManager;
    private FishManager.QTELocation currentQTE;

    private float zoneStartAngle;
    private float zoneEndAngle;
    private float currentAngle;

    List<Vector2> targetPositions = new List<Vector2>();

    public event Action onHook;

    private void Start()
    {
        base.Start();
        fishManager = FindAnyObjectByType<FishManager>();
    }


    private void Update()
    {
        if (targetPositions.Count < fishManager._fish[0].Locations.Length)
        {
            AddRandomTarget();
        }

        currentAngle = (currentAngle + fishManager._fish[0].speed * Time.deltaTime) % 360f;
        trigger.localEulerAngles = new Vector3(0f, 0f, -currentAngle);
        if (hook.WasPressedThisFrame() && CheckForOverlap(currentAngle, currentAngle))
        {
            onHook?.Invoke();
        }
    }

    private void AddRandomTarget()
    {
        int index = targetPositions.Count;
        currentQTE = fishManager._fish[0].Locations[index];

        zoneStartAngle = currentQTE.location;
        zoneEndAngle = zoneStartAngle + currentQTE.size;

        if (!CheckForOverlap(zoneStartAngle, zoneEndAngle))
        {
            targetPositions.Add(new Vector2(zoneStartAngle, zoneEndAngle));

            Instantiate(target, transform.position, Quaternion.Euler(0f, 0f, -zoneStartAngle), transform)
                .GetComponent<Image>().fillAmount = currentQTE.size / 360f;
        }
        else
        {
            targetPositions.Add(Vector2.zero);
        }
    }


    private bool CheckForOverlap(float positionA, float positionB)
    {
        for (int i = 0; i < targetPositions.Count; i++)
        {
            float existingStartTargetAngle = targetPositions[i].x;
            float existingEndTargetAngle = targetPositions[i].y;
            if (positionA < existingEndTargetAngle && positionB > existingStartTargetAngle)
            {
                return true;
            }
        }
        return false;
    }
}