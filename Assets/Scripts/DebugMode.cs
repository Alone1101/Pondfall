using UnityEngine;

public class DebugMode : MonoBehaviour
{
    public static DebugMode Instance { get; private set; }

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1) && Input.GetKeyDown(KeyCode.UpArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyStability(10);
        }

        if (Input.GetKey(KeyCode.F1) && Input.GetKeyDown(KeyCode.DownArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyStability(-10);
        }

        if (Input.GetKey(KeyCode.F2) && Input.GetKeyDown(KeyCode.UpArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyDiscontent(10);
        }

        if (Input.GetKey(KeyCode.F2) && Input.GetKeyDown(KeyCode.DownArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyDiscontent(-10);
        }

        if (Input.GetKey(KeyCode.F3) && Input.GetKeyDown(KeyCode.UpArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyFavor(10);
        }

        if (Input.GetKey(KeyCode.F3) && Input.GetKeyDown(KeyCode.DownArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyFavor(-10);
        }

        if (Input.GetKey(KeyCode.F4) && Input.GetKeyDown(KeyCode.UpArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyPopulation(10);
        }

        if (Input.GetKey(KeyCode.F4) && Input.GetKeyDown(KeyCode.DownArrow) && GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.ModifyPopulation(-10);
        }
    }
}
