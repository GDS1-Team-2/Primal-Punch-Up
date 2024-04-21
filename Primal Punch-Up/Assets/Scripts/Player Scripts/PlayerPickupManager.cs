using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupManager : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public GameObject currentItem;

    public PlayerBase PlayerBase;
    public int playerNo;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerBase = gameObject.GetComponent<PlayerBase>();
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
        }
    }

    public void PlaceItem()
    {
        currentItem = Instantiate(currentItem, gameObject.transform.position, Quaternion.identity);
    }
}
