using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum EventType
{
    InHive,
    OutHive,
    Defence,
    Crisis
}
public class EventDisplay : MonoBehaviour
{
    private AudioManager audioManager;
    public Event hiveEvent;
    public Image eventImage;
    public TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventText;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        eventText.text = hiveEvent.eventText;
        eventImage.sprite = hiveEvent.eventArt;
    }

    public void WiggleCard()
    {
        var seq = LeanTween.sequence();
        seq.append(transform.LeanRotateZ(0.5f, 0.1f).setLoopPingPong(-1)); 
        seq.append(transform.LeanRotateZ(-0.5f, 0.1f).setLoopPingPong(-1)); 
        
    }

    public void EndWiggle()
    {
        LeanTween.cancel(gameObject);
        audioManager.Play("FinishWiggle");
    }

}
