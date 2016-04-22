using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;
    public bool roundStarted = false;
    public float startSpeed = 0;


    void OnEnable()
    {
        //StartScene();
       // FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            FadeToClear();
        else
            FadeToBlack();

    }


    void FadeToClear()
    {
        if (roundStarted == false)
        {
            roundStarted = true;
            startSpeed = fadeSpeed / 2f;
        }
        else {
            startSpeed = fadeSpeed;
        }
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, startSpeed * Time.deltaTime);
        if (FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

        }
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.white, fadeSpeed * Time.deltaTime);
        if (FadeImg.color.a >= 0.95f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.white;
            //FadeImg.enabled = false;

        }
      
    }


   public  void StartScene()
    {
        FadeImg.enabled = true;
        sceneStarting = true;
    }


    public void EndScene()
    {
        FadeImg.enabled = true;
        sceneStarting = false;
            
    }
}