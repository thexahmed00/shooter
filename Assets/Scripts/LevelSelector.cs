using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public Fade sceneloader;
    public Button[] levelButtons;
    private void Start()
    {
       
        int levelReached = PlayerPrefs.GetInt("LevelReached",2);
        for (int i = 0; i <levelButtons.Length; i++)
        {
           if(i+2>levelReached)
            levelButtons[i].interactable = false;
        }
    }
    public void LoadLevel(string scene)
    {
        //SceneManager.LoadScene(1);
        sceneloader.fadeTo(scene);
        
    }
}
