using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public GameObject Meteor;
    public float meteorCD = 0.5f;
    private float meteorCurrent = 0.0f;
    private Vector3 meteorSpawnPos;
    private float randX;
    private float randZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meteorCurrent += Time.deltaTime;

        if (meteorCurrent >= meteorCD)
        {
            meteorCurrent = 0.0f;
            randX = Random.Range(-80, 80);
            randZ = Random.Range(-80, 80);
            meteorSpawnPos = new Vector3(randX, this.transform.position.y, randZ);
            Instantiate(Meteor, meteorSpawnPos, Quaternion.Euler(270, 0, 0));
        }

    }
}
