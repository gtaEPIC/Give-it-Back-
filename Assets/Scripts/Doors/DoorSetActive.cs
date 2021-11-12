using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour
{
    public void DoorOpen()
    {
        gameObject.SetActive(false);
    }


    public void DoorClose()
    {
        gameObject.SetActive(true);
    }

}
