using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRdetector : MonoBehaviour
{
    public GameObject vrObject;
    public GameObject nonVrObject;
    // Start is called before the first frame update
    void Start()
    {
        if (XRSettings.enabled)
        {
            Debug.Log("Game is running in VR mode");

            vrObject.SetActive(true);

            nonVrObject.SetActive(false);

        }
        else
        {

            vrObject.SetActive(false);

            nonVrObject.SetActive(true);

            Debug.Log("Game is not running in VR mode");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
