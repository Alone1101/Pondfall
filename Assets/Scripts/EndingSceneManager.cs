using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EndingSceneManager : MonoBehaviour
{
    [System.Serializable]
    public class EndingData
    {
        public string endingType;
        public Sprite backgroundImage;
        public string endingPhrase;
        public AudioClip endingSound;
    }
    
    [Header("Ending Configuration")]
    public List<EndingData> endings = new List<EndingData>();
    
    [Header("UI References")]
    public Image backgroundImage;
    public TextMeshProUGUI endingPhraseText;
    public TextMeshProUGUI daysSurvivedText;
    
    [Header("Timing")]
    public float displayTime = 10f;
    
    void Start()
    {
        string endingType = PlayerPrefs.GetString("EndingType", "Unknown");
        int daysSurvived = PlayerPrefs.GetInt("DaysSurvived", 1);
        
        EndingData currentEnding = FindEndingData(endingType);
        
        if (currentEnding != null)
        {
            backgroundImage.sprite = currentEnding.backgroundImage;
            endingPhraseText.text = currentEnding.endingPhrase;

            if (AudioManager.Instance != null && currentEnding.endingSound != null)
            {
                AudioManager.Instance.PlayEndingTrack(currentEnding.endingSound);
            }
        }
        else
        {
            endingPhraseText.text = "Game Over";
        }
        
        daysSurvivedText.text = $"Days Survived: {daysSurvived}";
        
        StartCoroutine(ReturnToMenu());
    }
    
    EndingData FindEndingData(string endingType)
    {
        foreach (EndingData ending in endings)
        {
            if (ending.endingType == endingType)
                return ending;
        }
        return null;
    }
    
    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(displayTime);
        GoToMenu();
    }
    
    public void SkipToMenu()
    {
        GoToMenu();
    }

    void GoToMenu()
    {
        ResetGameState();
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM();
        }

        SceneManager.LoadScene(0);
    }

    void ResetGameState()
    {
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ResetGame();
        }
    }
}