using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowTest : MonoBehaviour
{
    public GameObject objectToBeThrown;
    public float power = 10;
    public Vector3 throwPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            GameObject newThrownObject = Instantiate(objectToBeThrown, throwPosition, Quaternion.identity);
            Rigidbody rb = newThrownObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(Random.Range(-power, power), Random.Range(0, power), Random.Range(-power, power)), ForceMode.Impulse);
        }
    }
}
