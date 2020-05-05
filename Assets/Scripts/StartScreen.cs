using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ExperienceManager.Instance.loadScene();
            Debug.Log("space key was pressed");
        }
    }
}
