using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public Animator anim;
    public ParticleSystem firePrefab;
    public GameObject rightHand;
    public SphereCollider damageCollider;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    public float punchSpeed = 40.0f;
    private string abilityAnim = "BearUniqueAbility";

    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<PlayerBase>();
        anim = GetComponent<Animator>();
        damageCollider = GetComponent<SphereCollider>();
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
                    StartCoroutine(BearAttack());
                }
            }
            else if (baseScript.playerNo == 3)
            {
                if (baseScript.P3Controller.buttonNorth.wasPressedThisFrame && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead)
                {
                    StartCoroutine(BearAttack());
                }
            }
            else if (baseScript.playerNo == 4)
            {
                if (baseScript.P4Controller.buttonNorth.wasPressedThisFrame && !baseScript.isAttacking && !baseScript.isDashing && !baseScript.isUsingSpecial && !baseScript.isDead)
                {
                    StartCoroutine(BearAttack());
                }
            }
        }

        if (abilityCD)
        {
            cdTimer -= 1 * Time.deltaTime;
            if (cdTimer < 0)
            {
                cdTimer = 0.0f;
                abilityCD = false;
            }
        }
    }

    IEnumerator BearAttack()
    {
        baseScript.isUsingSpecial = true;
        anim.Play("BearUniqueAbility");
        yield return new WaitForSeconds(0.1f);

        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName(abilityAnim))
        {
            yield return new WaitForSeconds(currentState.length);
            ParticleSystem[] fireInstances = FindObjectsOfType<ParticleSystem>();
            abilityCD = true;
            cdTimer = cdLength;
            damageCollider.enabled = false;
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
        //baseScript.currentSpeed = punchSpeed;
        damageCollider.enabled = true;
        baseScript.bearFireMovement = true;
    }

    public void PunchMoveEnd()
    {
        baseScript.currentSpeed = baseScript.speed;
        damageCollider.enabled = false;
        baseScript.bearFireMovement = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        int firePunchDamage = 25;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != baseScript && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage(firePunchDamage));
        }
    }

}
