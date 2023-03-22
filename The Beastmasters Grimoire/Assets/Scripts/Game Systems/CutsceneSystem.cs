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
    // Start is called before the first frame update

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

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Cutscene()
    {
        while (currentSlide < slides.Length - 1)
        {
            //Let the current slide play for it's duration and then swap to the next slide;
            yield return new WaitForSecondsRealtime(slidesDurations[currentSlide]);

            currentSlide++;

            StartCoroutine(FadeIn(slides[currentSlide]));
            StartCoroutine(FadeOut(slides[currentSlide - 1]));
        }
        yield return new WaitForSecondsRealtime(slidesDurations[currentSlide]); // Let the final slide play then load the next scene
        if(transitionSceneOnEnd) SceneManager.LoadScene(nextScene); 
        else Destroy(gameObject);
    }

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

