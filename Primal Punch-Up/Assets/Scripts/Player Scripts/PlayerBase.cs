using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rbody;
    public BoxCollider boxCol;

    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    public float speed = 10.0f;
    public float rotateSpeed = 720.0f;
    public int hp = 50;

    public bool canMove;

    public float lizardSpeed = 10.0f;

    string idleAnim = "";
    string runAnim = "";
    string attack1Anim = "";
    string takeHit1Anim = "";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        canMove = true;

        switch (gameObject.tag)
        {
            case "Lizard":
                idleAnim = "LizardIdle";
                runAnim = "LizardRun";
                attack1Anim = "LizardAttack1";
                takeHit1Anim = "LizardTakeHit1";
                break;
            case "Bear":
                idleAnim = "BearIdle";
                runAnim = "BearRun";
                attack1Anim = "BearAttack1";
                takeHit1Anim = "BearTakeHit1";
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(PlayerBasicAttack());
        }

        ChangeDirection();
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

        if (rbody.velocity.magnitude > 0.1f)
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
        if (rbody.velocity.magnitude > 0.1f)
        {
            canMove = false;
            rbody.velocity = Vector3.zero;
            speed = 0;
            anim.Play(attack1Anim);
            //boxCol.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            boxCol.enabled = false;
            canMove = true;
            speed = lizardSpeed;
        } else
        {
            canMove = false;
            speed = 0;
            anim.Play(attack1Anim);
            //boxCol.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 4);
            boxCol.enabled = false;
            canMove = true;
            speed = lizardSpeed;
        }
    }

    void BAColliderOn()
    {
        boxCol.enabled = true;
    }

    void BAColliderOff()
    {
        boxCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();
        PlayerBase thisPlayer = GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage());
            Debug.Log(otherPlayer.gameObject.name + " has been hit");
        }
    }

    IEnumerator TakeDamage()
    {
        canMove = false;
        speed = 0;
        anim.Play(takeHit1Anim);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        speed = lizardSpeed;
        canMove = true;
    }

}
