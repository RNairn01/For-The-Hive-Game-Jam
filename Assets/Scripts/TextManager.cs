using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayCounterText;
    [SerializeField] private TextMeshProUGUI honeyText;
    public TextMeshProUGUI honeyPopup;
    [SerializeField] private TextMeshProUGUI beeText;
    public TextMeshProUGUI beePopup;
    [SerializeField] private TextMeshProUGUI prestigeText;
    public TextMeshProUGUI prestigePopup;
    [SerializeField] private TextMeshProUGUI honeyTransitionText;
    [SerializeField] private TextMeshProUGUI beeTransitionText;
    [SerializeField] private TextMeshProUGUI prestigeTransitionText;
    private float currentHoney, currentBees, currentPrestige;
    [HideInInspector] public float honeyThisTurn, beesThisTurn, prestigeThisTurn;
    [SerializeField] private TextMeshProUGUI quoteText;
    [SerializeField] private List<string> quotes;

    private void Start()
    {
        currentHoney = ResourceTracker.honey;
        currentBees = ResourceTracker.bees;
        currentPrestige = ResourceTracker.prestige;
        ClearPopups();
    }

    void Update()
    {
        dayCounterText.text = $"Day: {ResourceTracker.turnCounter}";
        honeyText.text = ResourceTracker.honey.ToString();
        beeText.text = ResourceTracker.bees.ToString();
        prestigeText.text = ResourceTracker.prestige.ToString();

        CheckHoney();
        CheckBees();
        CheckPrestige();
    }

    void CheckHoney()
    {
        if (currentHoney != ResourceTracker.honey)
        {

            float honeyDif = ResourceTracker.honey - currentHoney;

            if (honeyDif < 0)
            {
                honeyPopup.text = honeyDif.ToString();
                honeyPopup.color = Color.red;
            }
            else
            {
                honeyPopup.text = $"+{honeyDif}";
                honeyPopup.color = Color.green;
            }

            honeyThisTurn += honeyDif;
            currentHoney = ResourceTracker.honey;
            LeanTween.delayedCall(0.9f, ClearPopups);
        }
    }

    public void HoneyTransitionText()
    {
        if (honeyThisTurn < 0)
        {
            honeyTransitionText.text = honeyThisTurn.ToString();
            honeyTransitionText.color = Color.red;
        }
        else
        {
            honeyTransitionText.text = $"+{honeyThisTurn}";
            honeyTransitionText.color = Color.green;
        }
    }

    void CheckBees()
    {
        if (currentBees != ResourceTracker.bees)
        {
            float beeDif = ResourceTracker.bees - currentBees;

            if (beeDif < 0)
            {
                beePopup.text = beeDif.ToString();
                beePopup.color = Color.red;
            }
            else
            {
                beePopup.text = $"+{beeDif}";
                beePopup.color = Color.green;
            }

            beesThisTurn += beeDif;
            currentBees = ResourceTracker.bees;
            LeanTween.delayedCall(0.9f, ClearPopups);
        }
    }

    public void BeeTransitionText()
    {
        if (beesThisTurn < 0)
        {
            beeTransitionText.text = beesThisTurn.ToString();
            beeTransitionText.color = Color.red;
        }
        else
        {
            beeTransitionText.text = $"+{beesThisTurn}";
            beeTransitionText.color = Color.green;
        }
    }

    void CheckPrestige()
    {
        if (currentPrestige != ResourceTracker.prestige)
        {
            float prestigeDif = ResourceTracker.prestige - currentPrestige;

            if (prestigeDif < 0)
            {
                prestigePopup.text = prestigeDif.ToString();
                prestigePopup.color = Color.red;
            }
            else
            {
                prestigePopup.text = $"+{prestigeDif}";
                prestigePopup.color = Color.green;
            }

            prestigeThisTurn += prestigeDif;
            currentPrestige = ResourceTracker.prestige;
            LeanTween.delayedCall(0.9f, ClearPopups);
        }
    }

    public void PrestigeTransitionText()
    {
        if (prestigeThisTurn < 0)
        {
            prestigeTransitionText.text = prestigeThisTurn.ToString();
            prestigeTransitionText.color = Color.red;
        }
        else
        {
            prestigeTransitionText.text = $"+{prestigeThisTurn}";
            prestigeTransitionText.color = Color.green;
        }
    }

    public void ClearPopups()
    {
        honeyPopup.text = "";
        beePopup.text = "";
        prestigePopup.text = "";
    }

    public void UpdateQuote()
    {
        quoteText.text = quotes[Random.Range(0, quotes.Count)];
    }
}
