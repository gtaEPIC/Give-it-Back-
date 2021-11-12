using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private DoorSetActive door;


    private void OnTriggerEnter2D(Collider2D button)
    {
        if(Input.GetKey("e"))
        {
            door.DoorOpen();
        }


        if (Input.GetKey("r"))
        {
            door.DoorClose();
        }
    }
}

