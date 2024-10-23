using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingGrowth : MonoBehaviour
{
    public float startGrowth;
    public float growthSpeedPerSecond;
    public float maxGrowth;
    public float growthVariance;
    private float maxGrowthWithVariance;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(startGrowth, startGrowth, startGrowth);
        maxGrowthWithVariance = maxGrowth + Random.Range(-growthVariance, growthVariance);
    }

    // Update is called once per frame
    void Update()
    {
        float growth = growthSpeedPerSecond * Time.deltaTime; 
        if (transform.localScale.y < maxGrowthWithVariance) // if growing still
        {
            transform.localScale += new Vector3(growth, growth, growth); 
        }
        else // no longer growing
        {
            transform.localScale = new Vector3(maxGrowthWithVariance, maxGrowthWithVariance, maxGrowthWithVariance);
            Destroy(this);
        }
    }
}
