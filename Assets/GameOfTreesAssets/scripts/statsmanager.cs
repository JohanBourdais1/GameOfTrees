using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class daystats
{
    public int quta;
    public int daynumber;
    public int treeschopped;
    public bool finished;
    public int TimeinDay;

    public int finishtime;
}

public class statsmanager : MonoBehaviour
{
    public static List<daystats> daysList = new List<daystats>();
    public static List<string> TreestoDelete = new List<string>();

    public static List<Vector3> treesToCreate = new List<Vector3>();
    public GameObject treeCreatePrefab;

    public static int demonumber = 0;

    public List<daystats> editorlist = new List<daystats>();
    public static int currentday = 0;

    public float timescenelaunched;
    // Start is called before the first frame update
    void Start()
    {
        timescenelaunched = Time.time;
        if (daysList.Count == 0)
            daysList = editorlist;

        if (SceneManager.GetSceneByName("Landsacape").isLoaded)
        {
            ChangeSkybox sunSetter = GameObject.FindGameObjectWithTag("sun").GetComponent<ChangeSkybox>();
            sunSetter.DayLength = daysList[currentday].TimeinDay * 2f;
            sunSetter.startHour = 7.5f;

            if (currentday == 0)
            {
                int treeNum = 0;
                foreach (Vector3 treePos in treesToCreate)
                {
                    GameObject newTree = Instantiate(treeCreatePrefab, treePos, Quaternion.identity);
                    newTree.transform.localScale = newTree.transform.localScale * 0.8f;
                    newTree.name = "Created Tree " + treeNum;
                    DontDestroyOnLoad(newTree);
                    treeNum++;
                }
            }
            if (currentday > 0)
            {
                int treeNum = 0;
                foreach (Vector3 treePos in treesToCreate)
                {
                    GameObject myObject = GameObject.Find("Created Tree " + treeNum);
                    myObject.transform.position = treesToCreate[treeNum];
                    myObject.transform.rotation = Quaternion.identity;

                    treeNum++;
                }
            }
            foreach (var tree in TreestoDelete)
            {
                ///set to a function that spawns the stump but not the log

                GameObject myObject = GameObject.Find(tree);
                Debug.Log("tree name: " + tree);

                TreeScript a = myObject.GetComponent<TreeScript>();
                a.CreateStump();
                myObject.SetActive(false);
            }
        }
        Debug.Log(daysList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        // Debug.Log(
        //      "Current Time: " + currentTime.ToString("F2") + "the end time" + currentday.TimeinDay
        // ); // Displaying time formatted to 2 decimal places
        if (SceneManager.GetSceneByName("Landsacape").isLoaded)
        {
            if (currentTime > daysList[currentday].TimeinDay + timescenelaunched || daysList[currentday].treeschopped >= daysList[currentday].quta)
            {
                // SceneManager.LoadScene("ResultScreen");

                daysList[currentday].finishtime = (int)(daysList[currentday].TimeinDay + timescenelaunched - currentTime);
                SceneFader componentA;
                componentA = GetComponent<SceneFader>();
                componentA.FadeToScene("ResultScreen");
            }
        }
        else
        if(SceneManager.GetSceneByName("Devestation").isLoaded)
        {
            if (currentTime > (20 + timescenelaunched))
            {
                
            ClearDays();
            SceneFader componentA;
            componentA = GetComponent<SceneFader>();
            componentA.FadeToScene("StartingMenu");
            }
        }
    }
    public void NextDay()
    {
        if (daysList[currentday].treeschopped >= daysList[currentday].quta)
        {

            daysList[currentday].finished = true;
            if (currentday != daysList.Count - 1)
            {
                currentday++;
                Debug.Log("Current Day finished: " + daysList[currentday].finished);
                SceneFader componentA;
                componentA = GetComponent<SceneFader>();
                componentA.FadeToScene("Landsacape");
            }
            else
            {
                
                SceneFader componentA;
                componentA = GetComponent<SceneFader>();
                componentA.FadeToScene("finalResults");
            }
        }
        else
        {

        }
    }
    public void AddTree(GameObject game)
    {
        daysList[currentday].treeschopped++;
        TreestoDelete.Add(game.name);
    }
    public static void ClearDays()
    {
        daysList.Clear();
        TreestoDelete.Clear();
    }
    public static void addTreeToCreate(Vector3 treePos)
    {
        treesToCreate.Add(treePos);
    }
}
