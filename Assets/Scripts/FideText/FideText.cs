using UnityEngine;
using System.Collections;

public class FadeText : MonoBehaviour
{
    [Space(5)]
    [Header("Canvas Group")]
    [Space(5)]
    public CanvasGroup canvasGroup;

    [Space(10)]
    public float fadeTime = 2f;
    [Space(5)]
    public float showTime = 10f;

    IEnumerator FadeRoutine()
    {
        // fade in.
        yield return StartCoroutine(Fade(0, 1));

        // wait.
        yield return new WaitForSeconds(showTime);

        // fade out.
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = end;
    }

    public void RestartFade()
    {
        if (!gameObject.activeInHierarchy) return;

        StopAllCoroutines();
        canvasGroup.alpha = 0f;
        StartCoroutine(FadeRoutine());
    }
}