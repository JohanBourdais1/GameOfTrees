using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainTreeGrowth : MonoBehaviour
{
    public float maxGrowthHeight;
    public float minGrowthHeight;
    public float growthSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, minGrowthHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * growthSpeed * Time.deltaTime);
        //print("start height: " + startHeight + ", max height: " + maxGrowthHeight + ", totalHeight: " + totalHeight + ", position height: " + transform.position.y);
        if (transform.position.y > maxGrowthHeight)
        {
            GameObject.Find("STATMANAGER").GetComponent<SceneFader>().FadeToScene("landsacape");
            print("done");
            Destroy(this);
        }
    }
}
