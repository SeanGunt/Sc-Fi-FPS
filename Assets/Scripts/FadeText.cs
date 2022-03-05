using UnityEngine;
using System.Collections;
using TMPro;

public class FadeText : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(FadeTextToZeroAlpha(15f, GetComponent<TextMeshPro>()));
    }
    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshPro i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
