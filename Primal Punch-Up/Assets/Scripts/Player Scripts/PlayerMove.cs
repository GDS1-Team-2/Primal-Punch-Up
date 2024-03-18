using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rbody;

    private Vector3 moveDirection;
    public float speed = 10.0f;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
    }

    void FixedUpdate()
    {
        Move();
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

        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);

        if (rbody.velocity.z != 0 || rbody.velocity.x != 0)
        {
            anim.Play("LizardRun");
        } else
        {
            anim.Play("LizardIdle");
        }
    }
}
