using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
///if you read this its a cry for help i hate unity and i hate these units god help me

public class dumbdemoscript : MonoBehaviour
{public GameObject object1; // Assign the first object in the Inspector
    public GameObject object2; // Assign the second object in the Inspector

    private Renderer object1Renderer;
    private Renderer object2Renderer;

    private Rigidbody object1Rigidbody;

    private Vector3 resetPosition = new Vector3(0f, 5f, 9.31163f); 
    private Vector3 resetPosition2 = new Vector3(6.73f, 5f, 9.31163f); 
    // Start is called before the first frame update
    void Start()
    {
         object1Renderer = object1.GetComponent<Renderer>();
        object2Renderer = object2.GetComponent<Renderer>();
        object1Rigidbody = object1.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Reset color to white for both objects
            object1Renderer.material.color = Color.white;
            object2Renderer.material.color = Color.white;

            // Reset position of both objects
            object1.transform.position = resetPosition;
            object2.transform.position = resetPosition2;

            // Set velocity of object1 to a very high value
            object1Rigidbody.velocity = new Vector3(0f, -50f, 0f);
        }
    }
}
