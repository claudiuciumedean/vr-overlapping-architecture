using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            ExperienceManager.Instance.setCondition(1);
            ExperienceManager.Instance.setOverlapLevel("overlap-45");
            ExperienceManager.Instance.setQuestionAnswer("Impossible");
            ExperienceManager.Instance.setAnswerTime(231.4);
            ExperienceManager.Instance.setDistanceEstimation(231);
            ExperienceManager.Instance.setEstimationTime(45.41);


            ExperienceManager.Instance.saveLog();
            ExperienceManager.Instance.loadScene();
        }
    }
}
