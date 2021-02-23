using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class DialogueDisplay : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI speakerName;
    public Image speakerIcon;
    public Dialogue[] sentences;
    private int index;
    [SerializeField] private float typingSpeed;
    [SerializeField] private SceneManage sceneManage;
    public GameObject continueButton;
    private AudioManager audioManager;
    int buzzCounter = 0;
    [SerializeField] private Image sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        sceneManage = FindObjectOfType<SceneManage>();
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index].dialogueText)
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        speakerName.text = sentences[index].speakerName;
        speakerIcon.sprite = sentences[index].speakerIcon;

        foreach (var letter in sentences[index].dialogueText.ToCharArray())
        {
            buzzCounter++;

            if (sentences[index].dialogueText.ToCharArray().Length < 5)
            {
                if (sentences[index].speakerName == "The Queen")
                {
                    int rand = Random.Range(0, 2);
                    switch(rand)
                    {
                        case 0:
                            audioManager.Play("BuzzQueen");
                            break;
                        case 1:
                            audioManager.Play("BuzzQueen2");
                            break;
                        case 2:
                            audioManager.Play("BuzzQueen3");
                            break;
                    }
                    
                    buzzCounter = 0;
                }
                else if (sentences[index].speakerName == "Worker #486")
                {
                    int rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            audioManager.Play("Buzz");
                            break;
                        case 1:
                            audioManager.Play("Buzz2");
                            break;
                        case 2:
                            audioManager.Play("Buzz3");
                            break;
                    }
                    buzzCounter = 0;
                }

            } 
            else if (buzzCounter >= 8)
            {
                if (sentences[index].speakerName == "The Queen")
                {
                    int rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            audioManager.Play("BuzzQueen");
                            break;
                        case 1:
                            audioManager.Play("BuzzQueen2");
                            break;
                        case 2:
                            audioManager.Play("BuzzQueen3");
                            break;
                    }
                    buzzCounter = 0;
                }
                else if (sentences[index].speakerName == "Worker #486")
                {
                    int rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            audioManager.Play("Buzz");
                            break;
                        case 1:
                            audioManager.Play("Buzz2");
                            break;
                        case 2:
                            audioManager.Play("Buzz3");
                            break;
                    }
                    buzzCounter = 0;
                }
            }
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        audioManager.Play("Click");

        continueButton.SetActive(false);
        buzzCounter = 0;

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
           
            if (SceneManager.GetActiveScene().name == "StartDialogue")
            {
                LeanTween.alphaCanvas(sceneTransition.GetComponent<CanvasGroup>(), 1f, 0.5f);
                LeanTween.delayedCall(1f, () =>
                {
                    sceneManage.EnterGame();
                    return;
                });

            } 
            else
            {
                LeanTween.alphaCanvas(sceneTransition.GetComponent<CanvasGroup>(), 1f, 0.5f);
                LeanTween.delayedCall(1f, () =>
                {
                    sceneManage.MainMenu();
                    return;
                });
            }
            
        }
    }
}
