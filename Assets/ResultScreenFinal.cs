using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResultScreenFinal : MonoBehaviour
{
    statsmanager statman;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI buttontext;



    // Start is called before the first frame update
    void Start()
    {

        statman = FindObjectOfType<statsmanager>();
        String dayMessage = "";
      
            dayMessage = "Congrats on Completing your job unfortunately you are no longer required ";

        
       
      
        int totalquata = 0;
        int totaltrees = 0;
        foreach (daystats day in statsmanager.daysList)
        {
            totalquata += day.quta;
            totaltrees += day.treeschopped;
        }
        // Generate a random value multiplied by the number of trees chopped
        int randomMultiplier = UnityEngine.Random.Range(1, 10); // Change the range as needed
        int randomValue = totaltrees * randomMultiplier;





        string quotaString = totalquata.ToString();
        string treesChoppedString = totaltrees.ToString();
        string newText = dayMessage + "\n" + "Trees Chopped: " + treesChoppedString + " / " + quotaString + "\n" + "\n" + "Forrest Ecosystems destroyed: 1 \n Final payment: $116";

        displayText.text = newText;
        statsmanager.ClearDays();
    }

    // Update is called once per frame
    void Update()
    {


    }
}
