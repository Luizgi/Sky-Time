using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public int life = 100;
    public int quantityJump = 2;
    public float moveSpeed = 5f;


    public GameObject LightAttackChar;
    public GameObject HeavyAttackChar;
    public DialogueSystem DS;
    //public GameObject ShieldChar;

    public Animator Anim;
    public Rigidbody Rb;
    public BoxCollider Bc;

    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        Bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
       // Jump();
       // LightAttack();
       // HeavyAttack();
        //Defend();
       // Roll();
        //Idle();
    }

    void Move()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        
        Vector3 CorrectedSpeed = moveH * transform.right + moveV * transform.forward;
        Rb.velocity = new Vector3(CorrectedSpeed.x, Rb.velocity.y, CorrectedSpeed.z);

        Vector3 movement = new Vector3(moveH, 0f, moveV);
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
            Anim.SetTrigger("IsJumping");
            Debug.Log("Pulou");
        }
    }


    //LightAttack
    void LightAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Anim.SetTrigger("LightAttack");
        }
    }

    void ActiveLightAttack()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            LightAttackChar.SetActive(true);
        }
    }
    
    void DeactiveLightAttack()
    {
        LightAttackChar.SetActive(false);
    }

    //HeavyAttack
    void HeavyAttack()
    {
        HeavyAttackChar.SetActive(true);
        Anim.SetTrigger("HeavyAttack");
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //  ShieldChar.SetActive(true);
            Anim.SetBool("Defending", true);
        }
   }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            Rb.AddForce(Vector3.forward * 330);
            Anim.SetTrigger("Roll");
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            quantityJump = 2;
        }
        if(other.gameObject.tag == "Teste")
        {
            DS.ShowButton();
        }
    }
}
