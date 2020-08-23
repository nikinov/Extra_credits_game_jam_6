using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    public float textSpeed = 1;
    private float speeding = -2000;

    private void Awake()
    {
        speeding = gameObject.GetComponent<RectTransform>().position.x;
    }
    private void Update()
    {
        RectTransform ThisText = gameObject.GetComponent<RectTransform>();
        ThisText.position = new Vector3((speeding += textSpeed*Time.deltaTime) * -1, ThisText.position.y, ThisText.position.z);
    }
}
