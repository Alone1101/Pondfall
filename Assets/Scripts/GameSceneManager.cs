using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public enum KingType
{
    None,
    Log,
    Eel,
    Heron,
    Council
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    // King change events
    public delegate void KingChangedDelegate(KingType newKing);
    public static event KingChangedDelegate OnKingChanged;

    // State change events
    public delegate void StatChangedDelegate(int newValue);
    public static event StatChangedDelegate OnStabilityChanged;
    public static event StatChangedDelegate OnDiscontentChanged;
    public static event StatChangedDelegate OnFavorChanged;
    public static event StatChangedDelegate OnPopulationChanged;
    public static event StatChangedDelegate OnDayChanged;

    [Header("Day Counter")]
    public int currentDay = 1;
    public TextMeshProUGUI dayText;
    
    [Header("Current Game Stats")]
    [Range(0, 100)] public int stability = 70;
    [Range(0, 100)] public int discontent = 30;
    [Range(0, 100)] public int favor = 5;
    [Range(0, 100)] public int population = 30;
    
    [Header("UI Sliders")]
    public Slider stabilitySlider;
    public Slider discontentSlider;
    public Slider favorSlider;
    public Slider populationSlider;

    [Header("King System")]
    public KingType currentKing = KingType.None;
    private int consecutiveHighDiscontentDays = 0;
    public int discontentThreshold = 80;
    public int daysRequiredForPrayer = 2;

    [System.Serializable]
    public class KingPassiveEffect
    {
        public KingType kingType;
        public int stabilityChangePerDay;
        public int discontentChangePerDay;
        public int favorChangePerDay;
        public int populationChangePerDay;
    }

    [Header("King System")]
    public PlayerIconController playerIconController;

    [Header("Daily Passive Effects")]
    public KingPassiveEffect[] dailyEffects;
    
    private KingType[] kingSequence = { 
        KingType.Log,
        KingType.Eel,
        KingType.Heron,
        KingType.Council
    };

    private int currentKingIndex = -1;

    private KingType pendingNextKing;

    [Header("Republic Ending")]
    private int councilPhaseDaysSurvived = 0;
    public int councilDaysRequired = 8;

    [Header("Perfect Harmony Ending")]
    private int perfectHarmonyConsecutiveDays = 0;
    public int perfectHarmonyDaysRequired = 3;
    public int perfectHarmonyStabilityThreshold = 70;
    public int perfectHarmonyDiscontentThreshold = 20;
    public int perfectHarmonyPopulationThreshold = 50;

    [Header("Eternal Stagnation Ending")]
    private int lowDiscontentConsecutiveDays = 0;
    public int stagnationDaysRequired = 10;
    public int stagnationDiscontentThreshold = 60;

    [Header("Dialogue and Events")]
    public DialogueManager dialogueManager;
    public EventsManager eventsManager;
    public Event day1Event;

    [Header("Transitions")]
    public Image fadePanel; 
    public float fadeDuration = 1.0f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        UpdateAllUI();
        OnKingChanged?.Invoke(currentKing);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) // Main Menu Scene
        {
            Debug.Log("Main Menu loaded - auto-resetting game");
            ResetGame();
        }
        
        if (scene.buildIndex == 3) // Game Scene
        {
            Debug.Log("Reconnecting UI for Main Game Scene");
            ReconnectUIReferences();
            UpdateAllUI();
            UpdateKingIcon();
            StartDay();
        }
    }

    public void ResetGame()
    {
        Debug.Log("Resetting all game stats to default");
    
        // Reset stats
        currentDay = 1;
        stability = 70;
        discontent = 30;
        favor = 5;
        population = 30;
        
        // Reset king system
        currentKing = KingType.None;
        currentKingIndex = -1;
        consecutiveHighDiscontentDays = 0;
        pendingNextKing = KingType.None;
        
        // Reset ending counters
        councilPhaseDaysSurvived = 0;
        perfectHarmonyConsecutiveDays = 0;
        lowDiscontentConsecutiveDays = 0;
        
        // Clear PlayerPrefs
        PlayerPrefs.DeleteKey("EndingType");
        PlayerPrefs.DeleteKey("DaysSurvived");
        PlayerPrefs.DeleteKey("NextKing");
        
        // Update UI
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            UpdateAllUI();
            UpdateKingIcon();
        }
        
        Debug.Log("Game reset is completed.");
    }
    
    private void ReconnectUIReferences()
    {
        // Find UI elements in the scene
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            // Find dialogue manager
            dialogueManager = FindFirstObjectByType<DialogueManager>();
            
            // Find events manager
            eventsManager = FindFirstObjectByType<EventsManager>();
            
            // Find player icon controller
            playerIconController = FindFirstObjectByType<PlayerIconController>();

            // Find fade panel
            GameObject fp = GameObject.Find("FadePanel");  
            if(fp != null) fadePanel = fp.GetComponent<Image>();
            
            Debug.Log("UI references reconnected successfully");
        }
        else
        {
            Debug.LogError("Canvas not found in scene!");
        }
    }
    
    private Slider FindSlider(GameObject parent, string name)
    {
        Slider[] sliders = parent.GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            if (slider.gameObject.name == name)
                return slider;
        }
        return null;
    }
    
    private TextMeshProUGUI FindText(GameObject parent, string name)
    {
        TextMeshProUGUI[] texts = parent.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI text in texts)
        {
            if (text.gameObject.name == name)
                return text;
        }
        return null;
    }

    public void AdvanceDay()
    {
        currentDay++;
        if (OnDayChanged == null)
        {
            Debug.Log("No day");
        }
        OnDayChanged?.Invoke(currentDay);
    }
    
    // Public methods to modify stats
    public void ModifyStability(int amount)
    {
        stability = Mathf.Clamp(stability + amount, 0, 100);
        UpdateStabilityUI();
    }
    
    public void ModifyDiscontent(int amount)
    {
        discontent = Mathf.Clamp(discontent + amount, 0, 100);
        UpdateDiscontentUI();
    }
    
    public void ModifyFavor(int amount)
    {
        favor = Mathf.Clamp(favor + amount, 0, 100);
        UpdateFavorUI();
    }
    
    public void ModifyPopulation(int amount)
    {
        population = Mathf.Clamp(population + amount, 0, 100);
        UpdatePopulationUI();
    }
    
    // Functions for UI (Sliders + Events) Update
    private void UpdateAllUI()
    {
        playerIconController.UpdateIcon(stability);
        UpdateStabilityUI();
        UpdateDiscontentUI();
        UpdateFavorUI();
        UpdatePopulationUI();
    }
    
    private void UpdateStabilityUI()
    {
        if (stabilitySlider != null)
            stabilitySlider.value = stability;

        OnStabilityChanged?.Invoke(stability);
    }
    
    private void UpdateDiscontentUI()
    {
        if (discontentSlider != null)
            discontentSlider.value = discontent;
        
        OnDiscontentChanged?.Invoke(discontent);
    }
    
    private void UpdateFavorUI()
    {
        if (favorSlider != null)
            favorSlider.value = favor;
        
        OnFavorChanged?.Invoke(favor);
    }
    
    private void UpdatePopulationUI()
    {
        if (populationSlider != null)
            populationSlider.value = population;
        
        OnPopulationChanged?.Invoke(population);
    }
    
    private void CheckGameState()
    {
        CheckEndConditions();
        CheckPrayerTrigger();
    }
    
    private void CheckEndConditions()
    {
        if (currentKing == KingType.Council && perfectHarmonyConsecutiveDays >= perfectHarmonyDaysRequired)
        {
            return;
        }

        if (currentKing == KingType.Council && councilPhaseDaysSurvived >= councilDaysRequired)
        {
            Debug.Log("ENDING: The Republic");
            TriggerEnding("Republic");
            return;
        }

        if (currentKing == KingType.Log && lowDiscontentConsecutiveDays >= stagnationDaysRequired)
        {
            Debug.Log("ENDING: Eternal Stagnation");
            TriggerEnding("EternalStagnation");
            return;
        }

        if (currentKing == KingType.Heron && population <= 0)
        {
            Debug.Log("ENDING: The Tyrant's Feast");
            TriggerEnding("TyrantsFeast");
            return;
        }

        if (stability <= 0)
        {
            Debug.Log("ENDING: Accelerated Collapse");
            TriggerEnding("AcceleratedCollapse");
            return;
        }
        
        if (population <= 0)
        {
            Debug.Log("ENDING: The Silent Pond");
            TriggerEnding("SilentPond");
            return;
        }
    }

    private void TriggerEnding(string endingType)
    {
        PlayerPrefs.SetString("EndingType", endingType);
        PlayerPrefs.SetInt("DaysSurvived", currentDay);
        SceneManager.LoadScene(6);
    }
    
    private void CheckPrayerTrigger()
    {
        if (discontent >= discontentThreshold)
        {
            consecutiveHighDiscontentDays++;
            
            if (consecutiveHighDiscontentDays >= daysRequiredForPrayer)
            {
                Debug.Log("PRAYER EVENT TRIGGERED!");
                StartCoroutine(TransitionToNextKing());
            }
        }
        else
        {
            consecutiveHighDiscontentDays = 0;
        }
    }

    private void CheckPerfectHarmony()
    {
        bool conditionsMet = 
        stability >= perfectHarmonyStabilityThreshold &&
        discontent <= perfectHarmonyDiscontentThreshold &&
        population >= perfectHarmonyPopulationThreshold;
    
        if (conditionsMet)
        {
            perfectHarmonyConsecutiveDays++;
            Debug.Log($"Perfect Harmony: Day {perfectHarmonyConsecutiveDays}/{perfectHarmonyDaysRequired}");
            
            if (perfectHarmonyConsecutiveDays >= perfectHarmonyDaysRequired)
            {
                Debug.Log("ENDING: Perfect Harmony");
                PlayerPrefs.SetString("EndingType", "PerfectHarmony");
                PlayerPrefs.SetInt("DaysSurvived", currentDay);
                SceneManager.LoadScene(6);
            }
        }
        else
        {
            perfectHarmonyConsecutiveDays = 0;
        }
    }

    private IEnumerator TransitionToNextKing()
    {
        KingType nextKing;
        
        if (currentKingIndex == -1)
        {
            nextKing = kingSequence[0];
            currentKingIndex = 0;
        }
        else if (currentKingIndex + 1 >= kingSequence.Length)
        {
            nextKing = KingType.Council;
        }
        else
        {
            nextKing = kingSequence[currentKingIndex + 1];
            currentKingIndex++;
        }
        
        if (nextKing == KingType.Council && !CanAffordRepublic())
        {
            nextKing = KingType.Heron;
            Debug.Log("Cannot afford Republic, remaining with Heron King");
            currentKingIndex--;
        }

        pendingNextKing = nextKing;

        if (nextKing != KingType.Council)
        {
            councilPhaseDaysSurvived = 0;
        }
        
        // Store king info to fetch from prayer scene
        PlayerPrefs.SetString("NextKing", nextKing.ToString());
        
        // Load prayer scene
        SceneManager.LoadScene(4);
        yield return null;
    }

    public void ApplyNewKing(KingType newKing)
    {
        currentKing = newKing;
        consecutiveHighDiscontentDays = 0;
        
        ModifyDiscontent(-60);
        UpdateDiscontentUI();
        UpdateKingIcon();
        SceneManager.LoadScene(3);
    }

    private void ApplyDailyKingEffects()
    {
        // Skip if no king
        if (currentKing == KingType.None) return;
        
        // Special handling for Council
        if (currentKing == KingType.Council)
        {
            int stabilityChange = Random.Range(0, 2) == 0 ? -8 : 8;
            stability = Mathf.Clamp(stability + stabilityChange, 0, 100);
            
            int discontentChange = Random.Range(0, 2) == 0 ? -12 : 12;
            discontent = Mathf.Clamp(discontent + discontentChange, 0, 100);
            
            UpdateAllUI();
            return;
        }
        
        foreach (var effect in dailyEffects)
        {
            if (effect.kingType == currentKing)
            {
                stability = Mathf.Clamp(stability + effect.stabilityChangePerDay, 0, 100);
                discontent = Mathf.Clamp(discontent + effect.discontentChangePerDay, 0, 100);
                favor = Mathf.Clamp(favor + effect.favorChangePerDay, 0, 100);
                population = Mathf.Clamp(population + effect.populationChangePerDay, 0, 100);
                
                UpdateAllUI();
                break;
            }
        }
    }

    public void CompleteKingTransition()
    {
        currentKing = pendingNextKing;
        consecutiveHighDiscontentDays = 0;

        if (currentKing == KingType.Council)
        {
            councilPhaseDaysSurvived = 0;
            perfectHarmonyConsecutiveDays = 0;
            Debug.Log("Council phase started! Survive 8 days to win.");
        }
        else
        {
            councilPhaseDaysSurvived = 0;
            perfectHarmonyConsecutiveDays = 0;
        }
        
        lowDiscontentConsecutiveDays = 0;

        ModifyDiscontent(-50);
        UpdateKingIcon();
        SceneManager.LoadScene(3);
    }

    public KingType GetPendingKing()
    {
        return pendingNextKing;
    }

    public void StartDay()
    {
        if (currentDay == 1)
        {
            dialogueManager.StartDialogue(day1Event);
            return;
        }

        // Get random event
        Event dailyEvent = eventsManager.GetRandomEvent();

        if (dailyEvent != null)
        {
            dialogueManager.StartDialogue(dailyEvent);
        }

    }

    // Call this after event choice
    public void EndOfDay()
    {
        CheckGameState();
        ApplyDailyKingEffects();

        if (currentKing == KingType.Council)
        {
            councilPhaseDaysSurvived++;
            Debug.Log($"Council Phase: Day {councilPhaseDaysSurvived}/{councilDaysRequired}");

            CheckPerfectHarmony();
        }
        else
        {
            perfectHarmonyConsecutiveDays = 0;
        }

        if (currentKing == KingType.Log && discontent <= stagnationDiscontentThreshold)
        {
            lowDiscontentConsecutiveDays++;
            Debug.Log($"Stagnation Streak: Day {lowDiscontentConsecutiveDays}/{stagnationDaysRequired}");
        }
        else
        {
            lowDiscontentConsecutiveDays = 0;
        }
    }

    public IEnumerator FadeRoutine(float targetAlpha)
    {
        if (fadePanel == null) yield break;
        
        float startAlpha = fadePanel.color.a;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, newAlpha);
            yield return null;
        }

        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, targetAlpha);

        // Stop blocking clicks only if the scene is now fully transparent
        if (targetAlpha <= 0) fadePanel.raycastTarget = false;
    }
    
    public void UpdateKingIcon()
    {
        OnKingChanged?.Invoke(currentKing);
    }
    
    public bool CanAffordRepublic()
    {
        return stability >= 50 && favor >= 40;
    }
    
    // Helper function to debug
    public string GetCurrentStats()
    {
        string councilInfo = (currentKing == KingType.Council) ? $" | Council Days: {councilPhaseDaysSurvived}/{councilDaysRequired}" : "";

        string harmonyInfo = (currentKing == KingType.Council) ? $" | Harmony Streak: {perfectHarmonyConsecutiveDays}/{perfectHarmonyDaysRequired}" : "";

        string stagnationInfo = (currentKing == KingType.Log) ? $" | Stagnation: {lowDiscontentConsecutiveDays}/{stagnationDaysRequired}" : "";

        return $"Stability: {stability} | Discontent: {discontent} | Favor: {favor} | Population: {population}";
    }
}