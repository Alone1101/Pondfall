using UnityEngine;
using UnityEngine.UI;

public class StabilitySlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        GameSceneManager.OnStabilityChanged += UpdateSlider;

        // Sync initial value
        UpdateSlider(GameSceneManager.Instance.stability);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameSceneManager.OnStabilityChanged -= UpdateSlider;
    }

    private void UpdateSlider(int value)
    {
        // Prevent feedback loop
        slider.SetValueWithoutNotify(value);
    }
}
