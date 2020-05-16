using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShift0 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExperienceManager.Instance.setOverlapLevel(0);
        enabled = false;
    }

}
