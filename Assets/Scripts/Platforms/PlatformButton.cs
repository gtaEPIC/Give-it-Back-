using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public GameObject[] platforms;
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

    private void Update()
    {
        if (Input.GetKey("e"))
        {
            foreach (PlatformSetActive platform in _platformsSetActive)
            {
                platform.PlatformVisable();
            }
        }


        if (Input.GetKey("r"))
        {
            foreach (PlatformSetActive platform in _platformsSetActive)
            {
                platform.PlatformInvisable();
            }
        }
    }
}
