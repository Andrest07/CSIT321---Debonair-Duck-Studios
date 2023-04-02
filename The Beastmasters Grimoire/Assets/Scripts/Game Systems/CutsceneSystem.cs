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
    private GameObject[] slides;
    public GameObject canvas;

    public float[] slidesDurations;

    private int currentSlide = 0;
    public float fadeMultiplier = 1f;
    public bool playOnStart;
    public bool transitionSceneOnEnd;
    public string nextScene;


    private void Awake()
    {
        int children = canvas.transform.childCount - 2;
        slides = new GameObject[children];

        // get slides from canvas (excluding BG slide)
        for (int i = 0; i < children - 1; ++i)
            slides[i] = canvas.transform.GetChild(i + 1).gameObject;
    }

    void Start()
    {
        if (slidesDurations.Length < slides.Length) Debug.LogError("Slides != SlidesDurations");

        if (playOnStart) StartCoroutine(Cutscene());
    }

    public void Play(){
        StartCoroutine(Cutscene());
    }

    public void Skip()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(nextScene);
    }

    public IEnumerator Cutscene()
    {
        StartCoroutine(FadeIn(slides[currentSlide]));

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

