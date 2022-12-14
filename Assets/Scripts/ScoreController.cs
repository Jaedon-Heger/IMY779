using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    string scoreTrackingFile;

    public ScoreData newScoreData;
    public AllScoreDataContainer allScoreDataContainer;

    private void Start()
    {
        scoreTrackingFile = Application.persistentDataPath + "/scores.json";
        Debug.Log("Saving data to file location '" + scoreTrackingFile + "'");
        InitialiseAllScoreData();
        InitialiseNewScoreData();
    }

    public void AddToScore(int amountToAdd)
    {
        newScoreData.numberOfShotsOnTarget++;
        newScoreData.totalScore += amountToAdd;
    }    
    
    public void IncrementNumberOfShots()
    {
        newScoreData.numberOfShots++;
    }


    public void InitialiseAllScoreData()
    {
        Debug.Log("Initialising all scores list");
        if (File.Exists(scoreTrackingFile))
        {
            string fileContents = File.ReadAllText(scoreTrackingFile);
            allScoreDataContainer = JsonUtility.FromJson<AllScoreDataContainer>(fileContents);

            Debug.Log("Found: "+ allScoreDataContainer.scoreDataObjects.Count +" score objects");

        } else
        {
            allScoreDataContainer = new AllScoreDataContainer();
           
        }
    }    
    
    public void InitialiseNewScoreData()
    {
        newScoreData = new ScoreData();
        newScoreData.user = "User-"+ allScoreDataContainer.scoreDataObjects.Count;
    }

    public void SaveScoreData()
    {
        Debug.Log("Saving score");
        allScoreDataContainer.scoreDataObjects.Add(newScoreData);
        string scoreDataJsonString = JsonUtility.ToJson(allScoreDataContainer);
        File.WriteAllText(scoreTrackingFile, scoreDataJsonString);
    }
}
