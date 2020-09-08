using System.Collections;
using UnityEngine;

namespace UI
{
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
            LeanTween.Framework.LeanTween.alphaCanvas(thistext, .2f, 1);
            yield return new WaitForSeconds(1);
            LeanTween.Framework.LeanTween.alphaCanvas(thistext, .8f, 1);
            yield return new WaitForSeconds(1);
            StartCoroutine(wait());
        }
    }
}
