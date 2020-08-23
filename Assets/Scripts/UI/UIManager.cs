using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider HappyBar;
    [SerializeField] private CanvasGroup BlackPanel;
    [SerializeField] private Image[] CountDownImages;
    [SerializeField] private float BlackPanelShowTransitionTime = .5f;
    [SerializeField] private float CountDownSpeed = 1f;
    [SerializeField] private GameObject DethPanelUI;
    [SerializeField] private GameObject FinishPanelUI;
    // Start is called before the first frame update
    void Start()
    {
        HappyBar.value = 100f;
        foreach (Image image in CountDownImages)
        {
            image.gameObject.SetActive(false);
        }
        DethPanelUI.GetComponent<CanvasGroup>().alpha = 0;
        FinishPanelUI.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FadeBlackPanelIn(float time = 0)
    {
        if (time == 0)
        {
            LeanTween.alphaCanvas(BlackPanel, 1, BlackPanelShowTransitionTime);
        }
        else
        {
            LeanTween.alphaCanvas(BlackPanel, 1, time);
        }
    }
    public void FadeBlackPanelOut(float time = 0)
    {
        if (time == 0)
        {
            LeanTween.alphaCanvas(BlackPanel, 0, BlackPanelShowTransitionTime);
        }
        else
        {
            LeanTween.alphaCanvas(BlackPanel, 0, time);
        }
    }
    public void SetHappyBar(float BarValue)
    {
        HappyBar.value = BarValue;
    }
    public void StartCountDown()
    {
        StartCoroutine(waitForImageCountDown(CountDownSpeed, CountDownImages));
    }
    public void Deth()
    {
        Debug.Log("The player failed");
        LeanTween.alphaCanvas(DethPanelUI.GetComponent<CanvasGroup>(), 1, .25f);
    }
    public void LevelFinished()
    {
        Debug.Log("The player finished the level");
        LeanTween.alphaCanvas(DethPanelUI.GetComponent<CanvasGroup>(), 1, .25f);
    }
    IEnumerator waitForImageCountDown(float WaitingTime, Image[] image)
    {
        image[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitingTime);
        image[0].gameObject.SetActive(false);
        image[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitingTime);
        image[1].gameObject.SetActive(false);
        image[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitingTime);
        image[2].gameObject.SetActive(false);
        image[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(WaitingTime);
        image[3].gameObject.SetActive(false);
        HappyBar.gameObject.GetComponent<BayManager>().isGettingHappyLess = true;
    }
}
