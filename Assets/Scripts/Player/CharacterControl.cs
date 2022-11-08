using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EG
{
    public class CharacterControl : MonoBehaviour
    {
        private Animator animator;
        private CharacterController controller;
        private GameObject camara;
        public Rigidbody rb;

        [Header("Stats")]
        [SerializeField] private float speed = 3f;
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float spinTime = 0.1f;
        float speedSpin;
        [Header("Ground Data")]
        [SerializeField] private Transform detectGround;
        [SerializeField] private float distanceGround;
        [SerializeField] private LayerMask layerGround;

        public float horizontal = 0.0f;
        public float vertical = 0.0f;
        float gravity = -9.81f;
        Vector3 velocity;
        bool touchFloor;
        public bool canJump;
        public bool attack = false;
        public bool die = false;
        public bool dead = false;
        public bool superattack = false;

        // Start is called before the first frame update
        void Start()
        {
            canJump = false;
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            camara = GameObject.FindGameObjectWithTag("MainCamera");
        }
        private void FixedUpdate()
        {
            Movement();

        }
        // Update is called once per frame
        void Update()
        {

            DamageManage();


        }

        void Falling()
        {
            canJump = false;
            animator.SetBool("touchFloor", false);
            animator.SetBool("Jump", false);
        }
        void Movement()
        {


            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direccion = new Vector3(horizontal, 0, vertical).normalized;
            if (direccion.magnitude >= 0.1f)
            {
                float objetivoAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.transform.eulerAngles.y;
                float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, objetivoAngulo, ref speedSpin, spinTime);
                transform.rotation = Quaternion.Euler(0, angulo, 0);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetBool("Run", true);
                    Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                    controller.Move(mover.normalized * runSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                    controller.Move(mover.normalized * speed * Time.deltaTime);
                    animator.SetBool("Run", false);
                }
            }
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Horizontal", horizontal);
            if (canJump)
            {
                if (touchFloor && velocity.y < 0)
                {
                    velocity.y = -2f;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetBool("Jump", true);
                    //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                    velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
                }
                animator.SetBool("touchFloor", true);
            }
            else
            {
                Falling();
            }

        }
        void DamageManage() 
        
        {
            if (dead)
            {
                if (die)
                {
                    animator.SetBool("Die", true);
                    die = !die;
                }
                return;
            }

            //AQUÍ EL PERSONAJE SEGURO QUE ESTÁ VIVO

            if (Input.GetKeyDown(KeyCode.C) && !attack)
            {
                attack = true;
                animator.SetBool("Attack", attack);
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                attack = false;
                animator.SetBool("Attack", attack);
            }



        }       


    }
}