using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider HappyBar;
    [SerializeField] private CanvasGroup BlackPanel;
    [SerializeField] private float BlackPanelShowTransitionTime = .5f;
    // Start is called before the first frame update
    void Start()
    {
        FadeBlackPanelOut();
        HappyBar.value = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeBlackPanelIn()
    {
        LeanTween.alphaCanvas(BlackPanel, 1, BlackPanelShowTransitionTime);
    }
    public void FadeBlackPanelOut()
    {
        LeanTween.alphaCanvas(BlackPanel, 0, BlackPanelShowTransitionTime);
    }
    public void SetHappyBar(float BarValue)
    {
        HappyBar.value = BarValue;
    }
    public void Deth()
    {

    }
}
