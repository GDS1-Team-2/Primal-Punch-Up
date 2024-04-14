using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupManager : MonoBehaviour
{
    public int numberOfItems = 2;
    public List<string> items = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

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
            // Destroy(other.gameObject);
            int rand = Random.Range(0, numberOfItems);
            switch (rand)
            {
                case 0:
                    //trap
                    items.Add("Trap");
                    break;
                case 1:
                    // landmine
                    items.Add("Landmine");
                    break;
            }
        }
    }
}
