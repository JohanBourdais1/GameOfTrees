using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class VrAndPcButton : Button
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            onClick.Invoke();
            // Call your function or perform actions here
        }

    }
}
