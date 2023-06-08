using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public int life = 100;
    public int quantityJump = 2;

    public GameObject LightAttackChar;
    public GameObject HeavyAttackChar;
    public GameObject ShieldChar;

    public Animator Anim;
    public Rigidbody Rb;
    public BoxCollider Bc;
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
    }

    void Move()
    {
        float vAxis = Input.GetAxis("Vertical") * 3;
        float hAxis = 0;

        //Correcting Movement
        Vector3 CorrectedSpeed = hAxis * transform.right + vAxis * transform.forward;
        Rb.velocity = new Vector3(CorrectedSpeed.x, Rb.velocity.y, CorrectedSpeed.z);    


        //Animations
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            Rb.velocity = new Vector3(CorrectedSpeed.x * 3, Rb.velocity.y, CorrectedSpeed.z * 3);
            Anim.SetBool("IsRunning", true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
           Anim.SetBool("IsWalking", true);
        }

        //Calling Other Mechanics  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LightAttack();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            HeavyAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Defend();
        }
    }
    void Jump()
    {
        Rb.AddForce(Vector3.up);
        quantityJump--;
        Anim.SetTrigger("IsJumping");
    }

    void LightAttack()
    {
        LightAttackChar.SetActive(true);
        Anim.SetTrigger("LightAttack");
    }
    void HeavyAttack()
    {
        HeavyAttackChar.SetActive(true);
        Anim.SetTrigger("HeavyAttack");
    }

    void Defend()
    {
      ShieldChar.SetActive(true);
      Anim.SetTrigger("Defend");
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            quantityJump = 2;
        }
    }
}
