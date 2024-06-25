using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LizardUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public SkinnedMeshRenderer smr;
    public GameObject smokePrefab;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    Vector3 smokeSpawnLoc = new Vector3(0, 2, 0);
    public float invisTime = 3.0f;

    public Slider cooldownSlider;
    public Image cooldownIcon;
    public Image cooldownGray;
    public Sprite iconSprite;
    public Sprite graySprite;

    public AudioSource audioSource;
    public AudioClip clip;

    public bool usedAbility = false;

    //commen
    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<PlayerBase>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
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
        if (!abilityCD && !usedAbility)
        {
            if (baseScript.thisController.buttonWest.wasPressedThisFrame && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead && !baseScript.isShielding)
            {
                StartCoroutine(LizardAttack());
            }
        }

        if (abilityCD)
        {
            usedAbility = false;
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

    public IEnumerator LizardAttack()
    {
        usedAbility = true;
        audioSource.clip = clip;
        audioSource.Play();
        smr.enabled = false;
        GameObject particlesInstance = Instantiate(smokePrefab, transform.position + smokeSpawnLoc, Quaternion.identity);
        LizardSmoke thisLizardSmoke = particlesInstance.GetComponent<LizardSmoke>();
        PlayerBase thisPlayer = GetComponent<PlayerBase>();
        thisLizardSmoke.thisPlayer = thisPlayer;
        StartCoroutine(thisLizardSmoke.SmokeCollider());
        yield return new WaitForSeconds(invisTime);
        particlesInstance = Instantiate(smokePrefab, transform.position + smokeSpawnLoc, Quaternion.identity);
        LizardSmoke thisNewLizardSmoke = particlesInstance.GetComponent<LizardSmoke>();
        thisNewLizardSmoke.thisPlayer = thisPlayer;
        StartCoroutine(thisNewLizardSmoke.SmokeCollider());
        smr.enabled = true;
        abilityCD = true;
        cdTimer = cdLength;
    }
}
