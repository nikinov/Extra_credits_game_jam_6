using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup BlackPanel;
    [SerializeField] private float BlackPanelShowTransitionTime = 1f;
    [SerializeField] private GameObject MovingNewsText;
    [SerializeField] private GameObject BackgroundMovingNewsText;
    [SerializeField] private float MovingNewsTextSpeed = 100;
    private GameObject go;
    private List<GameObject> existingText = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        FadeBlackPanelOut();
        go = Instantiate(MovingNewsText, BackgroundMovingNewsText.transform);
        go.GetComponent<TextMove>().textSpeed = MovingNewsTextSpeed;
        existingText.Add(go);
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPlayer()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
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
    public void StartTheGame()
    {

    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds((go.GetComponent<RectTransform>().rect.width*2.1f) / MovingNewsTextSpeed);
        go = Instantiate(MovingNewsText, BackgroundMovingNewsText.transform);
        go.GetComponent<TextMove>().textSpeed = MovingNewsTextSpeed;
        existingText.Add(go);
        StartCoroutine(wait2());
    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds((go.GetComponent<RectTransform>().rect.width * 2.2f) / MovingNewsTextSpeed);
        go = Instantiate(MovingNewsText, BackgroundMovingNewsText.transform);
        go.GetComponent<TextMove>().textSpeed = MovingNewsTextSpeed;
        existingText.Add(go);
        Destroy(existingText[0]);
        existingText.Remove(existingText[0]);
        StartCoroutine(wait2());
    }
}
