using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class flashing : MonoBehaviour
{
    private CanvasGroup thistext;
    private void Start()
    {
        thistext = gameObject.GetComponent<CanvasGroup>();
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        LeanTween.alphaCanvas(thistext, .2f, 1);
        yield return new WaitForSeconds(1);
        LeanTween.alphaCanvas(thistext, .8f, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(wait());
    }
}
