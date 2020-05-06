using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public int condition = 0;

    void Start() {
        ExperienceManager.Instance.setCondition(this.condition);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ExperienceManager.Instance.loadQuestionScene();
            Debug.Log("space key was pressed");
        }
    }
}
