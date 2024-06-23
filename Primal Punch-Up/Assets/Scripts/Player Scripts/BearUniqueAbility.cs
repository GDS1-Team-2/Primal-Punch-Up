using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BearUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public Animator anim;
    public ParticleSystem firePrefab;
    public GameObject rightHand;
    public GameObject damageCollider;
    private GameObject newFireCollider;

    private bool abilityCD = false;
    public float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    public float punchSpeed = 40.0f;
    private string abilityAnim = "BearUniqueAbility";

    public Slider cooldownSlider;
    public Image cooldownIcon;
    public Image cooldownGray;
    public Sprite iconSprite;
    public Sprite graySprite;

    public AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<PlayerBase>();
        anim = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        string playerCooldownSlider = "Player" + baseScript.playerNo + "AbilityCooldown";
        cooldownSlider = GameObject.Find(playerCooldownSlider).GetComponent<Slider>();
        cooldownSlider.maxValue = cdLength;
        cooldownSlider.value = 0;
        string playerCooldownIcon = "Player" + baseScript.playerNo + "AbilityIcon";
        cooldownIcon = GameObject.Find(playerCooldownIcon).GetComponent<Image>();
        cooldownIcon.sprite = iconSprite;
        string playerCooldownGray = "Player" + baseScript.playerNo + "AbilityGray";
        cooldownGray = GameObject.Find(playerCooldownGray).GetComponent<Image>();
        cooldownGray.sprite = graySprite;
        cooldownGray.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!abilityCD)
        {
            if (baseScript.thisController.buttonNorth.wasPressedThisFrame && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead && !baseScript.isShielding)
            {
                StartCoroutine(BearAttack());
            }
        }

        if (abilityCD)
        {
            cooldownGray.enabled = true;
            cooldownSlider.value = cdTimer;
            cdTimer -= 1 * Time.deltaTime;
            if (cdTimer < 0)
            {
                cdTimer = 0.0f;
                abilityCD = false;
                cooldownGray.enabled = false;
            }
        }
    }

    IEnumerator BearAttack()
    {
        baseScript.isUsingSpecial = true;
        anim.Play("BearUniqueAbility");
        
        yield return new WaitForSeconds(0.1f);
        audioSource.clip = clip;
        audioSource.Play();
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(abilityAnim))
        {
            yield return new WaitForSeconds(currentState.length - 0.6f);
            
            ParticleSystem[] fireInstances = FindObjectsOfType<ParticleSystem>();
            abilityCD = true;
            cdTimer = cdLength;
            Destroy(newFireCollider);
            baseScript.bearFireMovement = false;
            foreach (ParticleSystem instance in fireInstances)
            {
                if (instance.name.Contains(firePrefab.name))
                {
                    instance.Stop();
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                }
            }
        }
        baseScript.isUsingSpecial = false;
    }

    public void PunchMoveStart()
    {
        ParticleSystem particlesInstance = Instantiate(firePrefab, transform.position, Quaternion.LookRotation(Vector3.up));
        particlesInstance.transform.SetParent(rightHand.transform);
    }

    public void MovementStart()
    {
        baseScript.rbody.velocity = transform.forward * punchSpeed;
        newFireCollider = Instantiate(damageCollider);
        newFireCollider.transform.SetParent(this.transform);
        newFireCollider.transform.position = this.transform.position;
        newFireCollider.transform.localScale = new Vector3(1, 1, 1);
        FirePunchCollision thisFirePunch = newFireCollider.GetComponent<FirePunchCollision>();
        PlayerBase thisPlayer = GetComponent<PlayerBase>();
        thisFirePunch.thisPlayer = thisPlayer;
        baseScript.bearFireMovement = true;
    }

    public void PunchMoveEnd()
    {
        baseScript.currentSpeed = baseScript.speed;
        Destroy(newFireCollider);
        baseScript.bearFireMovement = false;
    }
}
