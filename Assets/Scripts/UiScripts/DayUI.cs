using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    private TMP_Text dayText;
    private void Start()
    {
        dayText = GetComponent<TMP_Text>();
        GameSceneManager.OnDayChanged += UpdateDay;
        UpdateDay(GameSceneManager.Instance.currentDay); // force sync
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameSceneManager.OnDayChanged -= UpdateDay;
    }

    private void UpdateDay(int day)
    {
        dayText.text = $"Day {day}";
    }
}
