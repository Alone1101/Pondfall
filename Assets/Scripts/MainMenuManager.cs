using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager: MonoBehaviour
{
    public void OnInfoButton()
    {
        PlaySelectSound();
        SceneManager.LoadScene(2);
    }

    public void OnSettingsButton()
    {
        PlaySelectSound();
        SceneManager.LoadScene(1);
    }

    public void OnPlayButton()
    {
        PlaySelectSound();
        SceneManager.LoadScene(3);
    }

    public void OnCreditsButton()
    {
        PlaySelectSound();
        SceneManager.LoadScene(7);
    }

    public void OnQuitButton()
    {
        PlaySelectSound();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void PlaySelectSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySelectSound();
        }
        else
        {
            Debug.LogWarning("AudioManager Instance is null!");
        }
    }
}