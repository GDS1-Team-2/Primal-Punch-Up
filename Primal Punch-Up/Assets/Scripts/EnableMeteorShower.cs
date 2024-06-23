using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableMeteorShower : MonoBehaviour
{
    private int meteorChanceDenominator = 6;
    private int meteorChance;
    private bool willShower = false;
    private float timeUntilShower = 0.0f;
    MeteorSpawner spawnerScript;
    public GameObject meteorWarning;

    //commen
    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = GetComponent<MeteorSpawner>();
        meteorChance = Random.Range(1, meteorChanceDenominator);
        if (meteorChance == 1)
        {
            willShower = true;
            print("will it shower? " + willShower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (willShower)
        {
            timeUntilShower += Time.deltaTime;
        }

        if (timeUntilShower >= 85  && timeUntilShower <= 90)
        {
            meteorWarning.SetActive(true);
        }
        else if (timeUntilShower >= 90)
        {
            meteorWarning.SetActive(false);
            spawnerScript.enabled = true;
        }
    }
}
