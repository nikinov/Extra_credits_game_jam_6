using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    private void Start()
    {
        StartCoroutine(wait());
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
}
