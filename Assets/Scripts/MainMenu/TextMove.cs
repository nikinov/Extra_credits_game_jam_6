using UnityEngine;

namespace MainMenu
{
    public class TextMove : MonoBehaviour
    {
        public float textSpeed = 1;
        private float _speeding = -2000;

        private void Awake()
        {
            _speeding = gameObject.GetComponent<RectTransform>().position.x;
        }
        private void Update()
        {
            RectTransform thisText = gameObject.GetComponent<RectTransform>();
            var position = thisText.position;
            position = new Vector3((_speeding += textSpeed*Time.deltaTime) * -1, position.y, position.z);
            thisText.position = position;
        }
    }
}
