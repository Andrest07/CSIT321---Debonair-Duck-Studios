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
    public string nextScene;
    public float fadeMultiplier =1f;
    void Start()
    {
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
            yield return new WaitForSeconds(slidesDurations[currentSlide]);

            currentSlide++;

            StartCoroutine(FadeIn(slides[currentSlide]));
            StartCoroutine(FadeOut(slides[currentSlide - 1]));
        }
        yield return new WaitForSeconds(slidesDurations[currentSlide]); // Let the final slide play then load the next scene
        SceneManager.LoadScene(nextScene); //Can replace / hardcode with First level
    }

    public IEnumerator FadeIn(GameObject slide)
    {
        while (slide.GetComponent<CanvasGroup>().alpha < 1)
        {
            slide.GetComponent<CanvasGroup>().alpha += Time.deltaTime*fadeMultiplier;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator FadeOut(GameObject slide)
    {
        while (slide.GetComponent<CanvasGroup>().alpha > 0)
        {
            slide.GetComponent<CanvasGroup>().alpha -= Time.deltaTime*fadeMultiplier;
            yield return new WaitForFixedUpdate();
        }
    }
}

