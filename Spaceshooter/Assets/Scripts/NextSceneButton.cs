using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    public void NextScene(int sceneIndex)
    {
        PlayerPrefs.SetInt("FinalScore", PlayerPrefs.GetInt("FinalScore",0)+ Player.Score);
        SceneManager.LoadScene(sceneIndex);
    }
}
