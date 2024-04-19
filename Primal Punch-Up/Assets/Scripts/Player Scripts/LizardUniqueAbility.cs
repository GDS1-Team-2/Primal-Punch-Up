using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardUniqueAbility : MonoBehaviour
{

    private PlayerBase baseScript;
    public SkinnedMeshRenderer smr;
    public ParticleSystem smokePrefab;

    private bool abilityCD = false;
    private float cdTimer = 0.0f;
    public float cdLength = 5.0f;
    Vector3 smokeSpawnLoc = new Vector3(0, 2, 0);

    // Start is called before the first frame update
    void Start()
    {
        baseScript = GetComponent<PlayerBase>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
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
                    StartCoroutine(LizardAttack());
                }
            }
            else if (baseScript.playerNo == 3)
            {
                if (baseScript.P3Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(LizardAttack());
                }
            }
            else if (baseScript.playerNo == 4)
            {
                if (baseScript.P4Controller.buttonNorth.wasPressedThisFrame)
                {
                    StartCoroutine(LizardAttack());
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

    public IEnumerator LizardAttack()
    {
        smr.enabled = false;
        ParticleSystem particlesInstance = Instantiate(smokePrefab, transform.position + smokeSpawnLoc, Quaternion.identity);
        yield return new WaitForSeconds(3);
        particlesInstance = Instantiate(smokePrefab, transform.position + smokeSpawnLoc, Quaternion.identity);
        smr.enabled = true;
        abilityCD = true;
        cdTimer = cdLength;
    }
}
