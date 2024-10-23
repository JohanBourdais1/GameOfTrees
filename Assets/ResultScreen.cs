using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResultScreen : MonoBehaviour
{
    statsmanager statman;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI buttontext;



    // Start is called before the first frame update
    void Start()
    {

        statman = FindObjectOfType<statsmanager>();
        String dayMessage = "";
        List<String> randomstat = new List<string> { "Bird nests destroyed: ", "Squirrels killed:", "Frogs smashed: ", "Butterflies squished: " };
        float payment = UnityEngine.Random.Range(1, 10) * statsmanager.daysList[statsmanager.currentday].treeschopped;

        if (statsmanager.daysList[statsmanager.currentday].treeschopped >= statsmanager.daysList[statsmanager.currentday].quta)
        {
            dayMessage = "Congrats you made Quota you get to keep your job";

        }
        else
        {
            payment = 0;
            dayMessage = "You did not make Quota you are fired";
            buttontext.text = "Go Home";
        }
        int randomIndex = UnityEngine.Random.Range(0, randomstat.Count);
        string randomStat = randomstat[randomIndex];
        // Generate a random value multiplied by the number of trees chopped
        int randomMultiplier = UnityEngine.Random.Range(1, 10); // Change the range as needed
        int randomValue = statsmanager.daysList[statsmanager.currentday].treeschopped * randomMultiplier;
        string quotaString = statsmanager.daysList[statsmanager.currentday].quta.ToString();
        string treesChoppedString = statsmanager.daysList[statsmanager.currentday].treeschopped.ToString();
        string newText = dayMessage + "\n" + "Trees Chopped: " + treesChoppedString + " / " + quotaString + "\n" + randomStat + randomValue.ToString() + "\n" + "Payment: $" + payment.ToString();

        displayText.text = newText;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
