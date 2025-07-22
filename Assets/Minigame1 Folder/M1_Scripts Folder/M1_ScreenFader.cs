using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class M1_ScreenFader : MonoBehaviour
{
    public Image fadeImage;

    public IEnumerator FadeOut(float duration)
    {
        float tt = 0f;
        Color c = fadeImage.color;
        while (tt < duration)
        {
            tt += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, tt / duration);
            fadeImage.color = c;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration)
    {
        float tt = 0f;
        Color c = fadeImage.color;
        while (tt < duration)
        {
            tt += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, tt / duration);
            fadeImage.color = c;
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
