using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSpawn : MonoBehaviour
{
    private AudioManager audioManager;
    private int eventsSpawned = 3;
    public List<Event> eventDeck;
    public List<Event> seenCards;
    public Transform eventBlock;
    public EventDisplay[] eventCards;
    public Sprite inHiveBack;
    public Sprite outHiveBack;
    public Sprite defenceBack;
    public Sprite crisisBack;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    int RandomEventIndex()
    {
        return Random.Range(0, eventDeck.Count);
    }

    public void ChangeCardText()
    {
        if (eventDeck.Count < 3)
        {
            eventDeck.AddRange(seenCards);
            seenCards.Clear();
        }
        for (int i = 0; i < eventsSpawned; i++)
        {
            eventCards[i].hiveEvent = eventDeck[RandomEventIndex()];
            eventCards[i].eventTitle.text = eventCards[i].hiveEvent.eventName;
            eventCards[i].eventTitle.color = Color.black;
            seenCards.Add(eventCards[i].hiveEvent);
            eventDeck.Remove(eventCards[i].hiveEvent);

            eventCards[i].GetComponentInChildren<Slider>().value = 0;

            switch(eventCards[i].hiveEvent.eventType)
            {
                case EventType.InHive:
                    eventCards[i].GetComponentInChildren<Image>().sprite = inHiveBack;
                    break;
                case EventType.OutHive:
                    eventCards[i].GetComponentInChildren<Image>().sprite = outHiveBack;
                    break;
                case EventType.Defence:
                    eventCards[i].GetComponentInChildren<Image>().sprite = defenceBack;
                    break;
                case EventType.Crisis:
                    eventCards[i].GetComponentInChildren<Image>().sprite = crisisBack;
                    break;
            }

        }
    }

    public void EnableDisable()
    {
        if (eventCards[0].isActiveAndEnabled)
        {
            foreach (var card in eventCards)
            {
                card.gameObject.SetActive(false);
            }
        } else
        {
            foreach (var card in eventCards)
            {
                card.gameObject.SetActive(true);
            }
        }
    }

    public void MoveCards()
    {
        LeanTween.cancel(eventBlock.gameObject);
        eventBlock.transform.position = new Vector2(500, 500);
        LeanTween.move(eventBlock.gameObject, new Vector2(4, 4), 1.5f);
        LeanTween.delayedCall(1.5f, () => audioManager.Play("BellSound"));
    }

    public void MoveOffScreen()
    {
        LeanTween.move(eventBlock.gameObject, new Vector2(-500, -500), 1.5f);
        audioManager.Play("Woosh");
    }
}
