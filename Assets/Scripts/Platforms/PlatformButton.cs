using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    [SerializeField] private PlatformSetActive platform;
    public void start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey("e"))
        {
            platform.PlatformVisable();
        }


        if (Input.GetKey("r"))
        {
            platform.PlatformInvisable();
        }
    }
}
