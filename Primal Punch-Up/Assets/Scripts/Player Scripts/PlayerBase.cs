using System.Collections;
using System.Collections.Generic;
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
    }

    IEnumerator PlayerBasicAttack()
    {
        canMove = false;
        speed = 0;
        anim.Play(attack1Anim);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length/2);
        canMove = true;
        speed = lizardSpeed;
    }
}
