using System.Collections;
using UnityEngine;

namespace Player.CharController
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float jumpMultiplayer;
        [SerializeField] private float health = 100;

        //public AudioSource AudioS;
        private bool _isJumping;
        private bool _isMoving;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            MovePlayer();
            //Footsteps();
        }

        private void MovePlayer()
        {
            float vertInput = Input.GetAxis("Vertical");
            float horizInput = Input.GetAxis("Horizontal");

            Vector3 vertMove = transform.forward * vertInput;
            Vector3 horizMove = transform.right * horizInput;

            _characterController.SimpleMove(Vector3.ClampMagnitude(vertMove + horizMove, 1f) * moveSpeed);


            JumpInput();
        }

        //void Footsteps()
        //{ if (characterController.velocity.magnitude > 0.5f && characterController.isGrounded == false)
        //	{ 
        //		//AudioS.Play();
        //	//AudioS.loop = true; }
        //	if (characterController.velocity.magnitude < 0.2f)
        //		AudioS.Stop();
        //}

        private void JumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                _isJumping = true;
                StartCoroutine(JumpEvent());
            }

        }

        private IEnumerator JumpEvent()
        {
            float timeInAir = 0f;
            _characterController.slopeLimit = 90f;

            do
            {
                float jumpForce = jumpCurve.Evaluate(timeInAir);
                _characterController.Move(Vector3.up * jumpForce * jumpMultiplayer * Time.deltaTime);
                timeInAir += Time.deltaTime;
                yield return null;
            } while (!_characterController.isGrounded && _characterController.collisionFlags != CollisionFlags.Above);

            _isJumping = false;
            _characterController.slopeLimit = 45f;
        }

        public void Damage()
        {
            health -= 25;
        }

        public float Health
        {
            get;
            set;
        }
    }
}
