using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clipText : MonoBehaviour
{
    [SerializeField] private Transform follow;
    // Update is called once per frame
    void Update()
    {
        Vector3 dirFromAtoB = (follow.position - Camera.main.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, Camera.main.transform.forward);

        if (dotProd > 0.9)
        {
            Vector3 namePos = Camera.main.WorldToScreenPoint(follow.position);
            this.transform.position = namePos;
            if (gameObject.GetComponent<CanvasGroup>().alpha == 0)
            {
                gameObject.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
