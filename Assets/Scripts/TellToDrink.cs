using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TellToDrink : MonoBehaviour
{
    System.Timers.Timer timer;
    public GameObject textBubble;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        textBubble.SetActive(true);
        InvokeRepeating("ShowDrinkText", 0f, 1800f); // Appelle la méthode MyMethod toutes les 30 minutes (1800 secondes)
        InvokeRepeating("ShowStretchingText", 0f, 3500f); 
        StartCoroutine(HideTextAuto());

    }

    private void ShowDrinkText()
    {
        StartCoroutine(HideTextAuto("Time to Drink!"));
    }

    private void ShowStretchingText()
    {
        StartCoroutine(HideTextAuto("Time to stretch"));
    }

    private IEnumerator HideTextAuto(string t = "No text", int delay = 60)
    {
        textBubble.SetActive(true);
        text.text = t;
        yield return new WaitForSeconds(delay);
        textBubble.SetActive(false);
    }



 

    // Update is called once per frame
    void Update()
    {
    
        
    }


}
