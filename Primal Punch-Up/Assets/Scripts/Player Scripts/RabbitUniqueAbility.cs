using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public Animator anim;
    public GameObject carrotKnifePrefab;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    public float throwSpeed = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<PlayerBase>();
        anim = GetComponent<Animator>();
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
                    StartCoroutine(RabbitAttack());
                }
            }
            else if (baseScript.playerNo == 3)
            {
                if (baseScript.P3Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(RabbitAttack());
                }
            }
            else if (baseScript.playerNo == 4)
            {
                if (baseScript.P4Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(RabbitAttack());
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

    IEnumerator RabbitAttack()
    {
        if (baseScript.rbody.velocity.magnitude > 0.1f)
        {
            baseScript.canMove = false;
            baseScript.rbody.velocity = Vector3.zero;
            anim.Play("RabbitUniqueAbility");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 1.5f);
            abilityCD = true;
            cdTimer = cdLength;
            baseScript.canMove = true;
        } else
        {
            baseScript.canMove = false;
            anim.Play("RabbitUniqueAbility");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 2);
            abilityCD = true;
            cdTimer = cdLength;
            baseScript.canMove = true;
        }
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
