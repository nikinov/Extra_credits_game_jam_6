using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(ConfigurableJoint))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float gravity = -9.8f;

        private Vector3 velocity = Vector3.zero;
        private Vector3 rotation = Vector3.zero;
        private float cameraRotationX = 0f;
        private float currentCameraRotationX = 0f;
        private Vector3 thrusterForse = Vector3.zero;
        private float gravity_velocity = 0;
        private float epsilon = .0001f;
        [SerializeField]private float max_gravity = .5f;

        [SerializeField] private float cameraRotationLimit = 85;
        // [SerializeField] private ConfigurableJoint joint;

        public bool grounded;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            // joint = GetComponent<ConfigurableJoint>();
        }

        // get movement vector
        public void Move (Vector3 _velocity)
        {
            velocity = _velocity;
        }

        // get rotation vector
        public void Rotate (Vector3 _rotation)
        {
            rotation = _rotation;
        }

        // get rotation vector
        public void RotateCamera(float _cameraRotationX)
        {
            cameraRotationX = _cameraRotationX;
        }

        public void ApplyThruster(Vector3 _thruseterForce)
        {
            thrusterForse = _thruseterForce;
        }

        // run every physics iteration
        private void FixedUpdate()
        {
            if (!grounded)
            {
                gravity_velocity += gravity * Time.fixedDeltaTime;
                velocity += Vector3.up * Mathf.Clamp(gravity_velocity, -max_gravity, 0);
            }
            PerformMovement();
            PerformRotation();
        }

        // performe movement based on velocity variable
        void PerformMovement()
        {
            Ray ray = new Ray(transform.position, -transform.up);

            RaycastHit hit;

            //ground check raycast
            if (Physics.Raycast(ray, out hit) == true)
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);

                if (hit.distance < 1f)
                {
                    velocity.y = 0;
                    grounded = true;
                    gravity_velocity = 0;
                }
                else
                {
                    grounded = false;
                }
                // if (joint.connectedAnchor.y != hit.point.y + 1.5f)
                // {
                //     joint.connectedAnchor = new Vector3(0, hit.point.y + 1f, 0);
                // }
            }

            if (velocity != Vector3.zero)
            {
                //declare a new Ray. It will start at this object's position and it's direction will be straight down from the object (in local space, that is)
                Debug.DrawRay(transform.position, velocity, Color.magenta);
                // Debug.Log(velocity);
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            }

            if (thrusterForse != Vector3.zero)
            {
                rb.AddForce(thrusterForse * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }

        // private void OnCollision(Collision coll)
        // {

        // }

        // performe rotation
        void PerformRotation ()
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            if (cam != null)
            {
                // set our rotation and clamp it
                currentCameraRotationX -= cameraRotationX;
                currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

                // apply our rotation to the transform of our camera
                cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
            }
        }
    }
}
