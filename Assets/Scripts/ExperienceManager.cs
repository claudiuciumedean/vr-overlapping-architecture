using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperienceManager : Singleton<ExperienceManager>
{
    public int playerId = 0;
    List<int> occlusionScenes = new List<int> { 2 };
    List<int> partialOcclusionOneScenes = new List<int> { 3 };
    List<int> partialOcclusionTwoScenes = new List<int> { 4 };
    List<int> totalOcclusionScenes = new List<int> { 5 };
    List<int> scenes = new List<int>();
    int currentScene = 0;
    bool isMerged = false;
    int condition = 0;
    string overlapLevel = "";
    string question = "";
    double answerTime = 0;
    int distanceEstimation = 0;
    double estimationTime = 0;

    private void Start()
    {
        if (!isMerged)
        {
            this.mergeScenes();
            isMerged = true;
            //foreach (int item in this.scenes) { Debug.Log(item); }
        }

        CSVManager.setFileName(this.playerId);
    }

    void mergeScenes() {
        this.scenes.AddRange(occlusionScenes);
        this.scenes.AddRange(partialOcclusionOneScenes);
        this.scenes.AddRange(partialOcclusionTwoScenes);
        this.scenes.AddRange(totalOcclusionScenes);

        Randomizer.Shuffle(this.scenes);
    }

    public void loadStartScene() {
        SceneManager.LoadScene("StartScene");
    }

    public void loadQuestionScene() {
        SceneManager.LoadScene("QuestionScene");
    }

    public void loadScene() {
        Debug.Log(this.currentScene); 
        if(this.currentScene == this.scenes.Count) {
            this.loadStartScene();
            return;
        }

        SceneManager.LoadScene(this.scenes[this.currentScene]);
        this.currentScene++;
    }

    public void setCondition(int condition)
    {
        this.condition = condition;
    }

    public void setOverlapLevel(string overlapLevel)
    {
        this.overlapLevel = overlapLevel;
    }

    public void setQuestionAnswer(string question)
    {
        this.question = question;
    }

    public void setAnswerTime(double time) {
        this.answerTime = time;
    }

    public void setDistanceEstimation(int dist) {
        this.distanceEstimation = dist;
    }

    public void setEstimationTime(double time) {
        this.estimationTime = time;
    }

    public void saveLog()
    {
        CSVManager.AppendToReport(new string[] { 
            this.playerId.ToString(),
            this.condition.ToString(),
            this.overlapLevel,
            this.question,
            this.answerTime.ToString(),
            this.distanceEstimation.ToString(),
            this.estimationTime.ToString()
        });
    }
}
