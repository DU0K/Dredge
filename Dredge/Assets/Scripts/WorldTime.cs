using UnityEngine;
using TMPro;

public class WorldTime : MonoBehaviour
{
    [SerializeField] private RectTransform dayAndNightChart;
    [SerializeField] private TMP_Text time;

    [SerializeField] private float timeInMinutes = 720f;

    private void Update()
    {
        timeInMinutes = (timeInMinutes + Time.deltaTime) % 1440f;

        int hours = Mathf.FloorToInt(timeInMinutes / 60f);
        int minutes = Mathf.FloorToInt(timeInMinutes % 60f);

        time.text = $"{hours:00}:{minutes:00}";

        float rotation = ((hours - 12) * 15f) + (minutes * 0.25f);

        dayAndNightChart.localEulerAngles = new Vector3(0f, 0f, -rotation);
    }
}
