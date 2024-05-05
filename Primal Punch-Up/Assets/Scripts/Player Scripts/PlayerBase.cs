using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    public int playerNo = 0;
    public GameObject teleportGatePrefab;
    private GameObject firstPortal = null;
    private GameObject secondPortal = null;
    private bool hasTeleportGate = false;
    public Gamepad P3Controller = null;
    public Gamepad P4Controller = null;

    public GameObject flamePrefab;
    public bool hasFlameItem = false;
    public float flameTrailDuration = 5.0f;
    public float flameSpawnInterval = 0.5f;
    public float flameLifetime = 1.0f;

    public Animator anim;
    public Rigidbody rbody;
    public BoxCollider boxCol;
    public CapsuleCollider capCol;

    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    public float currentSpeed = 10.0f;
    public float rotateSpeed = 720.0f;
    public int hp = 50;
    public int maxHP = 50;
    public float deathTimer = 5.0f;

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
    KeyCode? moveForwardKey = null;
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

    public bool isDashing = false;
    private float dashTimer = 0.0f;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public GameObject respawnScreen;
    public Slider respawnSlider;
    public Text respawnTimer;

    public GameObject Manager;
    private RoundsScript RoundsScript;
    public PlayerPickupManager PlayerPickupManager;
    public PauseScript PauseScript;

    private Vector3 spawnPos;
    private PickupItem PickupItem;

    public GameObject fruitPrefab;

    private AudioSource audioSource;
    public AudioClip[] audioClips; 
    public bool isDead = false;
    public bool isTakingDamage = false;
    public bool isAttacking = false;
    public bool isUsingSpecial = false;
    public bool bearFireMovement = false;
    public bool lizSmokeDmg = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        capCol = GetComponent<CapsuleCollider>();

        Manager = GameObject.FindGameObjectWithTag("Manager");
        RoundsScript = Manager.GetComponent<RoundsScript>();
        PlayerPickupManager = gameObject.GetComponent<PlayerPickupManager>();
        PauseScript = Manager.GetComponent<PauseScript>();

        string s = "Player" + playerNo + "Respawn";
        respawnScreen = GameObject.Find(s);
        s = "Player" + playerNo + "RespawnSlider";
        respawnSlider = GameObject.Find(s).GetComponent<Slider>();
        s = "Player" + playerNo + "RespawnTimer";
        respawnTimer = GameObject.Find(s).GetComponent<Text>();
        respawnScreen.SetActive(false);

        PickupItem = GetComponent<PickupItem>();

        canMove = true;

        spawnPos = transform.position;

        audioSource = GetComponent<AudioSource>();

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
        HandleInput();

        if (playerNo == 1 || playerNo == 2)
        {
            if (attack1Key.HasValue && Input.GetKey(attack1Key.Value) &&!isAttacking && !isDashing &&!isUsingSpecial &&!isDead)
            {
                StartCoroutine(PlayerBasicAttack());
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            if (itemKey.HasValue && Input.GetKey(itemKey.Value))
            {
                PlayerPickupManager.UseItem();
            }
            if (dashKey.HasValue && Input.GetKey(dashKey.Value) && !isAttacking && !isDashing && !isUsingSpecial && !isDead)
            {
                isDashing = true;
                dashTimer = dashDuration;
            }
            if (playerNo == 1 && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))){
                 audioSource.Stop();
            }
            if (playerNo == 1 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))){
                audioSource.clip = audioClips[2];
                 audioSource.Play();
            }
            if (playerNo == 2 && (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))){
                 audioSource.Stop();
            }
            if (playerNo == 2 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))){
                audioSource.clip = audioClips[2];
                 audioSource.Play();
            }
        }
        else if (playerNo == 3)
        {
            if (P3Controller.buttonEast.wasPressedThisFrame && !isAttacking && !isDashing && !isUsingSpecial && !isDead)
            {
                StartCoroutine(PlayerBasicAttack());
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            if (P3Controller.buttonWest.wasPressedThisFrame)
            {
                PlayerPickupManager.UseItem();
            }
            if (P3Controller.buttonSouth.wasPressedThisFrame && !isDashing && !isUsingSpecial && !isDead)
            {
                isDashing = true;
                dashTimer = dashDuration;
            }
             if (playerNo == 3 && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))){
                 audioSource.Stop();
            }
            if (playerNo == 3 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))){
                audioSource.clip = audioClips[2];
                 audioSource.Play();
            }
        }
        else if (playerNo == 4)
        {
            if (P4Controller.buttonEast.wasPressedThisFrame && !isAttacking && !isDashing && !isUsingSpecial && !isDead)
            {
                StartCoroutine(PlayerBasicAttack());
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
            if (P4Controller.buttonWest.wasPressedThisFrame)
            {
                PlayerPickupManager.UseItem();
            }
            if (P4Controller.buttonSouth.wasPressedThisFrame && !isDashing && !isUsingSpecial && !isDead)
            {
                isDashing = true;
                dashTimer = dashDuration;
            }
             if (playerNo == 4 && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))){
                 audioSource.Stop();
            }
            if (playerNo == 4 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))){
                audioSource.clip = audioClips[2];
                 audioSource.Play();
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
        }
        else
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

        if (hp <= 0 && !isDead)
        {
            StartCoroutine(OnDeath());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScript.PauseGame();
        }

    }

    void FixedUpdate()
    {
        Move();
    }

    void ChangeDirection()
    {
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
        }
        else if (playerNo == 3)
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
        }
        else if (playerNo == 4)
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
        if (!bearFireMovement)
        {
            if (!isDead && !isTakingDamage)
            {
                rbody.velocity = transform.forward * moveDirection.z * currentSpeed;
            }
            else
            {
                rbody.velocity = Vector3.zero;
            }
        }


        if (!isDashing && !isAttacking && !isDead && !isTakingDamage && !isUsingSpecial)
        {
            if (moveDirection != Vector3.zero)
            {
                anim.Play(runAnim);
                lastMoveDirection = moveDirection;
            }
            else
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
        isAttacking = true;
        anim.Play(attack1Anim);
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(attack1Anim))
        {
            yield return new WaitForSeconds(currentState.length);
            boxCol.enabled = false;
        }
        isAttacking = false;
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

        if (other.gameObject.CompareTag("PortalPickup"))
        {
            hasTeleportGate = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("FirePickup"))
        {
            this.hasFlameItem = true;
            Destroy(other.gameObject);
        }
    }

    public IEnumerator TakeDamage(int damage)
    {
        isTakingDamage = true;
        currentSpeed = 0;
        anim.Play(takeHit1Anim);
        hp -= damage;
        //Debug.Log(gameObject.name + " HP: " + hp);
        inCombat = true;
        inCombatTimer = inCombatLength;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 5);
        currentSpeed = speed;
        isTakingDamage = false;
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public void setSpeed(bool half)
    {
        this.speed = half ? 5.0f : 10.0f;
    }

    IEnumerator OnDeath()
    {
        isDead = true;
        anim.Play(deathAnim);
        respawnScreen.SetActive(true);
        int radius = 5;
        int fruitNumber = PickupItem.tempScore;
        Vector3 center = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
        for (int i = 0; i < fruitNumber; i++)
        {
            Vector3 pos = RandomCircle(center, 5.0f);
            Quaternion rot = Quaternion.Euler(270, 0, 0);
            GameObject fruit = Instantiate(fruitPrefab, pos, rot);
            //fruit.GetComponent<FruitScript>().StartPickupAfterDrop(playerNo);
        }
        PickupItem.tempScore = 0;
        capCol.enabled = false;

        for (float timer = deathTimer; timer >= 0; timer -= 1f)
        {
            respawnTimer.text = timer.ToString();
            respawnSlider.value = timer;
            yield return new WaitForSeconds(1f);
        }

        capCol.enabled = true;
        transform.position = spawnPos;
        hp = maxHP;
        isDead = false;
        respawnScreen.SetActive(false);
    }

    void HandleInput()
    {
        KeyCode actionKey = KeyCode.None;
        if (playerNo == 1) actionKey = KeyCode.T;
        else if (playerNo == 2) actionKey = KeyCode.L;
        else if (playerNo == 3 && P3Controller != null) actionKey = KeyCode.Joystick3Button2; 
        else if (playerNo == 4 && P4Controller != null) actionKey = KeyCode.Joystick4Button2; 

        if (Input.GetKeyDown(actionKey))
        {
            if (hasFlameItem)
            {
                StartCoroutine(CreateFlameTrail());
                hasFlameItem = false; 
            }
            else if (hasTeleportGate)
            {
                PlacePortal();
                hasTeleportGate = false; 
            }
        }
    }


    void PlacePortal()
    {
        if (firstPortal == null)
        {
            firstPortal = Instantiate(teleportGatePrefab, transform.position, Quaternion.identity);
            firstPortal.name = "FirstPortal";
        }
        else if (secondPortal == null)
        {
            secondPortal = Instantiate(teleportGatePrefab, transform.position, Quaternion.identity);
            secondPortal.name = "SecondPortal";
            firstPortal.GetComponent<Portal>().SetPartner(secondPortal);
            secondPortal.GetComponent<Portal>().SetPartner(firstPortal);
        }
    }

    IEnumerator CreateFlameTrail()
    {
        float endTime = Time.time + flameTrailDuration;
        while (Time.time <= endTime)
        {
            GameObject flame = Instantiate(flamePrefab, transform.position, Quaternion.identity);
            Destroy(flame, flameLifetime);
            yield return new WaitForSeconds(flameSpawnInterval);
        }
    }
}