using UnityEngine;
using UnityEngine.UI;

public class PlayerIconController : MonoBehaviour
{
    public Image Player_Icon;
    public Sprite stability_low_icon;
    public Sprite stabiltiy_mid_icon;
    public Sprite stabiltiy_high_icon;
    
    public void UpdateIcon(int stabiltiyValue)
    {
        if (stabiltiyValue <= 30)
            Player_Icon.sprite = stability_low_icon;
        else if (stabiltiyValue <= 60)
            Player_Icon.sprite = stabiltiy_mid_icon;
        else
            Player_Icon.sprite = stabiltiy_high_icon;
    }
}