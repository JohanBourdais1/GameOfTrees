using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public float shakeTime = 1;
    public float shakeAmount = 0.7f;
    public float decreaseAmount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTime > 0)
        {
            transform.localPosition = Random.insideUnitSphere * shakeAmount * shakeTime;
            shakeTime -= Time.deltaTime * decreaseAmount;
        }
        else
        {
            Destroy(this);
            shakeTime = 0;
        }
    }
}
