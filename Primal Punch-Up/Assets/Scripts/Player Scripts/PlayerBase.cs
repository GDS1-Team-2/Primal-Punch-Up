using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    public int playerNo = 0;

    public Gamepad P3Controller = null;
    public Gamepad P4Controller = null;

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

    float inCombatTimer = 0.0f;
    public float inCombatLength = 10.0f;
    bool inCombat = false;
    float gainHP = 0.0f;

    string idleAnim = "";
    string runAnim = "";
    string attack1Anim = "";
    string takeHit1Anim = "";
    string deathAnim = "";
    string dashAnim = "";
    KeyCode ? moveForwardKey = null;
    KeyCode? moveBackKey = null;
    KeyCode? rotateLeftKey = null;
    KeyCode? rotateRightKey = null;
    KeyCode? attack1Key = null;
    public KeyCode? attack2Key = null;
    KeyCode? itemKey = null;
    KeyCode? dashKey = null;

    public float dashSpeed = 20.0f;
    public float dashCooldown = 0.5f;
    public float dashDuration = 0.5f;

    private bool isDashing = false;
    private float dashTimer = 0.0f;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public GameObject Manager;
    private RoundsScript RoundsScript;
    public PlayerPickupManager PlayerPickupManager;

    private Vector3 spawnPos;
    private PickupItem PickupItem;

    public GameObject fruitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();

        Manager = GameObject.FindGameObjectWithTag("Manager");
        RoundsScript = Manager.GetComponent<RoundsScript>();
        PlayerPickupManager = gameObject.GetComponent<PlayerPickupManager>();


        PickupItem = GetComponent<PickupItem>();

        canMove = true;

        spawnPos = transform.position;

        switch (playerNo)
        {
            case 1:
                moveForwardKey = KeyCode.W;
                moveBackKey = KeyCode.S;
                rotateLeftKey = KeyCode.A;
                rotateRightKey = KeyCode.D;
                attack1Key = KeyCode.C;
                attack2Key = KeyCode.V;
                dashKey = KeyCode.B;
                itemKey = KeyCode.N;
                healthBar = GameObject.Find("Player 1 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player1Model", gameObject.tag);
                break;
            case 2:
                moveForwardKey = KeyCode.UpArrow;
                moveBackKey = KeyCode.DownArrow;
                rotateLeftKey = KeyCode.LeftArrow;
                rotateRightKey = KeyCode.RightArrow;
                attack1Key = KeyCode.O;
                attack2Key = KeyCode.P;
                dashKey = KeyCode.LeftBracket;
                itemKey = KeyCode.RightBracket;
                healthBar = GameObject.Find("Player 2 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player2Model", gameObject.tag);
                break;
            case 3:
                healthBar = GameObject.Find("Player 3 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player3Model", gameObject.tag);
                break;
            case 4:
                healthBar = GameObject.Find("Player 4 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player4Model", gameObject.tag);
                break;
        }

        switch (gameObject.tag)
        {
            case "Lizard":
                idleAnim = "LizardIdle";
                runAnim = "LizardRun";
                attack1Anim = "LizardAttack1";
                takeHit1Anim = "LizardTakeHit1";
                deathAnim = "LizardDeath";
                dashAnim = "LizardDash";
                break;
            case "Bear":
                idleAnim = "BearIdle";
                runAnim = "BearRun";
                attack1Anim = "BearAttack1";
                takeHit1Anim = "BearTakeHit1";
                deathAnim = "BearDeath";
                dashAnim = "BearDash";
                break;
            case "Rabbit":
                idleAnim = "RabbitIdle";
                runAnim = "RabbitRun";
                attack1Anim = "RabbitAttack1";
                takeHit1Anim = "RabbitTakeHit1";
                deathAnim = "RabbitDeath";
                dashAnim = "RabbitDash";
                break;
            case "Fox":
                idleAnim = "FoxIdle";
                runAnim = "FoxRun";
                attack1Anim = "FoxAttack1";
                takeHit1Anim = "FoxTakeHit1";
                deathAnim = "FoxDeath";
                dashAnim = "FoxDash";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNo == 1 || playerNo == 2)
        {
            if (attack1Key.HasValue && Input.GetKey(attack1Key.Value))
            {
                StartCoroutine(PlayerBasicAttack());
            }
            if (itemKey.HasValue && Input.GetKey(itemKey.Value))
            {
                PlayerPickupManager.UseItem();
            }
            if (dashKey.HasValue && Input.GetKey(dashKey.Value))
            {
                isDashing = true;
                dashTimer = dashDuration;
            }
        } else if (playerNo == 3)
        {
            if (P3Controller.buttonEast.wasPressedThisFrame)
            {
                StartCoroutine(PlayerBasicAttack());
            }
            if (P3Controller.buttonWest.wasPressedThisFrame)
            {
                PlayerPickupManager.UseItem();
            }
            if (P3Controller.buttonSouth.wasPressedThisFrame)
            {
                isDashing = true;
                dashTimer = dashDuration;
            }
        } else if (playerNo == 4)
        {
            if (P4Controller.buttonEast.wasPressedThisFrame)
            {
                StartCoroutine(PlayerBasicAttack());
            }
            if (P4Controller.buttonWest.wasPressedThisFrame)
            {
                PlayerPickupManager.UseItem();
            }
            if (P4Controller.buttonSouth.wasPressedThisFrame)
            {
                isDashing = true;
                dashTimer = dashDuration;
            }
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


        //setting the players for the round score
        switch (playerNo)
        {
            case 1:
                RoundsScript.SetPlayer1(gameObject);
                break;
            case 2:
                RoundsScript.SetPlayer2(gameObject);
                break;
            case 3:
                RoundsScript.SetPlayer3(gameObject);
                break;
            case 4:
                RoundsScript.SetPlayer4(gameObject);
                break;
        }

        if (hp <= 0)
        {
            StartCoroutine(OnDeath());
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
        //float moveX = 0
        float moveZ = 0;

        if (playerNo == 1 || playerNo == 2)
        {
            if (rotateLeftKey.HasValue && Input.GetKey(rotateLeftKey.Value) && !isDashing)
            {
                transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            }
            else if (rotateRightKey.HasValue && Input.GetKey(rotateRightKey.Value) && !isDashing)
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }

            if (moveForwardKey.HasValue && Input.GetKey(moveForwardKey.Value))
            {
                moveZ = 1;
            }
            else if (moveBackKey.HasValue && Input.GetKey(moveBackKey.Value))
            {
                moveZ = -1;
            }
        } else if (playerNo == 3)
        {
            if (P3Controller.leftStick.left.isPressed && !isDashing)
            {
                transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            } 
            else if (P3Controller.leftStick.right.isPressed && !isDashing)
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }

            if (P3Controller.leftStick.up.isPressed)
            {
                moveZ = 1;
            }
            else if (P3Controller.leftStick.down.isPressed)
            {
                moveZ = -1;
            }
        } else if (playerNo == 4)
        {
            if (P4Controller.leftStick.left.isPressed && !isDashing)
            {
                transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            }
            else if (P4Controller.leftStick.right.isPressed && !isDashing)
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }

            if (P4Controller.leftStick.up.isPressed)
            {
                moveZ = 1;
            }
            else if (P4Controller.leftStick.down.isPressed)
            {
                moveZ = -1;
            }
        }

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection.Normalize();
    }

    void Move()
    {
        rbody.velocity = transform.forward * moveDirection.z * speed;

        if (moveDirection != Vector3.zero)
        {
            if (!isDashing)
            {
                anim.Play(runAnim);
                lastMoveDirection = moveDirection;
            }
        }
        else
        {
            if (!isDashing)
            {
                anim.Play(idleAnim);
            }
        }

        if (isDashing)
        {
            transform.position += transform.forward * dashSpeed * Time.deltaTime;
            anim.Play(dashAnim);
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
        if (canMove)
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
            }
            else
            {
                canMove = false;
                currentSpeed = 0;
                anim.Play(attack1Anim);
                //boxCol.enabled = true;
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 3.5f);
                boxCol.enabled = false;
                canMove = true;
                currentSpeed = speed;
            }
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
            //Debug.Log(otherPlayer.gameObject.name + " has been hit");
        }
    }

    public IEnumerator TakeDamage(int damage)
    {
        canMove = false;
        currentSpeed = 0;
        anim.Play(takeHit1Anim);
        hp -= damage;
        for (int i = 0; i < PickupItem.tempScore; i++)
        {
            Debug.Log("drop");
            GameObject fruit = Instantiate(fruitPrefab, gameObject.transform.position, Quaternion.identity);
            fruit.GetComponent<FruitScript>().StartPickupAfterDrop(playerNo);
        }
        //Debug.Log(gameObject.name + " HP: " + hp);
        inCombat = true;
        inCombatTimer = inCombatLength;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length/5);
        currentSpeed = speed;
        
        PickupItem.LoseTempScore();
        canMove = true;
    }

    public void setSpeed(bool half)
    {
        this.speed = half ? 5.0f : 10.0f;
    }

    IEnumerator OnDeath()
    {
        canMove = false;
        anim.Play(deathAnim);
        PickupItem.tempScore = 0;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 2);
        canMove = true;
        hp = maxHP;
        transform.position = spawnPos;
    }
}
