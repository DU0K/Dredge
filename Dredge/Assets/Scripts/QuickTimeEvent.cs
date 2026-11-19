using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : InputManager
{
    [SerializeField] private RectTransform trigger;
    [SerializeField] private GameObject target;
    [SerializeField] private int ammount = 4;
    [SerializeField] private float minZoneFill;
    [SerializeField] private float maxZoneFill;
    [SerializeField] private float speed = 5f;

    private float zoneStartAngle;
    private float zoneEndAngle;
    private float currentAngle;

    List<Vector2> targetPositions = new List<Vector2>();

    public event Action onHook;

    private void Update()
    {
        if (targetPositions.Count < ammount)
        {
            AddRandomTarget();
        }

        currentAngle = (currentAngle + speed * Time.deltaTime) % 360f;
        trigger.localEulerAngles = new Vector3(0f, 0f, -currentAngle);
        if (hook.WasPressedThisFrame() && CheckForOverlap(currentAngle, currentAngle))
        {
            onHook?.Invoke();
        }
    }

    private void AddRandomTarget()
    {
        float fill = UnityEngine.Random.Range(minZoneFill, maxZoneFill);
        float zoneArcDegrees = fill * 360f;

        zoneStartAngle = UnityEngine.Random.Range(60f, 340f - zoneArcDegrees);
        zoneEndAngle = zoneStartAngle + zoneArcDegrees;

        if (!CheckForOverlap(zoneStartAngle, zoneEndAngle))
        {
            targetPositions.Add(new Vector2(zoneStartAngle, zoneEndAngle));
            Instantiate(target, transform.position, Quaternion.Euler(0f, 0f, -zoneStartAngle), transform).GetComponent<Image>().fillAmount = fill;
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
