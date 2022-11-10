using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EG
{
    public class CharacterControl : MonoBehaviour
    {
        private Animator animator;
        private CharacterController controller;
        private new GameObject camera;
        public Rigidbody rb;

        [Header("Stats")]
        [SerializeField] private float speed = 3f;
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private float jumpForce = 0.1f;
        [SerializeField] private float spinTime = 0.1f;
        float speedSpin;
        [Header("Ground Data")]
        [SerializeField] private Transform detectGround;
        [SerializeField] private float distanceGround;
        [SerializeField] private LayerMask layerGround;
        private Transform elementstaff;
        public GameObject fireballPrefab;
        private GameObject currentFireball;
        public float horizontal = 0.0f;
        public float vertical = 0.0f;
        float gravity = -9.81f;
        Vector3 velocity = Vector3.zero;
        bool touchFloor;
        public bool canJump;
        public bool attack = false;
        public bool die = false;
        public bool dead = false;
        public bool superattack = false;
        public GameObject efecto_sangre;
        public GameObject efecto_impacto;
        public ParticleSystem flashEffect;
        public ParticleSystem efectoCasquillo;
        public float range = 100f;


        public AudioSource _audSource;

        public AudioClip clip_disparo;
        public AudioClip clip_hit;


        // Start is called before the first frame update
        void Start()
        {
            canJump = false;
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            camera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        private void FixedUpdate()
        {
            Movement();
            controller.Move(velocity);

        }
        // Update is called once per frame
        void Update()
        {

            DamageManage();
            Attack();

        }

        void Falling()
        {
            canJump = false;
            animator.SetBool("touchFloor", false);
            animator.SetBool("Jump", false);
        }
        void Disparar()
        {
            flashEffect.Play();
            efectoCasquillo.Play();

            PonerPlaySonido(clip_disparo);

            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    
                    PonerPlaySonido(clip_hit);
                    GameObject go = Instantiate(efecto_sangre, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(go, 3f);

                    GameObject goImpact = Instantiate(efecto_impacto, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(goImpact, 3f);
                }
                if (hit.transform.CompareTag("Enemy2"))
                {
                    //Enemigo.health -= 30;
                    PonerPlaySonido(clip_hit);
                    GameObject go = Instantiate(efecto_sangre, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(go, 3f);

                    GameObject goImpact = Instantiate(efecto_impacto, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(goImpact, 3f);
                }
                if (hit.transform.CompareTag("Wall"))
                {

                    PonerPlaySonido(clip_hit);
                    GameObject go = Instantiate(efecto_sangre, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(go, 3f);

                    GameObject goImpact = Instantiate(efecto_impacto, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(goImpact, 3f);
                }
            }
        }
        void Attack()
        {

            float fireSpeed = 200.0f;

            if (Input.GetButtonDown("Fire1"))
            {
                attack = true;
                Disparar();
                currentFireball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                currentFireball.GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed,
                                                                   ForceMode.Impulse);
                currentFireball.gameObject.transform.parent = null;
                currentFireball.GetComponent<AudioSource>().volume = GameMaster.sharedInstance.sfxVolume;
                currentFireball.GetComponent<AudioSource>().Play();
                animator.SetBool("Attack", true);
                Invoke("LoadNewFireball", 1.0f);

            }
            if (Input.GetButtonDown("Fire1"))
            {
                attack = false;
                animator.SetBool("Attack", false);
            }
          

            if (Input.GetMouseButton(1))
            {
                superattack = true;


            }

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
                float objetivoAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
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
            if (Input.GetButtonDown("Jump") )
            {
                //if (touchFloor && velocity.y < 0)
                //{
                //    velocity.y = -2f;
                //}
                canJump = true;
                    animator.SetBool("Jump", true);
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                    velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
               
                //animator.SetBool("touchFloor", true);
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
        private void LoadNewFireball()
        {
            currentFireball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentFireball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentFireball.transform.position = elementstaff.position;
            currentFireball.gameObject.transform.parent = elementstaff;
        }
        void PonerPlaySonido(AudioClip clip)
        {
            _audSource.clip = clip;
            _audSource.Play();
        }

    }
}