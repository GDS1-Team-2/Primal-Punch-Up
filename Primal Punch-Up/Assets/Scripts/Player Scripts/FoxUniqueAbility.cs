using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public ParticleSystem magicPrefab;
    public Animator anim;
    public SphereCollider damageCollider;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    Vector3 magicSpawnLoc = new Vector3(0, 0.1f, 0);

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
                    StartCoroutine(FoxAttack());
                }
            }
            else if (baseScript.playerNo == 3)
            {
                if (baseScript.P3Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(FoxAttack());
                }
            }
            else if (baseScript.playerNo == 4)
            {
                if (baseScript.P4Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(FoxAttack());
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

    public IEnumerator FoxAttack()
    {
        if (baseScript.rbody.velocity.magnitude > 0.1f)
        {
            baseScript.canMove = false;
            ParticleSystem particlesInstance = Instantiate(magicPrefab, transform.position + magicSpawnLoc, Quaternion.LookRotation(Vector3.up));
            baseScript.rbody.velocity = Vector3.zero;
            anim.Play("FoxUniqueAbility");
            damageCollider.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 2);
            Destroy(particlesInstance);
            abilityCD = true;
            cdTimer = cdLength;
            baseScript.canMove = true;
            damageCollider.enabled = false;
        } else
        {
            baseScript.canMove = false;
            ParticleSystem particlesInstance = Instantiate(magicPrefab, transform.position + magicSpawnLoc, Quaternion.LookRotation(Vector3.up));
            anim.Play("FoxUniqueAbility");
            damageCollider.enabled = true;
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(particlesInstance);
            abilityCD = true;
            cdTimer = cdLength;
            baseScript.canMove = true;
            damageCollider.enabled = false;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        int vortexDamage = 20;
        PlayerBase otherPlayer = other.gameObject.GetComponent<PlayerBase>();

        if (otherPlayer != null && otherPlayer != baseScript && !other.isTrigger)
        {
            StartCoroutine(otherPlayer.TakeDamage(vortexDamage));
        }
    }
}

