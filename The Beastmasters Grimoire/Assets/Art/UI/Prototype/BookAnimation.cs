using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookAnimation : MonoBehaviour
{
    public float animationTime = 10.0f;
    public float animationPause = 10.0f;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        Color color;
        while (this.GetComponent<Image>().color.a > 0)
        {
            color = image.color;
            float fade = color.a - (animationTime * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fade);
            this.GetComponent<Image>().color = color;
            yield return null;
        }

        yield return new WaitForSeconds(animationPause/2);
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        Color color;
        while (this.GetComponent<Image>().color.a < 1)
        {
            color = image.color;
            float fade = color.a + (animationTime * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fade);
            this.GetComponent<Image>().color = color;
            yield return null;
        }

        yield return new WaitForSeconds(animationPause);
        StartCoroutine(FadeOut());
    }
}
