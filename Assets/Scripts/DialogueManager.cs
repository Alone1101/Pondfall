using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Button yesButton;
    public Button noButton;
    public float textSpeed;

    private Event frogEvent;
    private Line[] lines;
    private int index;
    private bool running = false;
    private bool isAfterChoice = false;
    private bool isTransitioning = false;

    public PotraitManager potraitManager;

    void Start()
    {
        DisableButtons();
        
    }

    void Update()
    {
        // If the screen is black/transitioning, ignore all clicks
        if (isTransitioning == true) return;

        if (running && Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index].line)
            {
                NextLine();
            }
            else
            {
                // show full text
                StopAllCoroutines();
                textComponent.text = lines[index].line;
            }
        }
        
    }

    public void StartDialogue(Event frogEvent)
    {
        potraitManager.DisablePotraits();
        isAfterChoice = false;
        this.frogEvent = frogEvent;
        this.lines = frogEvent.lines;
        index = 0;
        textComponent.text = string.Empty;
        running = true;
        StartCoroutine(TypeLine());
    }

    void StartAfterChoiceDialogue()
    {
        potraitManager.DisablePotraits();
        isAfterChoice = true;
        DisableButtons();
        index = 0;
        running = true;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    void displayPotrait(Line line)
    {
        if (line.isLeftSpeaking) 
        {
            potraitManager.DisplayLeftPotrait(frogEvent.leftPotrait);
        }

        if (line.isRightSpeaking)
        {
            potraitManager.DisplayRightPotrait(frogEvent.rightPotrait);
        }

        if (!line.isLeftSpeaking && !line.isRightSpeaking)
        {
            potraitManager.DisablePotraits();
        }
    }

    IEnumerator TypeLine()
    {
        displayPotrait(lines[index]);
        foreach (char c in lines[index].line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            AudioManager.Instance.PlayContinueDialogueSound();
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if (!isAfterChoice)
        {
            running = false;
            EnableButtons();
        }
        else
        {
            running = false;
            StartDayTransition();
        }
    }

    public void StartDayTransition()
    {
        if (isTransitioning) return; // Ignore clicks if already transitioning
        StartCoroutine(DayTransitionSequence());
    }

    IEnumerator DayTransitionSequence()
    {
        isTransitioning = true;

        // 1. Fade to Black
        yield return StartCoroutine(GameSceneManager.Instance.FadeRoutine(1f));

        // 2. Apply Logic while screen is black
        GameSceneManager.Instance.EndOfDay();
        GameSceneManager.Instance.AdvanceDay();
        
        // Optional: Small delay for "feel"
        yield return new WaitForSeconds(0.5f);

        // 3. Start New Day Dialogue
        GameSceneManager.Instance.StartDay();

        // 4. Fade back in
        yield return StartCoroutine(GameSceneManager.Instance.FadeRoutine(0f));

        isTransitioning = false;
    }

    void EnableButtons()
    {
        yesButton.interactable = true;
        noButton.interactable = true;
    }

    void DisableButtons()
    {
        yesButton.interactable = false; 
        noButton.interactable = false;
    }

    public void OnYes()
    {
        Debug.Log("Button is pressed");
        AudioManager.Instance.PlaySelectSound();
        HandleStatChanges(frogEvent.yesChoice.statChanges);
        lines = frogEvent.yesChoice.afterChoiceLine;
        StartAfterChoiceDialogue();
    }

    public void OnNo()
    {
        AudioManager.Instance.PlaySelectSound();
        HandleStatChanges(frogEvent.noChoice.statChanges);
        lines = frogEvent.noChoice.afterChoiceLine;
        StartAfterChoiceDialogue();
    }

    void HandleStatChanges(List<StatChange> statChanges)
    {
        foreach (StatChange statChange in statChanges)
        {
            switch (statChange.stat) 
            {
                case StatType.Stability:
                    GameSceneManager.Instance.ModifyStability(statChange.amount);
                    break;
                case StatType.Discontent:
                    GameSceneManager.Instance.ModifyDiscontent(statChange.amount);
                    break;
                case StatType.Favor:
                    GameSceneManager.Instance.ModifyFavor(statChange.amount);
                    break;
                case StatType.Population:
                    GameSceneManager.Instance.ModifyPopulation(statChange.amount);
                    break;
            }
        }
    }
}