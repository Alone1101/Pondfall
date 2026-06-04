using UnityEngine;
using UnityEngine.UI;

public class DiscontentSlider : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        GameSceneManager.OnDiscontentChanged += UpdateSlider;

        // Sync initial value
        UpdateSlider(GameSceneManager.Instance.discontent);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameSceneManager.OnDiscontentChanged -= UpdateSlider;
    }

    private void UpdateSlider(int value)
    {
        // Prevent feedback loop
        slider.SetValueWithoutNotify(value);
    }
}
