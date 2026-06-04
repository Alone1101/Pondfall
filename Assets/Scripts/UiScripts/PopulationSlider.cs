using UnityEngine;
using UnityEngine.UI;

public class PopulationSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        GameSceneManager.OnPopulationChanged += UpdateSlider;

        // Sync initial value
        UpdateSlider(GameSceneManager.Instance.population);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameSceneManager.OnPopulationChanged -= UpdateSlider;
    }

    private void UpdateSlider(int value)
    {
        // Prevent feedback loop
        slider.SetValueWithoutNotify(value);
    }
}
