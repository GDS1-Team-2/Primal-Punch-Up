using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] bases;
    public GameObject[] fruitSpawnPoints;
    public GameObject[] fruits;
    public List<Vector3> filledPositions;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject position in fruitSpawnPoints)
        {
            int rand = Random.Range(0, fruits.Length);
            Quaternion rot = Quaternion.Euler(270, 0, 0);
            Instantiate(fruits[rand], position.transform.position, rot);
            filledPositions.Add(position.transform.position);
        }

        StartCoroutine(SpawnItems());
        for (int i = 0; i < bases.Length; i++)
        {
            if (i >= PlayerPrefs.GetInt("noOfPlayers"))
            {
                bases[i].SetActive(false);
            }
        }
        StartCoroutine(SpawnFruit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(5);
        int rand = Random.Range(0, spawnPoints.Length);
        if (spawnPoints[rand].activeSelf == false)
        {
            spawnPoints[rand].SetActive(true);
        }
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnFruit()
    {
        yield return new WaitForSeconds(5);
        int a = Random.Range(0, fruitSpawnPoints.Length);
        if (!filledPositions.Contains(fruitSpawnPoints[a].transform.position))
        {
            int rand = Random.Range(0, fruits.Length);
            Quaternion rot = Quaternion.Euler(270, 0, 0);
            Instantiate(fruits[rand], fruitSpawnPoints[a].transform.position, rot);
            filledPositions.Add(fruitSpawnPoints[a].transform.position);
        } 
        StartCoroutine(SpawnFruit());
    }
}
