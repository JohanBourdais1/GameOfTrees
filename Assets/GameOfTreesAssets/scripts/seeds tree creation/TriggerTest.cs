using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TriggerTest : MonoBehaviour
{
    InputDevice rightController;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool value);
        Debug.LogError("right controller button down: " + value);
        if (value)
        {
            Rigidbody inst = Instantiate(rb);
            inst.AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
        }
    }
}
