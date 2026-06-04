using UnityEngine;
using UnityEngine.UI;

public class PopulationIconController : MonoBehaviour
{
    public Image Population_Icon;
    public Sprite population_low_icon;
    public Sprite population_mid_icon;
    public Sprite population_high_icon;

    private void OnEnable()
    {
        GameSceneManager.OnPopulationChanged += UpdateIcon;
    }
    
    private void OnDisable()
    {
        GameSceneManager.OnPopulationChanged -= UpdateIcon;
    }
    
    public void UpdateIcon(int populationValue)
    {
        if (populationValue <= 30)
            Population_Icon.sprite = population_low_icon;
        else if (populationValue <= 60)
            Population_Icon.sprite = population_mid_icon;
        else
            Population_Icon.sprite = population_high_icon;
    }
}