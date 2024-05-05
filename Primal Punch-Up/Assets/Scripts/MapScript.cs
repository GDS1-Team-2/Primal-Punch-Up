using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] bases;
    public GameObject[] fruitSpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnItems());
        for (int i = 0; i < bases.Length; i++)
        {
            if (i >= PlayerPrefs.GetInt("noOfPlayers"))
            {
                bases[i].SetActive(false);
            }
        }
        //StartCoroutine(SpawnFruit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(5);
        int rand = Random.Range(0, spawnPoints.Length);
        /*while (spawnPoints[rand].activeSelf == true)
        {
            rand = Random.Range(0, spawnPoints.Length);
        }*/
        if (spawnPoints[rand].activeSelf == false)
        {
            spawnPoints[rand].SetActive(true);
        }
        StartCoroutine(SpawnItems());
    }
    IEnumerator SpawnFruit()
    {
        yield return new WaitForSeconds(5);
        int rand = Random.Range(0, fruitSpawnPoints.Length);
        if (fruitSpawnPoints[rand].activeSelf == false)
        {
            fruitSpawnPoints[rand].SetActive(true);
        }
        StartCoroutine(SpawnFruit());
    }
}
