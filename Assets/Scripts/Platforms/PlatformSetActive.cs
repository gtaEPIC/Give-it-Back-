using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSetActive : MonoBehaviour
{
    
    public void PlatformVisable()
    {
        gameObject.SetActive(true);
    }


    public void PlatformInvisable()
    {
        gameObject.SetActive(false);
    }

}