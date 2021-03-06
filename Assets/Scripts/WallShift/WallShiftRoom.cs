﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShiftRoom : MonoBehaviour
{

    // input variables
    public float overlap = 0;
    public Transform avatar;
    public Transform corridorBoundary;
    public Transform wallA;
    public Transform wallB;
    public Transform wallMid;

    public Renderer midRender;
    public GameObject midCover;
    public GameObject hiddenA1;
    public GameObject hiddenA2;
    public GameObject hiddenB1;
    public GameObject hiddenB2;
    public GameObject coverA;
    public GameObject coverB;

    // internal variables
    int currentDirection;
    float originalPosA;
    float originalPosB;

    void Start()
    {
        ExperienceManager.Instance.setOverlapLevel(overlap);

        originalPosA = wallA.localPosition.z; // save original position of room boundary walls
        originalPosB = wallB.localPosition.z;

        shiftUpdate(); // call on start to position the walls according on initial position of the avatar
        switchHidden(true);


    }

    void Update()
    {
        if (avatar.position.x > 1f)
        {
            switchHidden(true);
        }
        if (avatar.position.x < 1f)
        {
            switchHidden(false);
        }

        if (avatar.position.x < corridorBoundary.position.x)
        {
            midCover.SetActive(true);
            midRender.enabled = true;

            return; // while avatar is in the same room do nothing
        }

        switchHidden(true);

        midCover.SetActive(false);
        shiftUpdate();

        if (avatar.position.z > -0.9 && avatar.position.z < 0.9)
        {
            coverA.SetActive(false);
            coverB.SetActive(false);
            midRender.enabled = false;
        }
        else
        {
            coverA.SetActive(true);
            coverB.SetActive(true);
            midRender.enabled = true;
        }

    }

    void shiftUpdate()
    {
        if (currentDirection != overlapDirection(avatar.position.z)) // shift the wall only if the avatar moved from A section to B section
        {
            currentDirection = overlapDirection(avatar.position.z); // override the old position

            if (currentDirection == 1)
            {
                shiftB();
            }
            else
            {
                shiftA();
            }
        }
    }

    int overlapDirection(float z)
    {
        if (z > 0) // avatar is located at B side of the room
        {
            return -1;
        }
        else    // avatar is located an A side of the room
        {
            return 1;
        }
    }

    void shiftA()
    {
        resetWalls();
        wallA.localPosition = new Vector3(wallA.localPosition.x, wallA.localPosition.y, wallA.localPosition.z + ((overlap * currentDirection) * 2));
        wallMid.localPosition = new Vector3(wallMid.localPosition.x, wallMid.localPosition.y, wallMid.localPosition.z + (overlap * currentDirection));
    }

    void shiftB()
    {
        resetWalls();
        wallB.localPosition = new Vector3(wallB.localPosition.x, wallB.localPosition.y, wallB.localPosition.z + ((overlap * currentDirection) * 2));
        wallMid.localPosition = new Vector3(wallMid.localPosition.x, wallMid.localPosition.y, wallMid.localPosition.z + (overlap * currentDirection));
    }


    void resetWalls()
    {
        wallA.localPosition = new Vector3(wallA.localPosition.x, wallA.localPosition.y, originalPosA);
        wallB.localPosition = new Vector3(wallB.localPosition.x, wallB.localPosition.y, originalPosB);
        wallMid.localPosition = new Vector3(wallMid.localPosition.x, wallMid.localPosition.y, 0);
    }

    void switchHidden(bool input)
    {
        if (currentDirection == 1)
        {
            hiddenB1.SetActive(input);
            hiddenB2.SetActive(input);
        }
        else
        {
            hiddenA1.SetActive(input);
            hiddenA2.SetActive(input);
        }
    }

}