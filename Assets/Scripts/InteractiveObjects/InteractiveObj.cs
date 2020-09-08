using UnityEngine;

namespace InteractiveObjects
{
    public class InteractiveObj : MonoBehaviour
    {
        public bool IsGrabbable { get; protected set; }

        public InteractiveObj()
        {
            IsGrabbable = true;
        }

        public virtual void Interact()
        {

        }
    }
}
