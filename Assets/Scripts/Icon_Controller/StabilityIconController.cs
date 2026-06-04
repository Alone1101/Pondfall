using UnityEngine;
using UnityEngine.UI;

public class StabilityIconController : MonoBehaviour
{
    public Image Stability_Icon;
    public Sprite stability_low_icon;
    public Sprite stability_mid_icon;
    public Sprite stability_high_icon;

    private void OnEnable()
    {
        GameSceneManager.OnStabilityChanged += UpdateIcon;
    }
    
    private void OnDisable()
    {
        GameSceneManager.OnStabilityChanged -= UpdateIcon;
    }
    
    public void UpdateIcon(int stabilityValue)
    {
        if (stabilityValue <= 30)
            Stability_Icon.sprite = stability_low_icon;
        else if (stabilityValue <= 60)
            Stability_Icon.sprite = stability_mid_icon;
        else
            Stability_Icon.sprite = stability_high_icon;
    }
}