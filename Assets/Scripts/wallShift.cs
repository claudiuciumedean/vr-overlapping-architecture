using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallShift : MonoBehaviour
{

    // input variables
    public float overlap = 0;
    public Transform avatar;
    public Transform corridorBoundary;
    public Transform wallA;
    public Transform wallB;
    public Transform wallMid;


    // internal variables
    int currentDirection;
    float originalPosA;
    float originalPosB;

    void Start()
    {
        originalPosA = wallA.localPosition.z;
        originalPosB = wallB.localPosition.z;

        shiftUpdate(); // call on start to position the walls according on initial position of the avatar

    }

    void Update()
    {
        if(avatar.position.x < corridorBoundary.position.x)
        {
            return; // while avatar is in the same room do nothing
        }

        shiftUpdate();

    }

    void shiftUpdate()
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
        wallMid.localPosition = new Vector3(wallMid.localPosition.x, wallMid.localPosition.y, wallMid.localPosition.z + (overlap * currentDirection));
    }

    void shiftB()
    {
        resetWalls();
        wallB.localPosition = new Vector3(wallB.localPosition.x, wallB.localPosition.y, wallB.localPosition.z + ((overlap * currentDirection) * 2) );
        wallMid.localPosition = new Vector3(wallMid.localPosition.x, wallMid.localPosition.y, wallMid.localPosition.z + (overlap * currentDirection));
    }

    void resetWalls()
    {
        wallA.localPosition = new Vector3(wallA.localPosition.x, wallA.localPosition.y, originalPosA);
        wallB.localPosition = new Vector3(wallB.localPosition.x, wallB.localPosition.y, originalPosB);
        wallMid.localPosition = new Vector3(wallMid.localPosition.x, wallMid.localPosition.y, 0);
    }

}
