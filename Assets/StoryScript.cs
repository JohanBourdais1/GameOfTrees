using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryScript : MonoBehaviour
{
    private List<String> stories = new List<string>();
    public TextMeshProUGUI displayText;
    int storyTracker = 0;
    // Start is called before the first frame update
    void Start()
    {
        stories.Add("Evergreen Timberworks (the largest logging company in the world) has hired you to chop down the forest.");
        stories.Add("The company is pushing hard to boost profits, and they're counting on you to hit these increasing quotas.");
        stories.Add("If you don't meet the targets, there's a line of others ready to take your place.");
        stories.Add("I know it's tough, but missing this chance could mean losing your job.");
        stories.Add("Good luck!");
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = stories[storyTracker];
    }

    public void nextStory()
    {
        if( storyTracker < stories.Count -1)
        {
            storyTracker++;
        }else{
            SceneFader  myComponent = FindObjectOfType<SceneFader>();
            if (myComponent != null)
            {
                    myComponent.FadeToScene("TreeGrowth");
                
            }
        }
    }
}
