using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject currentItem;

    public PlayerBase PlayerBase;
    public int playerNo;

    public GameObject[] itemTexts;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerBase = gameObject.GetComponent<PlayerBase>();
        playerNo = PlayerBase.playerNo;
        switch (playerNo)
        {
            case 1:
                itemTexts = GameObject.FindGameObjectsWithTag("Player1CurrentItem");
                break;
            case 2:
                itemTexts = GameObject.FindGameObjectsWithTag("Player2CurrentItem");
                break;
            case 3:
                itemTexts = GameObject.FindGameObjectsWithTag("Player3CurrentItem");
                break;
            case 4:
                itemTexts = GameObject.FindGameObjectsWithTag("Player4CurrentItem");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //pickup items:

        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            int rand = Random.Range(0, items.Count);
            currentItem = items[rand];
            string uitext = "Current Item: " + currentItem.name;
            foreach (GameObject text in itemTexts)
            {
                text.GetComponent<Text>().text = uitext;
            }
        }
    }

    public void UseItem()
    {
        if (currentItem.name == "Trap" || currentItem.name == "Landmine")
        {
            currentItem = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity);
        }
    }
}
