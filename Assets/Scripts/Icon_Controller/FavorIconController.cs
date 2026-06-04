using UnityEngine;
using UnityEngine.UI;

public class FavorIconController : MonoBehaviour
{
    public Image Favor_Icon;
    public Sprite favor_low_icon;
    public Sprite favor_mid_icon;
    public Sprite favor_high_icon;

    private void OnEnable()
    {
        GameSceneManager.OnFavorChanged += UpdateIcon;
    }
    
    private void OnDisable()
    {
        GameSceneManager.OnFavorChanged -= UpdateIcon;
    }
    
    public void UpdateIcon(int favorValue)
    {
        if (favorValue <= 30)
            Favor_Icon.sprite = favor_low_icon;
        else if (favorValue <= 60)
            Favor_Icon.sprite = favor_mid_icon;
        else
            Favor_Icon.sprite = favor_high_icon;
    }
}