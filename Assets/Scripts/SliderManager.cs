using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText;
    public TextMeshProUGUI chanceText;
    private GameManager gameManager;
    public Slider slider;
    public Slider[] sliders;
    public float beesToCommit;
    EventDisplay parent;
    int successChance = 1;

    private void Start()
    {
        beesToCommit = 0;
        chanceText.text = "";
    }

    private void Awake()
    {
        slider = GetComponent<Slider>();
        gameManager = FindObjectOfType<GameManager>();
        parent = GetComponentInParent<EventDisplay>();
    }
    void Update()
    {
        sliderText.text = slider.value.ToString();
    }

    public void CheckSliderMax()
    {
        if (ResourceTracker.bees < 0) slider.value = 0;

        float sliderSum = 0;

        foreach (var slide in sliders)
        {
            sliderSum += slide.value;
        }

        if (sliderSum > ResourceTracker.bees)
        {
            slider.value = Mathf.Abs(ResourceTracker.bees - (sliderSum - slider.value));
        }

        beesToCommit = slider.value;
        //Updating the success chance, there's probably a better way to do this
        gameManager.ResolveEvent(beesToCommit, parent.hiveEvent.eventDifficulty, out successChance);
        if (successChance < 1) chanceText.text = "??%";
        else chanceText.text = $"{successChance}%";



        //Reenables button
        gameManager.commitButton.enabled = true;
        gameManager.commitButton.gameObject.SetActive(true);
    }

    public void ResolveEventSlider()
    {

        if (gameManager.ResolveEvent(beesToCommit, parent.hiveEvent.eventDifficulty, out successChance))
        {
            Debug.Log($"Success on event in {parent.name} - Committed {beesToCommit} - Difficulty {parent.hiveEvent.eventDifficulty}");
            //Display on event card
            parent.eventTitle.text = "Success!";
            parent.eventTitle.color = Color.green;
            //Lose 10% of bees committed on success
            ResourceTracker.bees -= Mathf.RoundToInt(beesToCommit * 0.1f);
            //Gain resource from event success
            switch(parent.hiveEvent.succRes)
            {
                case GameResources.Bees:
                    ResourceTracker.bees += parent.hiveEvent.succAmount;
                    break;
                case GameResources.Honey:
                    ResourceTracker.honey += parent.hiveEvent.succAmount;
                    break;
                case GameResources.Prestige:
                    ResourceTracker.prestige += parent.hiveEvent.succAmount;
                    break;
                default:
                    Debug.Log("F");
                    break;
            }
        }
        else
        {
            Debug.Log($"Failure on event in {parent.name} - Committed {beesToCommit} - Difficulty {parent.hiveEvent.eventDifficulty}");
            //Display on event card
            parent.eventTitle.text = "Failure!";
            parent.eventTitle.color = Color.red;
            //Lose 25% of bees committed on failure
            ResourceTracker.bees -= Mathf.RoundToInt(beesToCommit * 0.25f);
            //Lose Resources from event failure
            switch (parent.hiveEvent.failRes)
            {
                case GameResources.Bees:
                    ResourceTracker.bees -= parent.hiveEvent.failAmount;
                    break;
                case GameResources.Honey:
                    ResourceTracker.honey -= parent.hiveEvent.failAmount;
                    break;
                case GameResources.Prestige:
                    ResourceTracker.prestige -= parent.hiveEvent.failAmount;
                    break;
                default:
                    Debug.Log("F");
                    break;
            }
        }
    }
  
    
}
