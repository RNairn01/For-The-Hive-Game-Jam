using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioManager audioManager;
    private EventSpawn eventSpawn;
    private SliderManager sliderManager;
    private TextManager textManager;
    [SerializeField] private SceneManage sceneManage;
    [SerializeField] private Image dayOverOverlay;
    public Button commitButton;
    public int honeyDrained;
    public int beesGained;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        textManager = FindObjectOfType<TextManager>();
        sliderManager = FindObjectOfType<SliderManager>();
        StartGame();
        textManager.ClearPopups();
        eventSpawn = FindObjectOfType<EventSpawn>();
        StartTurn();
    }

    void StartGame()
    {
        ResourceTracker.turnCounter = 0;
        ResourceTracker.honey = ResourceTracker.startingHoney;
        ResourceTracker.bees = ResourceTracker.startingBees;
        ResourceTracker.prestige = ResourceTracker.startingPrestige;

    }

    public void StartTurn()
    {
        textManager.honeyThisTurn = 0;
        textManager.beesThisTurn = 0;
        textManager.prestigeThisTurn = 0;
        ResourceTracker.decisionsMade = 0;

        //Costs 10 honey each turn by default
        if (ResourceTracker.turnCounter != 0) ResourceTracker.honey -= honeyDrained;
        //10 Bees born every turn
        if (ResourceTracker.turnCounter != 0) ResourceTracker.bees += beesGained;

        ResourceTracker.turnCounter++;
        eventSpawn.ChangeCardText();
        eventSpawn.MoveCards();
        LeanTween.alphaCanvas(dayOverOverlay.GetComponent<CanvasGroup>(), 0f, 1f);
        dayOverOverlay.GetComponent<CanvasGroup>().blocksRaycasts = false;
        dayOverOverlay.GetComponent<CanvasGroup>().interactable = false;

        foreach (var slider in sliderManager.sliders)
        {
            slider.value = 0;
            slider.GetComponentInParent<SliderManager>().chanceText.text = "";

        }

        textManager.honeyPopup.text = "";
        textManager.beePopup.text = "";
        textManager.prestigePopup.text = "";

        //Disables button until slider gets moved
        commitButton.enabled = false;
        commitButton.gameObject.SetActive(false);
    }

    public void NewCards()
    {
        eventSpawn.ChangeCardText();
        eventSpawn.MoveCards();

        foreach (var slider in sliderManager.sliders)
        {
            slider.value = 0;
            slider.GetComponentInParent<SliderManager>().chanceText.text = "";

        }

        textManager.honeyPopup.text = "";
        textManager.beePopup.text = "";
        textManager.prestigePopup.text = "";

        //Disables button until slider gets moved
        commitButton.enabled = false;
        commitButton.gameObject.SetActive(false);
    }
    public void EndTurn()
    {
        if (ResourceTracker.honey <= 0)
        {
            sceneManage.GameOverHoney();
            return;
        }

        if (ResourceTracker.bees <= 0)
        {
            sceneManage.GameOverBees();
            return;
        }

        if (ResourceTracker.turnCounter >= 7 && ResourceTracker.prestige >= 200)
        {
            sceneManage.GameWinTrue();
            return;
        }
        if (ResourceTracker.turnCounter >= 7)
        {
            sceneManage.GameWinNormal();
            return;
        }
        
        textManager.UpdateQuote();
        textManager.HoneyTransitionText();
        textManager.BeeTransitionText();
        textManager.PrestigeTransitionText();

        LeanTween.alphaCanvas(dayOverOverlay.GetComponent<CanvasGroup>(), 1f, 0.5f);
        dayOverOverlay.GetComponent<CanvasGroup>().blocksRaycasts = true;
        dayOverOverlay.GetComponent<CanvasGroup>().interactable = true;
    }

   public bool ResolveEvent(float beesIn, int eventDifficulty, out int succChance )
    {
        int successChance;
        int rand = GetPercent();
        if (beesIn >= eventDifficulty) successChance = 95;
        else
        {
            successChance = Mathf.RoundToInt((beesIn / eventDifficulty) * (100/1)) - 5;
            if (successChance < 1) successChance = 1;
        }
        succChance = successChance;

        if (rand <= successChance) return true;
        else
        {
            return false;
            
        }
    }

    public void StaggeredResolve()
    {
        audioManager.Play("StampSound");

        for (int i = 0; i < sliderManager.sliders.Length; i++)
        {
            sliderManager.sliders[i].GetComponentInParent<SliderManager>().chanceText.text = "";
            eventSpawn.eventCards[i].WiggleCard();
            LeanTween.delayedCall(i + 1, sliderManager.sliders[i].GetComponentInParent<SliderManager>().ResolveEventSlider);
            LeanTween.delayedCall(i + 1, eventSpawn.eventCards[i].EndWiggle);
            
        }

        //Temporary end turn test
        if (ResourceTracker.decisionsMade >= 1)
        {
            LeanTween.delayedCall(4, eventSpawn.MoveOffScreen);
            LeanTween.delayedCall(5, EndTurn);
        }
        else
        {
            ResourceTracker.decisionsMade++;
            LeanTween.delayedCall(4, eventSpawn.MoveOffScreen);
            LeanTween.delayedCall(5, NewCards);
        }
    }

    public int GetPercent()
    {
        return Random.Range(1, 101);
    }
}
