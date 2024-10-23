using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class Throwable : MonoBehaviour
{
    bool pickedUpRight = false;
    bool pickedUpLeft = false;
    public Transform rightHand;
    public Transform leftHand;

    public Rigidbody seeds;

    InputDevice rightController;
    InputDevice leftController;

    List<Vector3> leftTrackingPos = new List<Vector3>();
    List<Vector3> rightTrackingPos = new List<Vector3>();

    //List<GameObject> rightDebugPoints;
    //List<GameObject> leftDebugPoints;
    //public GameObject debugPoint;

    // Start is called before the first frame update
    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    // Update is called once per frame
    void Update()
    {
        if (rightController.isValid == false)
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (leftController.isValid == false)
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerPressed) && rightTriggerPressed > 0.7)
        {
            pickedUpRight = true;
        }
        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerPressed) && leftTriggerPressed > 0.7)
        {
            pickedUpLeft = true;
        }

        if (pickedUpLeft)
        {
            leftTrackingPos.Add(leftHand.position);
            //leftDebugPoints.Add(ref Instantiate(debugPoint, leftHand.position, leftHand.rotation));
            if (leftTrackingPos.Count > 4)
            {
                leftTrackingPos.RemoveAt(0);
                //Destroy(leftDebugPoints[0]);
                //leftDebugPoints.RemoveAt(0);
            }
        }
        if (pickedUpRight)
        {
            rightTrackingPos.Add(rightHand.position);
            //rightDebugPoints.Add(Instantiate(debugPoint, rightHand.position, rightHand.rotation));
            if (rightTrackingPos.Count > 4)
            {
                rightTrackingPos.RemoveAt(0);
                //Destroy(rightDebugPoints[0]);
                //rightDebugPoints.RemoveAt(0);
            }
        }

        if ((pickedUpLeft && leftTriggerPressed < 0.3) || (pickedUpRight && rightTriggerPressed < 0.3))
        {
            Vector3 velocity = Vector3.zero;
            Vector3 pos = Vector3.zero;
            Quaternion rot = Quaternion.identity;
            if (pickedUpLeft)
            {
                pickedUpLeft = false;
                //leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
                velocity = leftTrackingPos[leftTrackingPos.Count - 1] - leftTrackingPos[0];
                pos = leftHand.position;
                rot = leftHand.rotation;
            }
            if (pickedUpRight)
            {
                pickedUpRight = false;
                //rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
                velocity = rightTrackingPos[rightTrackingPos.Count - 1] - rightTrackingPos[0];
                pos = rightHand.position;
                rot = rightHand.rotation;
            }

            Rigidbody inst = Instantiate(seeds, pos, rot);
            inst.AddForce(velocity / Time.deltaTime * 0.6f, ForceMode.Impulse);
            inst.useGravity = true;
            inst.isKinematic = false;
        }
    }
}
