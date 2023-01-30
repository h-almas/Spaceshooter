using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
      PlayerPrefs.DeleteKey("FinalScore");
      PlayerPrefs.DeleteKey("Lives");
      PlayerPrefs.DeleteKey("FinalMissedHits");
      PlayerPrefs.DeleteKey("FinalStars");
      SceneManager.LoadScene(1);
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
