using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    public Sprite Buttonon;
    private DoorSetActive _doorScript;

    private void Start()
    {
        _doorScript = door.GetComponent<DoorSetActive>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {

            if(other.gameObject.CompareTag("Player"))
            {
                _doorScript.DoorOpen();
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Buttonon;
            }
            
    }
}

