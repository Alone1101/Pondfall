using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class KingRevealSceneManager : MonoBehaviour
{
    [Header("Background Images")]
    public GameObject logKingBG;
    public GameObject eelKingBG; 
    public GameObject heronKingBG;
    public GameObject councilBG;
    
    [Header("UI Text Elements")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI phraseText;
    public TextMeshProUGUI effectText;
    
    [Header("Timing")]
    public float kingDisplayTime = 10f;
    
    private GameObject[] allBackgrounds; // To hide all in ease
    
    void Start()
    {
        allBackgrounds = new GameObject[] {
            logKingBG, eelKingBG, heronKingBG, councilBG
        };
        
        StartCoroutine(ShowKingReveal());
    }
    
    IEnumerator ShowKingReveal()
    {
        KingType newKing = GameSceneManager.Instance.GetPendingKing();
        ShowKingDisplay(newKing);
        yield return new WaitForSeconds(kingDisplayTime);
        GameSceneManager.Instance.CompleteKingTransition(); // Return to game scene
    }
    
    void ShowKingDisplay(KingType king)
    {
        // Hide all backgrounds first
        foreach (GameObject bg in allBackgrounds)
        {
            if (bg != null) bg.SetActive(false);
        }
        
        switch (king)
        {
            case KingType.Log:
                if (logKingBG != null) logKingBG.SetActive(true);
                titleText.text = "The Log King";
                phraseText.text = "'A motionless ruler who maintains order but inspires nothing.'";
                effectText.text = "Effect: Stability +3, Discontent +5 per day";
                break;
                
            case KingType.Eel:
                if (eelKingBG != null) eelKingBG.SetActive(true);
                titleText.text = "The Eel King";
                phraseText.text = "'A slippery leader who makes frogs happy but creates disorder.'";
                effectText.text = "Effect: Stability -8, Discontent -5 per day";
                break;
                
            case KingType.Heron:
                if (heronKingBG != null) heronKingBG.SetActive(true);
                titleText.text = "The Heron King";
                phraseText.text = "'A predatory ruler who solves discontent by eliminating the discontented.'";
                effectText.text = "Effect: Discontent -10, Population -8 per day";
                break;
                
            case KingType.Council:
                if (councilBG != null) councilBG.SetActive(true);
                titleText.text = "The Frog Council";
                phraseText.text = "'The frogs rule themselves.'";
                effectText.text = "Effect: Stability ±8, Discontent ±12 per day";
                break;
        }
    }
}