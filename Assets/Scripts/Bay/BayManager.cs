using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayManager : MonoBehaviour
{
    private float HowHappyIsBay;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private float HowFastWillBayHappynesGoDown = .1f;
    public bool isGettingHappyLess;
    public string NeededTypeOfToy = "Battery";

    private void Start()
    {
        HowHappyIsBay = 100;
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
            }
            else
            {
                Debug.Log("you gave me the wrong toy, beeeee");
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
}
