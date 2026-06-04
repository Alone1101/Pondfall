using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PrayerSceneManager : MonoBehaviour
{
    public float displayTime = 5f;
    
    void Start()
    {
        StartCoroutine(ShowPrayer());
    }
    
    IEnumerator ShowPrayer()
    {
        yield return new WaitForSeconds(displayTime);
        
        // Load king reveal scene
        SceneManager.LoadScene(5);
    }
}