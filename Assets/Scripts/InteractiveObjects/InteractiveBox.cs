using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBox : InteractiveObj
{
    public InteractiveBox()
    {
        isGrabbable = true;
    }

    public override void Interac()
    {
        Debug.Log("Take me home! Country Roads!");
    }
}
