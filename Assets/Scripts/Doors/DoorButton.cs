using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    public AudioSource doorActivate;
    public Sprite Buttonon;
    private DoorSetActive _doorScript;
    private bool _soundPlayed;

    private void Start()
    {
        _doorScript = door.GetComponent<DoorSetActive>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_soundPlayed)
        {
            doorActivate.Play();
            _soundPlayed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
             _doorScript.DoorOpen();
             gameObject.GetComponent<SpriteRenderer>().sprite = Buttonon;
        }
            
    }
}

