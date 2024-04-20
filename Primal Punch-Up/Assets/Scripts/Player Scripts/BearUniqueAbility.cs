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
    public float punchSpeed = 5.0f;

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
                if (baseScript.attack2Key.HasValue && Input.GetKey(baseScript.attack2Key.Value))
                {
                    StartCoroutine(BearAttack());
                }
            }
            else if (baseScript.playerNo == 3)
            {
                if (baseScript.P3Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(BearAttack());
                }
            }
            else if (baseScript.playerNo == 4)
            {
                if (baseScript.P4Controller.buttonNorth.wasPressedThisFrame)
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
        if (baseScript.rbody.velocity.magnitude > 0.1f)
        {
            baseScript.canMove = false;
            baseScript.rbody.velocity = Vector3.zero;
            anim.Play("BearUniqueAbility");
            damageCollider.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 3);
            baseScript.rbody.velocity = Vector3.zero;
            baseScript.canMove = true;
            ParticleSystem[] fireInstances = FindObjectsOfType<ParticleSystem>();
            abilityCD = true;
            cdTimer = cdLength;
            damageCollider.enabled = false;
            foreach (ParticleSystem instance in fireInstances)
            {
                if (instance.name.Contains(firePrefab.name))
                {
                    instance.Stop();
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                }
            }
        } else
        {
            baseScript.canMove = false;
            anim.Play("BearUniqueAbility");
            damageCollider.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            baseScript.rbody.velocity = Vector3.zero;
            baseScript.canMove = true;
            ParticleSystem[] fireInstances = FindObjectsOfType<ParticleSystem>();
            abilityCD = true;
            cdTimer = cdLength;
            damageCollider.enabled = false;
            foreach (ParticleSystem instance in fireInstances)
            {
                if (instance.name.Contains(firePrefab.name))
                {
                    instance.Stop();
                    Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
                }
            }
        }
    }

    public void PunchMoveStart()
    {
        ParticleSystem particlesInstance = Instantiate(firePrefab, transform.position, Quaternion.LookRotation(Vector3.up));
        particlesInstance.transform.SetParent(rightHand.transform);
    }

    public void MovementStart()
    {
        baseScript.rbody.velocity = transform.forward * punchSpeed;
    }

    public void PunchMoveEnd()
    {
        baseScript.rbody.velocity = Vector3.zero;
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
