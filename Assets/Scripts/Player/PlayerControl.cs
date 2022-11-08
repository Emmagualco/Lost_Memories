using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EG
{
    public class PlayerControl : MonoBehaviour
    {
        private CharacterController controller;
        private GameObject camara;
        private Animator animator;
       

        [Header("Stats")]
        [SerializeField] private float speed;
        float totalSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float spinTime;

        [Header("Ground Data")]
        [SerializeField] private Transform detectGround;
        [SerializeField] private float distanceGround;
        [SerializeField] private LayerMask layerGround;

        float speedSpin;
        float gravity = -9.81f;
        Vector3 velocity;
        bool touchFloor;
        public float horizontal = 0.0f;
        public float vertical = 0.0f;
        public bool attack = false;
        public bool jump = false;
        public bool die = false;
        public bool superattack = false;

        public bool run = false;
        public bool dead = false;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            camara = GameObject.FindGameObjectWithTag("MainCamera");
        }

        private void Update()
        {
            
                        
            touchFloor = Physics.CheckSphere(detectGround.position, distanceGround, layerGround);

            if (touchFloor && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && touchFloor)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                animator.SetBool("jump", jump);
            }
            

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);
            Vector3 direccion = new Vector3(horizontal, 0, vertical).normalized;
            if (direccion.magnitude >= 0.1f)
            {
                float objetivoAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.transform.eulerAngles.y;
                float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, objetivoAngulo, ref speedSpin, spinTime);
                transform.rotation = Quaternion.Euler(0, angulo, 0);

                if (Input.GetKey(KeyCode.LeftShift) || speed >= 0.5)
                {
                    Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                    controller.Move(mover.normalized * runSpeed * Time.deltaTime);
                    animator.SetBool("run", true);
                }
                else if (speed < 0.5)
                {
                    Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                    controller.Move(mover.normalized * speed * Time.deltaTime);
                    animator.SetBool("run", false);
                }
                if (run == true)
                {
                    totalSpeed = speed * 1.1f;
                }
                else
                {
                    totalSpeed = speed;
                }
            }
            if (dead)
            {
                if (die)
                {
                    animator.SetBool("die", true);
                    die = !die;
                }
                return;
            }

            //AQUÍ EL PERSONAJE SEGURO QUE ESTÁ VIVO

            if (Input.GetKeyDown(KeyCode.Mouse0) && !attack)
            {
                attack = true;
                animator.SetBool("attack", attack);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                attack = false;
                animator.SetBool("attack", attack);
            }




            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                superattack = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                superattack = false;
            }
            animator.SetBool("superattack", superattack);


            //if (Input.GetKeyDown(KeyCode.LeftShift) || speed >= 0.5)
            //{
            //    run = true;
            //}
            //if (Input.GetKeyUp(KeyCode.LeftShift) || speed < 0.5)
            //{
            //    run = false;
            //}
            //animator.SetBool(ANIM_RUN, run);


           
            



        }
        //private void FixedUpdate()
        //{
        //    horizontal = Input.GetAxis("Horizontal");
        //    vertical = Input.GetAxis("Vertical");

        //    speed = new Vector2(horizontal, vertical).sqrMagnitude * GetComponent<PlayerAgent>().playerCharacterData.speed;

        //    animator.SetBool(ANIM_RUN, speed >= 0.5);
        //    animator.SetFloat(ANIM_SPEED, speed);
        //    animator.SetFloat(ANIM_HORI, horizontal);
        //    animator.SetFloat(ANIM_VERT, vertical);
        //}
    }
}
