using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BayManager : MonoBehaviour
{
    private float HowHappyIsBay;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private float HowFastWillBayHappynesGoDown = .1f;
    [SerializeField] private GameObject WrongToyText;
    [SerializeField] private Sprite[] ExistingToyTypeImages;
    public bool isGettingHappyLess;
    private int NeededTypeOfToy;
    public Image ImageForToyTypeInUse;

    private void Start()
    {
        HowHappyIsBay = 100;
        WrongToyText.SetActive(false);
        NeededTypeOfToy = 0;
        ImageForToyTypeInUse.sprite = ExistingToyTypeImages[NeededTypeOfToy];
    }
    private void Update()
    {
        if (isGettingHappyLess)
        {
            if (HowHappyIsBay <= 0f)
            {
                uIManager.Deth();
                isGettingHappyLess = false;
            }
            else if (HowHappyIsBay > 100f)
            {
                HowHappyIsBay = 100f;
            }
            else
            {
                HowHappyIsBay -= HowFastWillBayHappynesGoDown * Time.deltaTime;
            }
            uIManager.SetHappyBar(HowHappyIsBay);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Toys")
        {
            if (collision.gameObject.GetComponent<Toy>().WhatToyTypeIsThis() == NeededTypeOfToy)
            {
                gameManager.Player.GetComponent<CursorController>().DropItem();
                AddOrTakeBayHappiness(collision.gameObject.GetComponent<Toy>().HowMuchDoesToyGiveHappiness());
                Destroy(collision.gameObject);
                changeToyTypeWanted();
            }
            else
            {
                StartCoroutine(wait());
            }
        }
    }
    public void AddOrTakeBayHappiness(float AddorSubtractNum)
    {
        if (isGettingHappyLess)
        {
            HowHappyIsBay += AddorSubtractNum;
        }
    }
    private void changeToyTypeWanted()
    {
        int myRandomIndex;
        myRandomIndex = Random.Range(0, ExistingToyTypeImages.Length);
        ImageForToyTypeInUse.sprite = ExistingToyTypeImages[myRandomIndex];
        NeededTypeOfToy = myRandomIndex;
    }
    IEnumerator wait()
    {
        WrongToyText.SetActive(true);
        yield return new WaitForSeconds(.7f);
        WrongToyText.SetActive(false);
    }
}
