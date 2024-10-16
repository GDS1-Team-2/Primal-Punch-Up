using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    public int playerNo = 0;

    public Gamepad thisController = null;
    public Gamepad P1Controller = null;
    public Gamepad P2Controller = null;
    public Gamepad P3Controller = null;
    public Gamepad P4Controller = null;

    public Animator anim;
    public Rigidbody rbody;
    public BoxCollider boxCol;
    public CapsuleCollider capCol;
    public SkinnedMeshRenderer SMR;
    private Color originalColor;

    private Vector3 moveDirection;
    private Vector3 lastMoveDirection;
    public float currentSpeed = 10.0f;
    private float rotateSpeed = 500.0f;
    public float hp = 50;
    public float maxHP = 50;
    public float deathTimer = 5.0f;

    public bool canMove = true;
    public bool acceptInput = true;

    public float speed = 15.0f;
    public float baseSpeed = 0.0f;

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
    string arrowAnim = "";

    public float dashSpeed = 20.0f;
    private float dashCooldown = 2f;
    public float dashDuration = 0.5f;

    public bool isDashing = false;
    private float dashTimer = 0.0f;
    private float dashCdTimer = 0.0f;

    public GameObject healthBar;
    public Slider healthBarSlider;

    public GameObject respawnScreen;
    public Slider respawnSlider;
    public Text respawnTimer;

    public SpriteRenderer minimapIcon;
    public Material Player1Material;
    public Material Player2Material;
    public Material Player3Material;
    public Material Player4Material;

    public GameObject Manager;
    private RoundsScript RoundsScript;
    public PlayerPickupManager PlayerPickupManager;
    public PauseScript PauseScript;

    private Vector3 spawnPos;
    private PickupItem PickupItem;
    public int dropNumber = 3;
    public PickupUIScript PickupUIScript;

    public GameObject fruitPrefab;
    public bool isDead = false;
    public bool isTakingDamage = false;
    public bool isAttacking = false;
    public bool isUsingSpecial = false;
    public bool isShielding = false;
    private bool isStrafing = false;
    private bool isShooting = false;
    public bool canDash = true;
    public bool bearFireMovement = false;
    public bool lizSmokeDmg = false;

    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public AudioClip[] footsteps;
    private int footstepIndex = 0;
    private bool isPlayingFootstep = false;
    public float footstepInterval = 0.5f;

    //public GameObject magnetRangeIndicator;
    public float shieldHealth = 3f;
    public float shieldCD = 10f;
    public GameObject forceField;
    public GameObject forceFieldOuter;
    public MeshRenderer ffr;
    public Material ForceFieldMat;
    public Material ForceFieldMat1;
    public Material ForceFieldMat2;
    public Material ForceFieldMat3;
    public Slider forcefieldSlider;
    public Animator forcefieldSliderAnimator;

    public Camera playerCamera;
    private float cameraSensitivity = 1.5f;
    private float cameraYaw = 0.0f;
    private float cameraPitch = 0.0f;
    private float cameraDistance = 10.0f;
    private Vector3 cameraOffset;

    public bool usingIce = false;
    public AudioSource attackSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        dropNumber = 3;
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        capCol = GetComponent<CapsuleCollider>();
        originalColor = SMR.material.color;

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
        PickupUIScript = gameObject.GetComponent<PickupUIScript>();
        PickupUIScript.SetPlayerNo(playerNo);

        canMove = true;
        acceptInput = true;

        spawnPos = transform.position;

        audioSource = GetComponent<AudioSource>();

        ffr = forceField.GetComponent<MeshRenderer>();
        Material[] materials = ffr.materials;

        Vector3 forward = transform.forward;
        cameraYaw = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;

        switch (playerNo)
        {
            case 1:
                thisController = P1Controller;
                healthBar = GameObject.Find("Player 1 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player1Model", gameObject.tag);
                minimapIcon.material = Player1Material;
                RoundsScript.SetPlayer1(gameObject);
                PauseScript.AddPlayer(gameObject);
                //magnetRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(0.9716981f, 0.5469621f, 0.5469621f, 0.3764706f);
                //magnetRangeIndicator.SetActive(false);
                UpdateForceFieldMaterial(ForceFieldMat);
                forcefieldSlider = GameObject.Find("Player 1 Forcefield Bar").GetComponent<Slider>();
                forcefieldSliderAnimator = GameObject.Find("Actual Fill 1").GetComponent<Animator>();
                break;
            case 2:
                thisController = P2Controller;
                healthBar = GameObject.Find("Player 2 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player2Model", gameObject.tag);
                minimapIcon.material = Player2Material;
                RoundsScript.SetPlayer2(gameObject);
                PauseScript.AddPlayer(gameObject);
                //magnetRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(0.9716981f, 0.5469621f, 0.5469621f, 0.3764706f);
                //magnetRangeIndicator.SetActive(false);
                UpdateForceFieldMaterial(ForceFieldMat1);
                forcefieldSlider = GameObject.Find("Player 2 Forcefield Bar").GetComponent<Slider>();
                forcefieldSliderAnimator = GameObject.Find("Actual Fill 2").GetComponent<Animator>();
                break;
            case 3:
                thisController = P3Controller;
                healthBar = GameObject.Find("Player 3 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player3Model", gameObject.tag);
                minimapIcon.material = Player3Material;
                RoundsScript.SetPlayer3(gameObject);
                PauseScript.AddPlayer(gameObject);
                //magnetRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(0.6383248f, 1, 0.5518868f, 0.3764706f);
                //magnetRangeIndicator.SetActive(false);
                UpdateForceFieldMaterial(ForceFieldMat2);
                forcefieldSlider = GameObject.Find("Player 3 Forcefield Bar").GetComponent<Slider>();
                forcefieldSliderAnimator = GameObject.Find("Actual Fill 3").GetComponent<Animator>();
                break;
            case 4:
                thisController = P4Controller;
                healthBar = GameObject.Find("Player 4 Health");
                healthBarSlider = healthBar.GetComponent<Slider>();
                PlayerPrefs.SetString("Player4Model", gameObject.tag);
                minimapIcon.material = Player4Material;
                RoundsScript.SetPlayer4(gameObject);
                PauseScript.AddPlayer(gameObject);
                //magnetRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(1, 0.945283f, 0.5896226f, 0.3764706f);
                //magnetRangeIndicator.SetActive(false);
                UpdateForceFieldMaterial(ForceFieldMat3);
                forcefieldSlider = GameObject.Find("Player 4 Forcefield Bar").GetComponent<Slider>();
                forcefieldSliderAnimator = GameObject.Find("Actual Fill 4").GetComponent<Animator>();
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
                arrowAnim = "LizardShootArrow";
                break;
            case "Bear":
                idleAnim = "BearIdle";
                runAnim = "BearRun";
                attack1Anim = "BearAttack1";
                takeHit1Anim = "BearTakeHit1";
                deathAnim = "BearDeath";
                dashAnim = "BearDash";
                arrowAnim = "BearShootArrow";
                break;
            case "Rabbit":
                idleAnim = "RabbitIdle";
                runAnim = "RabbitRun";
                attack1Anim = "RabbitAttack1";
                takeHit1Anim = "RabbitTakeHit1";
                deathAnim = "RabbitDeath";
                dashAnim = "RabbitDash";
                arrowAnim = "RabbitShootArrow";
                break;
            case "Fox":
                idleAnim = "FoxIdle";
                runAnim = "FoxRun";
                attack1Anim = "FoxAttack1";
                takeHit1Anim = "FoxTakeHit1";
                deathAnim = "FoxDeath";
                dashAnim = "FoxDash";
                arrowAnim = "FoxShootArrow";
                break;
            default:
                break;
        }
        cameraOffset = new Vector3(0, 2, -cameraDistance);
        baseSpeed = speed;
    }

    void UpdateForceFieldMaterial(Material newMaterial)
    {
        Material[] materials = ffr.materials;
        materials[0] = newMaterial;
        ffr.materials = materials;
    }

    // Update is called once per frame
    void Update()
    {
        //HandleInput();

        if (acceptInput)
        {
            if (thisController.buttonEast.wasPressedThisFrame && !isAttacking && !isShooting && !isDashing && !isUsingSpecial && !isDead && !isShielding)
            {
                StartCoroutine(PlayerBasicAttack());
            }
            if (thisController.buttonWest.wasPressedThisFrame)
            {
                PlayerPickupManager.UseItem();
            }
            if (thisController.buttonSouth.wasPressedThisFrame && !isDashing && !isUsingSpecial && !isDead && !isShielding && canDash)
            {
                isDashing = true;
                dashTimer = dashDuration;
                dashCdTimer = dashCooldown;
            }
            if (thisController.rightTrigger.wasPressedThisFrame && !isAttacking && !isShooting && !isDashing && !isUsingSpecial && !isDead)
            {
                forceFieldOuter.SetActive(true);
                isShielding = true;
            }
            else if (thisController.rightTrigger.wasReleasedThisFrame)
            {
                forceFieldOuter.SetActive(false);
                isShielding = false;
            }
            if (thisController.leftTrigger.wasPressedThisFrame)
            {
                isStrafing = true;
            }
            else if (thisController.leftTrigger.wasReleasedThisFrame)
            {
                isStrafing = false;
            }

            if (isShielding)
            {
                shieldHealth -= 1 * Time.deltaTime;
                //print(shieldHealth);
                forcefieldSlider.value = -shieldHealth;
            }

            if (shieldHealth <= 0)
            {
                forceFieldOuter.SetActive(false);
                isShielding = false;
                shieldCD -= Time.deltaTime;
                if (!forcefieldSliderAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShieldFlash"))
                {
                    forcefieldSliderAnimator.Play("ShieldFlash");
                }
                forcefieldSlider.value = -3 + (shieldCD / 3);
            }

            if (shieldCD <= 0)
            {
                shieldHealth = 3f;
                shieldCD = 10f;
            }

            if (dashTimer > 0 && !isDashing)
            {
                dashTimer -= Time.deltaTime;
            }

            if (dashCdTimer > 0)
            {
                dashCdTimer -= Time.deltaTime;
                canDash = false;
            }
            else
            {
                dashCdTimer = 0;
                canDash = true;
            }

            HandleCamera();

            ChangeDirection();

            //|| thisController.startButton.wasPressedThisFrame
            if (thisController != null)
            {
                if (Input.GetKeyDown(KeyCode.Escape) || thisController.startButton.wasPressedThisFrame)
                {
                    PauseScript.PauseGame();
                }
            }
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

        
        if (hp <= 0 && !isDead)
        {
            StartCoroutine(OnDeath());
        }

        
    }

    void FixedUpdate()
    {
        if (canMove && acceptInput)
        {
            Move();
        }
        
    }

    private void HandleCamera()
    {
        float rightStickX = 0.0f;
        float rightStickY = 0.0f;
        
        rightStickX = thisController.rightStick.ReadValue().x;
        rightStickY = thisController.rightStick.ReadValue().y;

        //z
        cameraYaw += rightStickX * cameraSensitivity;
        //cameraYaw = Mathf.Clamp(cameraYaw, 0.0f, 1.0f);

        //y
        cameraPitch -= rightStickY * cameraSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, 10f, 60f);

        Quaternion rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0);
        Vector3 newPosition = transform.position + rotation * cameraOffset;

        playerCamera.transform.position = newPosition;
        playerCamera.transform.LookAt(transform.position + Vector3.up * 3f);
    }

    void ChangeDirection()
    {
        float leftStickX = 0.0f;
        float leftStickZ = 0.0f;

        leftStickX = thisController.leftStick.ReadValue().x;
        leftStickZ = thisController.leftStick.ReadValue().y;

        float deadZone = 0.05f;
        if (Mathf.Abs(leftStickX) < deadZone) leftStickX = 0.0f;
        if (Mathf.Abs(leftStickZ) < deadZone) leftStickZ = 0.0f;

        Vector3 move = new Vector3(leftStickX, 0, leftStickZ).normalized;
        moveDirection = playerCamera.transform.TransformDirection(move);
        moveDirection.y = 0;
        moveDirection.Normalize();


        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
        }
    }

    void Move()
    {
        
        if (!bearFireMovement)
        {
            if (!isDead && !isTakingDamage && !isShielding)
            {
                rbody.velocity = moveDirection * currentSpeed;
            }
            else
            {
                rbody.velocity = Vector3.zero;
            }
        }


        if (!isDashing && !isAttacking && !isShooting && !isDead && !isTakingDamage && !isUsingSpecial)
        {
            if (rbody.velocity != Vector3.zero)
            {
                anim.Play(runAnim);
                lastMoveDirection = moveDirection;

                if (!isPlayingFootstep)
                {
                    StartCoroutine(PlayFootsteps());
                }
            }
            else
            {
                anim.Play(idleAnim);
                audioSource.Stop();
                StopCoroutine(PlayFootsteps());
                isPlayingFootstep = false;
            }

            if (moveDirection != Vector3.zero && !isStrafing)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (isDashing)
        {
            rbody.velocity = transform.forward * dashSpeed;
            anim.Play(dashAnim);
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0.0f)
            {
                isDashing = false;
                //dashTimer = dashCooldown;
            }
        }
    }

    private IEnumerator PlayFootsteps()
    {
        isPlayingFootstep = true;

        while (rbody.velocity != Vector3.zero)
        {
            audioSource.clip = footsteps[footstepIndex];
            audioSource.Play();
            footstepIndex = (footstepIndex + 1) % footsteps.Length;

            // Wait for the footstep interval before playing the next sound
            yield return new WaitForSeconds(footstepInterval);
        }

        isPlayingFootstep = false;
    }

    IEnumerator PlayerBasicAttack()
    {        
        isAttacking = true;
        anim.Play(attack1Anim);
        attackSoundSource.PlayOneShot(audioClips[0]);
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(attack1Anim))
        {
            yield return new WaitForSeconds(currentState.length - 0.6f);
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
            if (otherPlayer.isDead)
            {
                PickupItem.canPickup = false;
            }
        }
        
    }


    public void DamagePlayer(float damage)
    {
        StartCoroutine(TakeDamage(damage));
    }

    public IEnumerator TakeDamage(float damage)
    {
        if (!isShielding)
        {
            hp -= damage;
            currentSpeed = 0;
            isTakingDamage = true;
            anim.Play(takeHit1Anim);
            audioSource.clip = audioClips[1];
            audioSource.Play();
            SMR.material.color = Color.red;
        }
        //Debug.Log(gameObject.name + " HP: " + hp);
        inCombat = true;
        inCombatTimer = inCombatLength;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 5);
        currentSpeed = speed;
        SMR.material.color = originalColor;
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


    public void PlayArrowAnim(GameObject bow, bool destroy)
    {
        StartCoroutine(PlayerShootArrow(bow, destroy));
    }

    IEnumerator PlayerShootArrow(GameObject bow, bool destroy)
    {
        isShooting = true;
        anim.Play(arrowAnim);
        currentSpeed = 0;

        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(arrowAnim))
        {
            yield return new WaitForSeconds(currentState.length - 0.6f);
        }

        currentSpeed = speed;

        if (destroy)
        {
            Destroy(bow);
        }        
        isShooting = false;
    }

    IEnumerator OnDeath()
    {
        isDead = true;
        acceptInput = false;
        PickupItem.canPickup = false;
        PlayerPickupManager.hasItem = false;
        PlayerPickupManager.itemText.text = "Current Item: None";
        PlayerPickupManager.itemIconUI.gameObject.SetActive(false);
        PlayerPickupManager.controlIcon.SetActive(false);
        PlayerPickupManager.itemCooldown.SetActive(false);
        if (PlayerPickupManager.bowHolding)
        {
            Destroy(PlayerPickupManager.bowHolding);
        }

        PlayerPickupManager.bowHolding = null;
        anim.Play(deathAnim);
        capCol.enabled = false;
        yield return new WaitForSeconds(0.1f);
        rbody.velocity = Vector3.zero;
        audioSource.PlayOneShot(audioClips[3]);
        respawnScreen.SetActive(true);
        int score = PickupItem.CurrentScore();
        int amount = dropNumber;
        if (amount > score)
        {
            amount = PickupItem.CurrentScore();
        }
        else
        {
            amount = dropNumber;
        }
        
        float radius = 5f;
        Vector3 center = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
        for (int i = 0; i < amount; i++)
        {
            //Vector3 pos = RandomCircle(center, radius);
            //Vector3 pos = 
            Quaternion rot = Quaternion.Euler(270, 0, 0);
            //GameObject target = Instantiate(targetPrefab, pos, rot);
            GameObject fruit = Instantiate(fruitPrefab, gameObject.transform.position, rot);
            fruit.GetComponent<FruitScript>().Drop(i, radius);
            //fruit.GetComponent<FruitScript>().target = target.transform;
           // fruit.GetComponent<FruitScript>().canDrop = true;
        }
        capCol.enabled = false;
        PickupItem.DropScoreOnDeath(amount);

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
        PickupItem.canPickup = true;
        acceptInput = true;
        respawnScreen.SetActive(false);
    }
}
