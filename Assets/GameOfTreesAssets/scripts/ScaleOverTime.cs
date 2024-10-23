using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public float startScale;
    public float scalePerSecond;

    float currentScale;

    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        transform.localScale = startScale * initialScale;
        currentScale = startScale;
    }

    // Update is called once per frame
    void Update()
    {
        currentScale += Time.deltaTime * scalePerSecond;
        transform.localScale = initialScale * currentScale;
    }
}
