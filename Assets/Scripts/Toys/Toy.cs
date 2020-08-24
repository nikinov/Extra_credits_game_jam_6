using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] private float AmountOfHappiness = 20f;
    [SerializeField] private int ToyType = 0;
    public float HowMuchDoesToyGiveHappiness()
    {
        return AmountOfHappiness;
    }
    public int WhatToyTypeIsThis()
    {
        return ToyType;
    }
}
