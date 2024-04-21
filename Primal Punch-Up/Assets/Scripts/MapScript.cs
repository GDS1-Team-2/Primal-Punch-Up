using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5);
        int rand = Random.Range(0, spawnPoints.Length);
        while (spawnPoints[rand].activeSelf == true)
        {
            rand = Random.Range(0, spawnPoints.Length);
        }
        spawnPoints[rand].SetActive(true);
        StartCoroutine(Spawn());
    }
}
