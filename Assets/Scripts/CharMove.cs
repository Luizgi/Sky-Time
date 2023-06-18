using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class CharMove : MonoBehaviour
{
    public int actualLife;
    public int life = 100;
    public int quantityJump = 2;
    public float moveSpeed = 5f;
    public int coin = 0;
    private bool AbilityActive = false;
    public int SwordDamage = 10;
    public int ArrowDamage = 5;
    public int ArrowMax = 10;

    public Image lifebar;
    public Image redbar;

    [SerializeField] private int lifeToRecover = 20;
    [SerializeField] private GameObject Ability;
    [SerializeField] private GameObject LightAttackChar;
    [SerializeField] private GameObject HeavyAttackChar;
    [SerializeField] private GameObject ShieldChar;
    [SerializeField] private TutorialController TC;
    //public GameObject ShieldChar;

    private Animator Anim;
    private Rigidbody Rb;
    private BoxCollider Bc;
    private AudioListener AL;

    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
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

        
        Rb.velocity = movement * moveSpeed;

        //Animations
        if (Mathf.Abs(moveV) > 0)
        {
            Anim.SetBool("IsWalking", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Rb.velocity = (movement * moveSpeed) * 2;
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
            Rb.AddForce(Vector3.up * 400);
            quantityJump--;
            
           
        }
    }


    //LightAttack
    void LightAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //LightAttackChar.SetActive(true);
            Anim.SetTrigger("LightAttack");
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
            //HeavyAttackChar.SetActive(true);
            Anim.SetTrigger("HeavyAttack");
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

   void Defend()
   {
        bool isDefending = Input.GetMouseButton(1);
        
        Anim.SetBool("Defending", isDefending);
    }


    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            quantityJump = 2;
        }
        if(other.gameObject.tag == "Teste")
        {
            TC.ShowButton();
        }

        if (other.CompareTag("Heart"))
        {
            RecoverHealth();
            Destroy(other.gameObject);
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


}
