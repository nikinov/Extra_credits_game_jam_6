using System.Collections;
using Baby;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class GameManager : MonoBehaviour
    {
        public UIManager uiManager;
        public GameObject player;
        public float timerLength = 30;
        public float timer;
        public bool startTimer = false;
        public float timerMultiplier = 0;

        private void Start()
        {
            StartCoroutine(Wait());
            DisablePlayer();
            timerMultiplier = 0;
            timer = timerLength;
        }

        private void Update()
        {
            if (startTimer)
            {
                startTimer = false;
                timerMultiplier = 1;
            }
            if (timer > 0 && timerMultiplier == 1.0f)
            {
                timer -= timerMultiplier * Time.deltaTime;
            }
            else if (timer <= 0)
            {
                TimerHasFinished();
                timerMultiplier = 0;
                timer = timerLength;
            }

        }

        IEnumerator Wait()
        {
            uiManager.FadeBlackPanelOut(1);
            yield return new WaitForSeconds(1);
            uiManager.StartCountDown();
        }
        public void Restart()
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        }
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        public void DisablePlayer()
        {
            player.GetComponent<PlayerMotor>().enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            uiManager.baby.GetComponent<BabyManager>().isGettingHappyLess = false;
        }
        public void EnablePlayer()
        {
            player.GetComponent<PlayerMotor>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            uiManager.baby.GetComponent<BabyManager>().isGettingHappyLess = true;
        }
        private void TimerHasFinished()
        {
            DisablePlayer();
            uiManager.LevelFinished();
        }
    }
}
