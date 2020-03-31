using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class occlusionSetting : MonoBehaviour
{
    // input variables
    public GameObject testPrefab;

    // internal variables
    int extra;
    Transform wallM;
    Transform wallMspawn;
    Transform wallMend;
    GameObject[] wallMclones;
    
    // Start is called before the first frame update
    void Start()
    {   
        wallM = GameObject.Find("WalkingSpace/wallsOverlap/wallMiddle").GetComponent<Transform>();
        wallMspawn = GameObject.Find("WalkingSpace/wallsOverlap/wallMiddle/spawnPoint").GetComponent<Transform>();
        wallMend = GameObject.Find("WalkingSpace/wallsOverlap/wallMiddle/spawnEnd").GetComponent<Transform>();

        if(transform.localScale.x % 2 != 0)
        {
            extra = 1;
        }

        else
        {
            extra = 0;
        }

        wallMclones = new GameObject[((int)(Vector3.Distance(wallMspawn.position, wallMend.position)) * 2) + extra];
       for(int i = 0; i < wallMclones.Length / 2; i++)
       {
           wallMclones[i] = Instantiate(testPrefab, new Vector3(wallMspawn.position.x + i, wallM.position.y, wallM.position.z), Quaternion.identity);
           wallMclones[wallMclones.Length - i -1] = Instantiate(testPrefab, new Vector3(wallMspawn.position.x + i + 0.5f, wallM.position.y, wallM.position.z), Quaternion.identity);
           wallMclones[i].transform.parent = wallM;
           wallMclones[wallMclones.Length - i -1].transform.parent = wallM;

           if( extra != 0 && i == wallMclones.Length / 2 -  1)
           {
               wallMclones[i + extra] = Instantiate(testPrefab, new Vector3(wallMspawn.position.x + i + 1f, wallM.position.y, wallM.position.z), Quaternion.identity);
               wallMclones[i + extra].transform.parent = wallM;
           }
       } 
    }

   
}
