using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    public bool isGrabbable { get; protected set; }

    public InteractiveObj()
    {
        isGrabbable = true;
    }

    public virtual void Interac()
    {

    }
}
