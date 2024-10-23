using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchScript : MonoBehaviour
{
    public Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = parentTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
