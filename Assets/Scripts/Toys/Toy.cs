using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] private float AmountOfHappiness = 20f;
    [SerializeField] private string ToyType = "Battery";
    public float HowMuchDoesToyGiveHappiness()
    {
        return AmountOfHappiness;
    }
    public string WhatToyTypeIsThis()
    {
        return ToyType;
    }
}
