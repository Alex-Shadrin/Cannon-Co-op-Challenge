using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed = 0.3f;
    IEnumerator Start()
    {
        Image fadeImage = GetComponent<Image>();
        Color color = fadeImage.color;

        while(color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
    }

}
