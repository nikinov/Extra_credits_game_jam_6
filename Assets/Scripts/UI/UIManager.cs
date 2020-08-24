using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider HappyBar;
    [SerializeField] private CanvasGroup BlackPanel;
    [SerializeField] private Image[] CountDownImages;
    [SerializeField] private float BlackPanelShowTransitionTime = .5f;
    [SerializeField] private float CountDownSpeed = 1f;
    [SerializeField] private GameObject DethPanelUI;
    [SerializeField] private GameObject FinishPanelUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI TimerUI;
    public GameObject Bay;

    // Start is called before the first frame update
    void Start()
    {
        DethPanelUI.SetActive(false);
        FinishPanelUI.SetActive(false);
        HappyBar.value = 100f;
        foreach (Image image in CountDownImages)
        {
            image.gameObject.SetActive(false);
        }
        DethPanelUI.GetComponent<CanvasGroup>().alpha = 0;
        FinishPanelUI.GetComponent<CanvasGroup>().alpha = 0;
        TimerUI.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.TimerMultiplyer == 1)
        {
            TimerUI.text = (Mathf.Round(gameManager.Timer * 10) / 10).ToString();
        }
    }
    public void FadeBlackPanelIn(float time = 0)
    {
        BlackPanel.gameObject.SetActive(true);
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
        if (time <= 0)
        {
            LeanTween.alphaCanvas(BlackPanel, 0, BlackPanelShowTransitionTime);
            StartCoroutine(waitB(BlackPanelShowTransitionTime));
        }
        else
        {
            LeanTween.alphaCanvas(BlackPanel, 0, time);
            StartCoroutine(waitB(time));
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
        Cursor.lockState = CursorLockMode.None;

        DethPanelUI.SetActive(true);
        Debug.Log("The player failed");
        LeanTween.alphaCanvas(DethPanelUI.GetComponent<CanvasGroup>(), 1, .5f);
        gameManager.TimerMultiplyer = 0;
        gameManager.Timer = gameManager.TimerLength;
        gameManager.DisablePlayer();
    }
    public void LevelFinished()
    {
        Cursor.lockState = CursorLockMode.None;
        
        FinishPanelUI.SetActive(true);
        Debug.Log("The player finished the level");
        LeanTween.alphaCanvas(FinishPanelUI.GetComponent<CanvasGroup>(), 1, .25f);
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
        gameManager.EnablePlayer();
        TimerUI.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameManager.startTimer = true;
    }
    IEnumerator waitB(float tim)
    {
        yield return new WaitForSeconds(tim);
        BlackPanel.gameObject.SetActive(false);
    }
}
