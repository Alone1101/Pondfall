using UnityEngine;
using UnityEngine.UI;

public class KingIconController : MonoBehaviour
{
    public Image kingIconImage;
    public Sprite noKingSprite;
    public Sprite logKingSprite;
    public Sprite eelKingSprite;
    public Sprite heronKingSprite;
    public Sprite councilSprite;
    
    private void OnEnable()
    {
        GameSceneManager.OnKingChanged += UpdateKingIcon;
    }
    
    private void OnDisable()
    {
        GameSceneManager.OnKingChanged -= UpdateKingIcon;
    }
    
    private void UpdateKingIcon(KingType newKing)
    {
        switch (newKing)
        {
            case KingType.None:
                kingIconImage.sprite = noKingSprite;
                break;
            case KingType.Log:
                kingIconImage.sprite = logKingSprite;
                break;
            case KingType.Eel:
                kingIconImage.sprite = eelKingSprite;
                break;
            case KingType.Heron:
                kingIconImage.sprite = heronKingSprite;
                break;
            case KingType.Council:
                kingIconImage.sprite = councilSprite;
                break;
        }
    }
}