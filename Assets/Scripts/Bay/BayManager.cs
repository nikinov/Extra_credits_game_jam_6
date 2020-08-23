﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayManager : MonoBehaviour
{
    private float HowHappyIsBay;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private float HowFastWillBayHappynesGoDown = .1f;
    public bool isGettingHappyLess;

    private void Start()
    {
        HowHappyIsBay = 100;
        isGettingHappyLess = false;
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
            AddOrTakeBayHappiness(collision.gameObject.GetComponent<Toy>().HowMuchDoesToyGiveHappiness());
            Destroy(collision.gameObject);
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