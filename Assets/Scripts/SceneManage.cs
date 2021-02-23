using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene("MainMenu");
        ResourceTracker.honey = ResourceTracker.startingHoney;
        ResourceTracker.bees = ResourceTracker.startingBees;
        ResourceTracker.prestige = ResourceTracker.startingPrestige;
    }
    public void StartScene()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene("StartDialogue");
    }

    public void EnterGame()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene("GameScene");
    }

    public void HowToPlayScene()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene("HowToPlay");
    }

    public void GameOverHoney()
    {
        SceneManager.LoadScene("GameOverHoney");
    }

    public void GameOverBees()
    {
        SceneManager.LoadScene("GameOverBees");
    }

    public void GameWinNormal()
    {
        SceneManager.LoadScene("GameWinNormal");

    }

    public void GameWinTrue()
    {
        SceneManager.LoadScene("GameWinTrue");
    }

}
