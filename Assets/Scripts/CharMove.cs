using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharMove : MonoBehaviour
{
    //public float interactionDistance = 3f;
    //public KeyCode interactionKey = KeyCode.E;
    //public GerenciadorDeDialogo dialogueManager;
    public bool isDialoguing = false;
    public bool canMove = true;

    [SerializeField] private GameObject interactableObject;
    [SerializeField] private GameObject visualEffect;

    public string SlipperyTag = "Slippery";

    private Vector3 movementDirection;
    private bool isSlipperyActivated = false;

    public int actualLife;
    public int life = 100;
    public int quantityJump = 2;
    public float moveSpeed = 5f;
    public int coin = 0;
    private bool AbilityActive = false;
    public int SwordDamage = 10;
    public int ArrowDamage = 5;
    public int ArrowMax = 10;
    public float maxDistance = 10f;

    public Image lifebar;
    public Image redbar;

    [SerializeField] private int lifeToRecover = 20;
    [SerializeField] private GameObject Ability;
    [SerializeField] private GameObject LightAttackChar;
    [SerializeField] private GameObject HeavyAttackChar;
    [SerializeField] private GameObject ShieldChar;
    [SerializeField] private TutorialController TC;
    [SerializeField] private PhaseManager PM;
    [SerializeField] private GameObject CameraUp;
    [SerializeField] private GameObject MyCam;
    //public GameObject ShieldChar;

    private Animator Anim;
    private Rigidbody Rb;
    private BoxCollider Bc;
    private AudioSource AL;
  //  [SerializeField] private GameObject audioObject;
    public AudioClip coinCollectSound;

    public Transform cameraTransform;


    // Start is called before the first frame update
    void Start()
    {
      
        AL = GetComponent<AudioSource>();
        actualLife = life;
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        Bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

            Move();
            Jump();
            LightAttack();
            HeavyAttack();
            Defend(); 
            OpenAbility();
    }
   /* private void FixedUpdate()
    {
        if (!isSlipperyActivated)
        {
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular a direção do movimento
        //
        //movementDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // Aplicar força ao Rigidbody para mover o personagem
       // Rb.AddForce(movementDirection * 10f);
    }*/
    void Move()
    {
       
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        
        Vector3 CorrectedSpeed = moveH * transform.right + moveV * transform.forward;
        Rb.velocity = new Vector3(CorrectedSpeed.x, Rb.velocity.y, CorrectedSpeed.z);

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;

        Vector3 movement = moveH * cameraRight.normalized + moveV * cameraForward.normalized;
        movement.Normalize();

        movement = movement * moveSpeed;
        //Rb.velocity = movement * moveSpeed;
        Rb.velocity = new Vector3(movement.x, Rb.velocity.y, movement.z);

        //Animations
        if (Mathf.Abs(moveV) > 0)
        {
            Anim.SetBool("IsWalking", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Rb.velocity = (movement * moveSpeed);
                Anim.SetBool("IsRunning", true);
            }
            else 
            {
                Anim.SetBool("IsRunning", false);
            }
        }
        else 
        {
            Anim.SetBool("IsWalking", false);
            Anim.SetBool("IsRunning", false);
        }

    

        }
       
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rb.AddForce(Vector3.up * 100);
            quantityJump--;
            
           
        }
    }


    //LightAttack
    void LightAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Anim.SetTrigger("LightAttack");
            Attack();
        }
    }

    void ActiveLightAttack()
    {
        LightAttackChar.SetActive(true);
    }
    void DeactiveLightAttack()
    {
        LightAttackChar.SetActive(false);
    }

   

    //HeavyAttack
    void HeavyAttack()
    {   if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Anim.SetTrigger("HeavyAttack");
            Attack();
        }
        
    }

    void ActiveHeavyAttack()
    {
        HeavyAttackChar.SetActive(true);
    }



    void DeactiveHeavyAttack()
    {
        HeavyAttackChar.SetActive(false);
    }

    private void Attack()
    {
        Vector3 attackPosition = transform.position;

        Vector3 attackDirection = transform.forward;
        RaycastHit hit;

        if(Physics.Raycast(attackPosition, attackDirection, out hit, maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            Vector3 hitPoint = hit.point;
            Vector3 hitNormal = hit.normal;

          /*  Debug.Log("Objeto atingido: " + hitObject.name);
            Debug.Log("Ponto de colisão: " + hitPoint);
            Debug.Log("Normal da superfície: " + hitNormal);*/
        }
        else
        {
           /* Debug.Log("Nenhum objeto atingido");*/
        }
    }

   void Defend()
   {
        bool isDefending = Input.GetMouseButton(1);
        
        Anim.SetBool("Defending", isDefending);
    }

    void ActiveDefense()
    {
        ShieldChar.SetActive(true);
    }

    void DeactiveDefense()
    {
        ShieldChar.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            quantityJump = 2;
        }
        if (other.gameObject.tag == "Teste")
        {
            TC.ShowButton();
        }

        /*if (other.CompareTag("Heart"))
        {
            RecoverHealth();
            Destroy(other.gameObject);
        }*/

        if (other.CompareTag("Interactable"))
        {
            interactableObject = other.gameObject;
        }

        if (other.CompareTag("Beholder"))
        {
            
        }

        if (other.CompareTag("Coin"))
        {
            coin++;

            if (coinCollectSound != null)
            {
                AL.PlayOneShot(coinCollectSound);
            }
            Debug.Log("Coletou");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("NextPhase"))
        {
            Debug.Log("Passou");
            PM.PassPhase(); ;
        }
        if (other.CompareTag("CameraUp"))
        {
            CameraUp.SetActive(true);
            MyCam.SetActive(false);
        }

        if (other.CompareTag("NormalCamera"))
        {
            CameraUp.SetActive(false);
            MyCam.SetActive(true);
        }
        if (other.CompareTag("DieLine"))
        {
            life--;
          //  SetHealth(10);
            PM.Fase(PM.ActualPhase);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == interactableObject)
        {
            interactableObject = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isSlipperyActivated)
        {
            // Verificar se colidiu com um objeto Slippery
            if (collision.gameObject.CompareTag(SlipperyTag))
            {
                isSlipperyActivated = true;
            }
        }
        else
        {
            // Verificar se colidiu com uma pedra
            if (collision.gameObject.CompareTag("Stone"))
            {
                // Parar o movimento
                Rb.velocity = Vector3.zero;
            }
        }
    }

        private void OpenAbility()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && AbilityActive == false)
        {
            Ability.SetActive(true);
            AbilityActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && AbilityActive == true)
        {
            Ability.SetActive(false);
            AbilityActive = false;
        }
    }
    
    public void SetHealth(int amount)
    {
        actualLife = Mathf.Clamp(actualLife + amount, 0, life);

        Vector3 lifebarScale = lifebar.rectTransform.localScale;
        lifebarScale.x = (float)actualLife / life;
        lifebar.rectTransform.localScale = lifebarScale;
        StartCoroutine(DecreasingRedBar(lifebarScale));
    }

    IEnumerator DecreasingRedBar(Vector3 newScale)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 redBarScale = redbar.transform.localScale;
        Debug.Log(Time.time);

        while(redbar.transform.localScale.x > newScale.x)
        {
            redBarScale.x -= Time.deltaTime * 0.25f;
            redbar.transform.localScale = redBarScale;

            yield return null;
        }

        redbar.transform.localScale = newScale;
      
    }

    public int GetHealth()
    {
        return actualLife;
    }

    public int GetCoin()
    {
        return coin;
    }

    private void RecoverHealth()
    {
        SetHealth(lifeToRecover);
    }

    public int SetSwordDamage()
    {
        return SwordDamage;
    }

    public int SetArrowDamage()
    {
        return ArrowDamage;
    }
}
