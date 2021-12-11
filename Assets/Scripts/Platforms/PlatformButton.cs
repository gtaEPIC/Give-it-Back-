using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public GameObject[] platforms;
    public Sprite Buttonon;
    public AudioSource platformActivate;
    private PlatformSetActive[] _platformsSetActive;
    public void Start()
    {
        _platformsSetActive = new PlatformSetActive[platforms.Length];
        for (var i = 0; i < platforms.Length; i++)
        {
            GameObject platform = platforms[i];
            _platformsSetActive[i] = platform.GetComponent<PlatformSetActive>();
            _platformsSetActive[i].PlatformInvisable();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             foreach (PlatformSetActive platform in _platformsSetActive)
             {
                  platform.PlatformVisable();
                  this.gameObject.GetComponent<SpriteRenderer>().sprite = Buttonon;
                platformActivate.Play();
            }
        }  
    }
}
