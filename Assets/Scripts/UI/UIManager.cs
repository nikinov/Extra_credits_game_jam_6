using System.Collections;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider happyBar;
        [SerializeField] private CanvasGroup blackPanel;
        [SerializeField] private Image[] countDownImages;
        [SerializeField] private float blackPanelShowTransitionTime = .5f;
        [SerializeField] private float countDownSpeed = 1f;
        [SerializeField] private GameObject dethPanelUI;
        [SerializeField] private GameObject finishPanelUI;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TextMeshProUGUI timerUI;
        public GameObject baby;

        // Start is called before the first frame update
        void Start()
        {
            dethPanelUI.SetActive(false);
            finishPanelUI.SetActive(false);
            happyBar.value = 100f;
            foreach (Image image in countDownImages)
            {
                image.gameObject.SetActive(false);
            }
            dethPanelUI.GetComponent<CanvasGroup>().alpha = 0;
            finishPanelUI.GetComponent<CanvasGroup>().alpha = 0;
            timerUI.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(gameManager.timerMultiplier == 1)
            {
                timerUI.text = (Mathf.Round(gameManager.timer * 10) / 10).ToString();
            }
        }
        public void FadeBlackPanelIn(float time = 0)
        {
            blackPanel.gameObject.SetActive(true);
            if (time == 0)
            {
                LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 1, blackPanelShowTransitionTime);
            }
            else
            {
                LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 1, time);
            }
        }
        public void FadeBlackPanelOut(float time = 0)
        {
            if (time <= 0)
            {
                LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 0, blackPanelShowTransitionTime);
                StartCoroutine(waitB(blackPanelShowTransitionTime));
            }
            else
            {
                LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 0, time);
                StartCoroutine(waitB(time));
            }
        }
        public void SetHappyBar(float barValue)
        {
            happyBar.value = barValue;
        }
        public void StartCountDown()
        {
            StartCoroutine(WaitForImageCountDown(countDownSpeed, countDownImages));
        }
        public void Death()
        {
            Cursor.lockState = CursorLockMode.None;

            dethPanelUI.SetActive(true);
            Debug.Log("The player failed");
            LeanTween.Framework.LeanTween.alphaCanvas(dethPanelUI.GetComponent<CanvasGroup>(), 1, .5f);
            gameManager.timerMultiplier = 0;
            gameManager.timer = gameManager.timerLength;
            gameManager.DisablePlayer();
        }
        public void LevelFinished()
        {
            Cursor.lockState = CursorLockMode.None;
        
            finishPanelUI.SetActive(true);
            Debug.Log("The player finished the level");
            LeanTween.Framework.LeanTween.alphaCanvas(finishPanelUI.GetComponent<CanvasGroup>(), 1, .25f);
        }
        IEnumerator WaitForImageCountDown(float waitingTime, Image[] image)
        {
            image[0].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitingTime);
            image[0].gameObject.SetActive(false);
            image[1].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitingTime);
            image[1].gameObject.SetActive(false);
            image[2].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitingTime);
            image[2].gameObject.SetActive(false);
            image[3].gameObject.SetActive(true);
            yield return new WaitForSeconds(waitingTime);
            image[3].gameObject.SetActive(false);
            gameManager.EnablePlayer();
            timerUI.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameManager.startTimer = true;
        }
        IEnumerator waitB(float tim)
        {
            yield return new WaitForSeconds(tim);
            blackPanel.gameObject.SetActive(false);
        }
    }
}
