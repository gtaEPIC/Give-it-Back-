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
            if(Input.GetKey("e"))
            {
                _doorScript.DoorOpen();
            }


            if (Input.GetKey("r"))
            {
                _doorScript.DoorClose();
            }
        }
    }
}

