using UnityEngine;

namespace InteractiveObjects
{
    public class InteractiveBox : InteractiveObj
    {
        public InteractiveBox()
        {
            IsGrabbable = true;
        }

        public override void Interact()
        {
            Debug.Log("Take me home! Country Roads!");
        }
    }
}
