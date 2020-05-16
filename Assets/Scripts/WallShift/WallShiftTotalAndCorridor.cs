using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShiftTotalAndCorridor : MonoBehaviour
{

    // input variables
    public float overlap = 0;
    public Transform avatar;
    public Transform corridorBoundary;
    public Transform wallA;
    public Transform wallB;
    public GameObject wallMid;
    public GameObject coverA;
    public GameObject coverB;

    /*public Renderer midRender;
    public Renderer coverARender;
    public Renderer coverBRender;*/


    // internal variables
    int currentDirection;
    float originalPosA;
    float originalPosB;
    bool coverAlive;

    void Start()
    {
        ExperienceManager.Instance.setOverlapLevel(overlap);
        
        originalPosA = wallA.localPosition.z; // save original position of room boundary walls
        originalPosB = wallB.localPosition.z;
        coverAlive = true; // cover function enabled by default

        shiftUpdate(); // call on start to position the walls according on initial position of the avatar

        if(overlap < 0.225f) // on overlap below 45% cover is disabled
        {
            Destroy(coverA);
            Destroy(coverB);
            coverAlive = false;
        }

        coverA.SetActive(false);
        coverB.SetActive(false);

    }

    void Update()
    {
        if (avatar.position.x < corridorBoundary.position.x)
        {

            if (coverAlive)
            {
                if (!wallMid.activeSelf)
                {
                    wallMid.SetActive(true);
                }
                if (currentDirection == 1 && !coverB.activeSelf)
                {
                    coverB.SetActive(true);
                }
                if (currentDirection == -1 && !coverA.activeSelf)
                {
                    coverA.SetActive(true);
                }
            }
           
            return; // while avatar is in the same room do nothing
        }

        if (coverAlive)
        {
            if (coverA.activeSelf || coverB.activeSelf)
            {
                coverA.SetActive(false);
                coverB.SetActive(false);
            }
            if (avatar.position.z > -0.9 && avatar.position.z < 0.9)
            {
                wallMid.SetActive(false);
            }
            else
            {
                wallMid.SetActive(true);
            }
        }

        shiftUpdate();

    }

    void shiftUpdate()
    {
        if(currentDirection != overlapDirection(avatar.position.z)) // shift the wall only if the avatar moved from A section to B section
        {
            currentDirection = overlapDirection(avatar.position.z); // override the old position
            
            if(currentDirection == 1)
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
        if(z > 0) // avatar is located at B side of the room
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
        wallA.localPosition = new Vector3(wallA.localPosition.x, wallA.localPosition.y, wallA.localPosition.z + ((overlap * currentDirection) * 2) );
        wallMid.transform.localPosition = new Vector3(wallMid.transform.localPosition.x, wallMid.transform.localPosition.y, wallMid.transform.localPosition.z + (overlap * currentDirection));
    }

    void shiftB()
    {
        resetWalls();
        wallB.localPosition = new Vector3(wallB.localPosition.x, wallB.localPosition.y, wallB.localPosition.z + ((overlap * currentDirection) * 2) );
        wallMid.transform.localPosition = new Vector3(wallMid.transform.localPosition.x, wallMid.transform.localPosition.y, wallMid.transform.localPosition.z + (overlap * currentDirection));

    }

    void resetWalls()
    {
        wallA.localPosition = new Vector3(wallA.localPosition.x, wallA.localPosition.y, originalPosA);
        wallB.localPosition = new Vector3(wallB.localPosition.x, wallB.localPosition.y, originalPosB);
        wallMid.transform.localPosition = new Vector3(wallMid.transform.localPosition.x, wallMid.transform.localPosition.y, 0);
    }

}