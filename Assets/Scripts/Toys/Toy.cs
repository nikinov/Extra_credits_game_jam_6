using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField] private float AmountOfHappiness = 20f;
    public float HowMuchDoesToyGiveHappiness()
    {
        return AmountOfHappiness;
    }
}
