using UnityEngine;
using UnityEngine.UI;

public class PotraitManager : MonoBehaviour
{
    public GameObject leftPotrait;
    public GameObject rightPotrait;
    public GameObject leftPotraitBackground;
    public GameObject rightPotraitBackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisablePotraits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayLeftPotrait(Sprite potrait)
    {
        leftPotrait.GetComponent<Image>().sprite = potrait;
        leftPotraitBackground.SetActive(true);
        rightPotraitBackground.SetActive(false);
    }

    public void DisplayRightPotrait(Sprite potrait)
    {
        rightPotrait.GetComponent<Image>().sprite = potrait;
        rightPotraitBackground.SetActive(true);
        leftPotraitBackground.SetActive(false);
    }

    public void DisablePotraits()
    {
        rightPotraitBackground.SetActive(false);
        leftPotraitBackground.SetActive(false);
    }
}
