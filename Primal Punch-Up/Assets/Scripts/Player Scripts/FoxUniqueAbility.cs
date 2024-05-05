using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public GameObject magicPrefab;
    public Animator anim;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    Vector3 magicSpawnLoc = new Vector3(0, 0.1f, 0);

    private string abilityAnim = "FoxUniqueAbility";

    public Slider cooldownSlider;
    public Image cooldownIcon;
    public Image cooldownGray;
    public Sprite iconSprite;
    public Sprite graySprite;

    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<PlayerBase>();
        anim = GetComponent<Animator>();
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
            if (baseScript.playerNo == 1 || baseScript.playerNo == 2)
            {
                if (baseScript.attack2Key.HasValue && Input.GetKey(baseScript.attack2Key.Value) && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead)
                {
                    StartCoroutine(FoxAttack());
                }
            }
            else if (baseScript.playerNo == 3)
            {
                if (baseScript.P3Controller.buttonNorth.wasPressedThisFrame && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead)
                {
                    StartCoroutine(FoxAttack());
                }
            }
            else if (baseScript.playerNo == 4)
            {
                if (baseScript.P4Controller.buttonNorth.wasPressedThisFrame && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead)
                {
                    StartCoroutine(FoxAttack());
                }
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

    public IEnumerator FoxAttack()
    {
        baseScript.isUsingSpecial = true;
        GameObject particlesInstance = Instantiate(magicPrefab, transform.position + magicSpawnLoc, Quaternion.LookRotation(Vector3.up));
        baseScript.currentSpeed = 0;
        FoxVortex thisFoxVortex = particlesInstance.GetComponent<FoxVortex>();
        PlayerBase thisPlayer = GetComponent<PlayerBase>();
        thisFoxVortex.thisPlayer = thisPlayer;
        anim.Play(abilityAnim);
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(abilityAnim))
        {
            yield return new WaitForSeconds(currentState.length);
            ParticleSystem[] vortexInstances = FindObjectsOfType<ParticleSystem>();
            abilityCD = true;
            cdTimer = cdLength;
            baseScript.currentSpeed = baseScript.speed;
            foreach (ParticleSystem instance in vortexInstances)
            {
                if (instance.name.Contains(magicPrefab.name))
                {
                    instance.Stop();
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                }
            }
        }
        baseScript.isUsingSpecial = false;
    }
}

