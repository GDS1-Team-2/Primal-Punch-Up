using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rbody;

    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    public float speed = 10.0f;
    public float rotateSpeed = 720.0f;

    public bool canMove;

    public float lizardSpeed = 10.0f;

    string idleAnim = "";
    string runAnim = "";
    string attack1Anim = "";
    public float dashSpeed = 20.0f; // 冲刺速度
    public float dashCooldown = 1.0f; // 冲刺冷却时间
    public float dashDuration = 1.0f; // 冲刺持续时间

    private bool isDashing = false; // 是否正在冲刺
    private float dashTimer = 0.0f; // 冲刺计时器

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        canMove = true;

        switch (gameObject.tag)
        {
            case "Lizard":
                idleAnim = "LizardIdle";
                runAnim = "LizardRun";
                attack1Anim = "LizardAttack1";
                break;
            case "Bear":
                idleAnim = "BearIdle";
                runAnim = "BearRun";
                attack1Anim = "BearAttack1";
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(PlayerBasicAttack());
        }

        if(dashTimer > 0 && !isDashing){
            dashTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
    }

    void ChangeDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection.Normalize();
    }

    void Move()
    {
        rbody.velocity = new Vector3(moveDirection.x * speed, 0.0f, moveDirection.z * speed);

        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }

        if (rbody.velocity.z != 0 || rbody.velocity.x != 0)
        {
            anim.Play(runAnim);
            lastMoveDirection = moveDirection;
        } else
        {
            anim.Play(idleAnim);
        }

    // dash
    if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
    
            isDashing = true;
            dashTimer = dashDuration;
        }

    if (isDashing)
        {
            //dash
            transform.position += transform.forward * dashSpeed * Time.deltaTime;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0.0f)
            {
                // end dash
                isDashing = false;
                dashTimer = dashCooldown;
            }
        }
    
    
    }


    IEnumerator PlayerBasicAttack()
    {
        canMove = false;
        speed = 0;
        anim.Play(attack1Anim);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length/3);
        canMove = true;
        speed = lizardSpeed;
    }
}
