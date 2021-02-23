using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSound : MonoBehaviour
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
        if (Input.GetMouseButtonDown(0)) audioManager.Play("Click");
        if (Input.GetMouseButtonUp(0)) audioManager.Play("ClickOff");
    }
}
