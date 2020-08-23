using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    [SerializeField] private GameObject Player;
    public float TimerLength = 30;
    public float Timer;
    public float TimerMultiplyer = 0;
    public bool startTimer = false;
    private void Start()
    {
        StartCoroutine(wait());
        DisablePlayer();
        TimerMultiplyer = 0;
        Timer = TimerLength;
    }
    private void Update()
    {
        if (startTimer)
        {
            startTimer = false;
            TimerMultiplyer = 1;
        }
        if (Timer > 0 && TimerMultiplyer == 1)
        {
            Timer -= TimerMultiplyer * Time.deltaTime;
        }
        else if (Timer <= 0)
        {
            TimerHasFinished();
            TimerMultiplyer = 0;
            Timer = TimerLength;
        }

    }
    IEnumerator wait()
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
        Player.GetComponent<PlayerMotor>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        uiManager.Bay.GetComponent<BayManager>().isGettingHappyLess = false;
    }
    public void EnablePlayer()
    {
        Player.GetComponent<PlayerMotor>().enabled = true;
        Player.GetComponent<PlayerController>().enabled = true;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        uiManager.Bay.GetComponent<BayManager>().isGettingHappyLess = true;
    }
    private void TimerHasFinished()
    {
        DisablePlayer();
        uiManager.LevelFinished();
    }
}
