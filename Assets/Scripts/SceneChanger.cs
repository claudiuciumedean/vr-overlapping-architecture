using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public int condition = 0;
    public GameObject checkpoint;

    void Start() {
        ExperienceManager.Instance.setCondition(this.condition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ExperienceManager.Instance.loadQuestionScene();
            Debug.Log("going to next scene");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            checkpoint.SetActive(true);
            Debug.Log("showing the checkpoint");
        }
    }
}
