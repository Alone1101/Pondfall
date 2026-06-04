using UnityEngine;
using UnityEngine.UI;

public class FavorSlider : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();

        GameSceneManager.OnFavorChanged += UpdateSlider;

        // Sync initial value
        UpdateSlider(GameSceneManager.Instance.favor);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        GameSceneManager.OnFavorChanged -= UpdateSlider;
    }

    private void UpdateSlider(int value)
    {
        // Prevent feedback loop
        slider.SetValueWithoutNotify(value);
    }
}
