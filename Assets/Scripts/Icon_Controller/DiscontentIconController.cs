using UnityEngine;
using UnityEngine.UI;

public class DiscontentIconController : MonoBehaviour
{
    public Image Discontent_Icon;
    public Sprite discontent_low_icon;
    public Sprite discontent_mid_icon;
    public Sprite discontent_high_icon;

    private void OnEnable()
    {
        GameSceneManager.OnDiscontentChanged += UpdateIcon;
    }
    
    private void OnDisable()
    {
        GameSceneManager.OnDiscontentChanged -= UpdateIcon;
    }
    
    public void UpdateIcon(int discontentValue)
    {
        if (discontentValue <= 30)
            Discontent_Icon.sprite = discontent_low_icon;
        else if (discontentValue <= 60)
            Discontent_Icon.sprite = discontent_mid_icon;
        else
            Discontent_Icon.sprite = discontent_high_icon;
    }
}