﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperienceManager : Singleton<ExperienceManager>
{
    public int playerId;
    public int trials = 5;
    List<int> partialOcclusionOneScenes = new List<int> { 2, 3, 4, 5, 6, 7 };
    List<int> occlusionScenes = new List<int> { 8, 9, 10, 11, 12, 13 };
    List<int> partialOcclusionTwoScenes = new List<int> { 14, 15, 16, 17, 18, 19 };
    List<int> scenes = new List<int>();
    int currentScene = 0;
    bool isMerged = false;
    int condition = 0;
    string overlapLevel = "";
    string answer = "";
    float answerTime = 0;
    float distanceEstimation = 0;
    float distanceEstimationTime = 0;

    private void Start()
    {
        if (!isMerged)
        {
            this.mergeScenes();
            isMerged = true;
        }

        CSVManager.setFileName(this.playerId);
    }

    void mergeScenes()
    {
        for (int i = 0; i < this.trials; i++)
        {
            this.occlusionScenes.ForEach(s => this.scenes.Add(s));
            this.partialOcclusionOneScenes.ForEach(s => this.scenes.Add(s));
            this.partialOcclusionTwoScenes.ForEach(s => this.scenes.Add(s));
        }

        Randomizer.Shuffle(this.scenes);
        Debug.Log(this.scenes.Count + "total scenes");
    }

    public void loadStartScene() {
        SceneManager.LoadScene("StartScene");
    }

    public void loadQuestionScene() {
        SceneManager.LoadScene("QuestionScene");
    }

    public void startExperiment()
    {
        SceneManager.LoadScene(this.scenes[this.currentScene]);
        this.currentScene++;
    }

    public void loadScene() {
        Debug.Log(this.currentScene + "current scenes");
        if (this.currentScene == this.scenes.Count) {
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

    public void setOverlapLevel(float overlapLevel)
    {
        //0.075 = 15
        //0.15 = 30
        //0.225 = 45
        //0.3 = 60
        //0.375 = 75
        switch (overlapLevel)
        {
            case 0.075f:
                this.overlapLevel = "overlap-15";
                break;

            case 0.15f:
                this.overlapLevel = "overlap-30";
                break;

            case 0.225f:
                this.overlapLevel = "overlap-45";
                break;

            case 0.3f:
                this.overlapLevel = "overlap-60";
                break;

            case 0.375f:
                this.overlapLevel = "overlap-75";
                break;

            default:
                this.overlapLevel = "overlap-0";
                break;
        }
    }

    public void setAnswer(string answer)
    {
        this.answer = answer;
    }

    public void setAnswerTime(float time) {
        this.answerTime = time;
    }

    public void setDistanceEstimation(float dist) {
        this.distanceEstimation = dist;
    }

    public void setDistanceEstimationTime(float time) {
        this.distanceEstimationTime = time;
    }

    public void saveLog()
    {
        CSVManager.AppendToReport(new string[] { 
            this.playerId.ToString(),
            this.condition.ToString(),
            this.overlapLevel,
            this.answer,
            this.answerTime.ToString(),
            this.distanceEstimation.ToString(),
            this.distanceEstimationTime.ToString()
        });
    }
}
