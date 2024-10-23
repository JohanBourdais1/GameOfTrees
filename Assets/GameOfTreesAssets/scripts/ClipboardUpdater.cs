using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class ClipboardUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    statsmanager statman;
    public TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Start()
    {
        statman = FindObjectOfType<statsmanager>();

    }

    // Update is called once per frame
    void Update()
    {
        string TimeText = "Time Left:" + ((int)(statsmanager.daysList[statsmanager.currentday].TimeinDay + statman.timescenelaunched - Time.time)).ToString();
        string quotaString = statsmanager.daysList[statsmanager.currentday].quta.ToString();
        string treesChoppedString = statsmanager.daysList[statsmanager.currentday].treeschopped.ToString();
        string newText = TimeText + "\n" + "Treess Chopped: " + treesChoppedString + " / " + quotaString;

        displayText.text = newText;

    }
}
