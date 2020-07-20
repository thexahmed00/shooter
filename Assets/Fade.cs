using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(fadeIn());
    }
    public void fadeTo(int scene)
    {
        StartCoroutine(fadeOut(scene));
    }

    IEnumerator fadeIn()
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0 ,0 , 0, a);
            yield return 0;
        }
    }
    IEnumerator fadeOut(int scene)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        

        }
        SceneManager.LoadScene(scene);
    }
}
