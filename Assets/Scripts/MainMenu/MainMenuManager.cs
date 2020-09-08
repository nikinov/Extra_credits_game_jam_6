using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup blackPanel;
        [SerializeField] private float blackPanelShowTransitionTime = 1f;
        [SerializeField] private GameObject movingNewsText;
        [SerializeField] private GameObject backgroundMovingNewsText;
        [SerializeField] private float movingNewsTextSpeed = 100;
        private GameObject _go;
        private readonly List<GameObject> _existingText = new List<GameObject>();

        void Start()
        {
            FadeBlackPanelOut();
            _go = Instantiate(movingNewsText, backgroundMovingNewsText.transform);
            _go.GetComponent<TextMove>().textSpeed = movingNewsTextSpeed;
            _existingText.Add(_go);
            StartCoroutine(Wait());
        }

        public void OnPlayer()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        public void FadeBlackPanelIn(float time = 0)
        {
            blackPanel.gameObject.SetActive(true);
            LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 1, time == 0 ? blackPanelShowTransitionTime : time);
        }
        public void FadeBlackPanelOut(float time = 0)
        {
            if (time <= 0)
            {
                LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 0, blackPanelShowTransitionTime);
                StartCoroutine(WaitB(blackPanelShowTransitionTime));
            }
            else
            {
                LeanTween.Framework.LeanTween.alphaCanvas(blackPanel, 0, time);
                StartCoroutine(WaitB(time));
            }
        }
        public void StartTheGame()
        {

        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds((_go.GetComponent<RectTransform>().rect.width*2.1f) / movingNewsTextSpeed);
            _go = Instantiate(movingNewsText, backgroundMovingNewsText.transform);
            _go.GetComponent<TextMove>().textSpeed = movingNewsTextSpeed;
            _existingText.Add(_go);
            StartCoroutine(Wait2());
        }
        IEnumerator Wait2()
        {
            yield return new WaitForSeconds((_go.GetComponent<RectTransform>().rect.width * 2.2f) / movingNewsTextSpeed);
            _go = Instantiate(movingNewsText, backgroundMovingNewsText.transform);
            _go.GetComponent<TextMove>().textSpeed = movingNewsTextSpeed;
            _existingText.Add(_go);
            Destroy(_existingText[0]);
            _existingText.Remove(_existingText[0]);
            StartCoroutine(Wait2());
        }
        IEnumerator WaitB(float time)
        {
            yield return new WaitForSeconds(time);
            blackPanel.gameObject.SetActive(false);
        }
    }
}
