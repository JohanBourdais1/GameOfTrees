using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public GameObject treeSapling;
    public float groundDist;
    public float minSaplingSize;
    public float maxSaplingSize;

    public GameObject seedsEffect;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NotTreeSpawnable"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.TryGetComponent(out TerrainCollider terrain))
        {
            // create sapling
            GameObject sapling = Instantiate(treeSapling, transform.position, Quaternion.identity);
            statsmanager.addTreeToCreate(sapling.transform.position);

            // move sapling to ground
            sapling.transform.Translate(Vector3.up * -groundDist);

            // add sapling growth
            float scale = Random.Range(minSaplingSize, maxSaplingSize);

            SaplingGrowth sapGrowth = sapling.AddComponent<SaplingGrowth>();
            sapGrowth.startGrowth = scale;
            sapGrowth.maxGrowth = 0.7f;
            sapGrowth.growthVariance = 0.2f;
            sapGrowth.growthSpeedPerSecond = Random.Range(0.005f, 0.03f);

            // spawn effect
            Instantiate(seedsEffect, transform.position, transform.rotation);

            //// camera shake
            //ShakeEffect shakeEffect = Camera.main.gameObject.AddComponent<ShakeEffect>();
            //shakeEffect.shakeTime = 1;
            //shakeEffect.shakeAmount = 0.1f;
        }
    }
}
