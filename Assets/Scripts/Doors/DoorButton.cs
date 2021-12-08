using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    private DoorSetActive _doorScript;

    private void Start()
    {
        _doorScript = door.GetComponent<DoorSetActive>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey("e") || Input.GetKey("joystick button 2"))
            {
                _doorScript.DoorOpen();
            }


            if (Input.GetKey("r") || Input.GetKey("joystick button 3"))
            {
                _doorScript.DoorClose();
            }
        }
    }
}

