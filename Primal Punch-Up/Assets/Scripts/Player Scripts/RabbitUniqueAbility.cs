using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public Animator anim;
    public GameObject carrotKnifePrefab;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    public float throwSpeed = 25.0f;

    private string abilityAnim = "RabbitUniqueAbility";

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
                StartCoroutine(RabbitAttack());
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

    IEnumerator RabbitAttack()
    {
        baseScript.isUsingSpecial = true;
        anim.Play(abilityAnim);
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(abilityAnim))
        {
            yield return new WaitForSeconds(currentState.length - 0.6f);
            abilityCD = true;
            cdTimer = cdLength;
        }
        baseScript.isUsingSpecial = false;
    }

    public void CarrotToss()
    {
        Vector3 instantiatePosition = transform.position + Vector3.up;
        GameObject carrotKnife = Instantiate(carrotKnifePrefab, instantiatePosition, gameObject.transform.rotation);
        PlayerBase thisPlayer = GetComponent<PlayerBase>();
        CarrotDagger thisCarrot = carrotKnife.GetComponent<CarrotDagger>();
        thisCarrot.thisPlayer = thisPlayer;
    }
}
