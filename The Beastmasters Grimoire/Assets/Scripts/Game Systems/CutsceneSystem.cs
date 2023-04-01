/*
AUTHOR DD/MM/YY: Kaleb 12/01/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSystem : MonoBehaviour
{
    public GameObject[] slides;

    public float[] slidesDurations;

    private int currentSlide = 0;
    public float fadeMultiplier =1f;
    public bool playOnStart;
    public bool transitionSceneOnEnd;
    public string nextScene;
    void Start()
    {
        if(playOnStart) StartCoroutine(Cutscene());
    }

    public void Play(){
        StartCoroutine(Cutscene());
    }

    public IEnumerator Cutscene()
    {
        //While there are more slides
        while (currentSlide < slides.Length - 1)
        {
            //Let the current slide play for it's duration and then swap to the next slide;
            yield return new WaitForSecondsRealtime(slidesDurations[currentSlide]);

            //Move to the next slide fading it in and fading out the last slide
            currentSlide++;
            StartCoroutine(FadeIn(slides[currentSlide]));
            StartCoroutine(FadeOut(slides[currentSlide - 1]));
        }
        yield return new WaitForSecondsRealtime(slidesDurations[currentSlide]); // Let the final slide play then load the next scene
        //If this cutscene loads a scene, load that scene. Otherwise destroy the cutscene manager
        if(transitionSceneOnEnd) SceneManager.LoadScene(nextScene); 
        else Destroy(gameObject);
    }

    //Methods for fading in and out slides.
    public IEnumerator FadeIn(GameObject slide)
    {
        while (slide.GetComponent<CanvasGroup>().alpha < 1)
        {
            slide.GetComponent<CanvasGroup>().alpha += Time.unscaledDeltaTime*fadeMultiplier;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeOut(GameObject slide)
    {
        while (slide.GetComponent<CanvasGroup>().alpha > 0)
        {
            slide.GetComponent<CanvasGroup>().alpha -= Time.unscaledDeltaTime*fadeMultiplier;
            yield return new WaitForEndOfFrame();
        }
    }
}

