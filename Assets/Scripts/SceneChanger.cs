using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ExperienceManager.Instance.loadQuestionScene();
            Debug.Log("space key was pressed");
        }
    }
}
