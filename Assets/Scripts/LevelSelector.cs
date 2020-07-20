using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public Fade sceneloader;
    
    public void LoadLevel(int scene)
    {
        //SceneManager.LoadScene(1);
        sceneloader.fadeTo(scene);
        
    }
}
