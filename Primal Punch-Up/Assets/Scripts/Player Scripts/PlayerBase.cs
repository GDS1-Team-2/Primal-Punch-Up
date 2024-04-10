using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rbody;
    public BoxCollider boxCol;

    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    public float currentSpeed = 10.0f;
    public float rotateSpeed = 720.0f;
    public int hp = 50;
    public int maxHP = 50;

    public bool canMove;

    public float speed = 10.0f;

    public float jumpSpeed;
    private float ySpeed;

    float inCombatTimer = 0.0f;
    public float inCombatLength = 5.0f;
    bool inCombat = false;
    float gainHP = 0.0f;

    string idleAnim = "";
    string runAnim = "";
    string attack1Anim = "";
    string takeHit1Anim = "";
    KeyCode? moveForwardKey = null;
    KeyCode? moveBackKey = null;
    KeyCode? rotateLeftKey = null;
    KeyCode? rotateRightKey = null;
    KeyCode? attack1Key = null;

    public float dashSpeed = 20.0f;
    public float dashCooldown = 1.0f;
    public float dashDuration = 1.0f;

    private bool isDashing = false;
    private float dashTimer = 0.0f;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public void setSpeed(bool half){
        this.speed = half ? 5.0f : 10.0f;
    }

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
                moveForwardKey = KeyCode.W;
                moveBackKey = KeyCode.S;
                rotateLeftKey = KeyCode.A;
                rotateRightKey = KeyCode.D;
                attack1Key = KeyCode.C;
                healthBar = GameObject.Find("Player 1 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                break;
            case "Bear":
                idleAnim = "BearIdle";
                runAnim = "BearRun";
                attack1Anim = "BearAttack1";
                takeHit1Anim = "BearTakeHit1";
                moveForwardKey = KeyCode.UpArrow;
                moveBackKey = KeyCode.DownArrow;
                rotateLeftKey = KeyCode.LeftArrow;
                rotateRightKey = KeyCode.RightArrow;
                attack1Key = KeyCode.P;
                healthBar = GameObject.Find("Player 2 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (attack1Key.HasValue && Input.GetKey(attack1Key.Value))
        {
            StartCoroutine(PlayerBasicAttack());
        }*/

        if (attack1Key.HasValue && Input.GetKey(attack1Key.Value) && canMove)
        {
            StartCoroutine(PlayerBasicAttack());
        }

        ChangeDirection();

        if (dashTimer > 0 && !isDashing)
        {
            dashTimer -= Time.deltaTime;
        }

        //Regen health when out of combat
        if (!inCombat)
        {
            gainHP += Time.deltaTime;
            if (gainHP >= 1.0f)
            {
                hp += 2;
                if (hp >= maxHP)
                {
                    hp = maxHP;
                }
                gainHP = 0.0f;
                //Debug.Log(this.hp);
            }
        } else
        {
            inCombatTimer -= 1 * Time.deltaTime;
            if (inCombatTimer < 0.0f)
            {
                inCombatTimer = 0.0f;
                inCombat = false;
            }
        }

        healthBarSlider.value = hp;

        ySpeed += Physics.gravity.y * Time.deltaTime;

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
        //float moveX = 0
        float moveZ = 0;

        if (rotateLeftKey.HasValue && Input.GetKey(rotateLeftKey.Value))
        {
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
        }
        else if (rotateRightKey.HasValue && Input.GetKey(rotateRightKey.Value))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

        if (moveForwardKey.HasValue && Input.GetKey(moveForwardKey.Value))
        {
            moveZ = 1 * this.speed;
        }
        else if (moveBackKey.HasValue && Input.GetKey(moveBackKey.Value))
        {
            moveZ = -1 * this.speed;
        }

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection.Normalize();
    }

    void Move()
    {
        rbody.velocity = transform.forward * moveDirection.z * speed;

        if (moveDirection != Vector3.zero)
        {
            anim.Play(runAnim);
            lastMoveDirection = moveDirection;
        }
        else
        {
            anim.Play(idleAnim);
        }

        //dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }
        if (isDashing)
        {
            transform.position += transform.forward * dashSpeed * Time.deltaTime;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0.0f)
            {
                isDashing = false;
                dashTimer = dashCooldown;
            }
        }
    }

    IEnumerator PlayerBasicAttack()
    {
        if (rbody.velocity.magnitude > 0.1f)
        {
            canMove = false;
            rbody.velocity = Vector3.zero;
            currentSpeed = 0;
            anim.Play(attack1Anim);
            //boxCol.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            boxCol.enabled = false;
            canMove = true;
            currentSpeed = speed;
        } else
        {
            canMove = false;
            currentSpeed = 0;
            anim.Play(attack1Anim);
            //boxCol.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length/4);
            boxCol.enabled = false;
            canMove = true;
            currentSpeed = speed;
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
        int BADamage = 10;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();
        PlayerBase thisPlayer = GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != thisPlayer && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage(BADamage));
            Debug.Log(otherPlayer.gameObject.name + " has been hit");
        }
    }

    IEnumerator TakeDamage(int damage)
    {
        canMove = false;
        currentSpeed = 0;
        anim.Play(takeHit1Anim);
        hp -= damage;
        //Debug.Log(gameObject.name + " HP: " + hp);
        inCombat = true;
        inCombatTimer = inCombatLength;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length/5);
        currentSpeed = speed;
        canMove = true;
    }

}
