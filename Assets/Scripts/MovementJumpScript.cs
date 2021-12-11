using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJumpScript : MonoBehaviour
{
    public AudioSource playerMovementLeftSFX;
    public AudioSource playerMovementRightSFX;
    public AudioSource playerJumpSFX;
    void Update()
    {
        if (Input.GetKey("left"))
        {
            playerMovementLeftSFX.enabled = true;
            playerMovementLeftSFX.loop = true;
        }
        else
        {
            playerMovementLeftSFX.enabled = false;
            playerMovementLeftSFX.loop = false;
        }

        if (Input.GetKey("right"))
        {
            playerMovementRightSFX.enabled = true;
            playerMovementRightSFX.loop = true;
        }
        else
        {
            playerMovementRightSFX.enabled = false;
            playerMovementRightSFX.loop = false;
        }


        if (Input.GetKeyDown("up"))
        {
            playerJumpSFX.Play();
        }
    }
}
