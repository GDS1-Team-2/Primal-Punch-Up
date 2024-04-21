using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public bool move = true;
    public int playerNo;
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
        if (other.gameObject.CompareTag("Lizard") ||
            other.gameObject.CompareTag("Bear") ||
            other.gameObject.CompareTag("Fox") ||
            other.gameObject.CompareTag("Rabbit"))
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo != playerNo)
            {
                StartCoroutine(Trapped(other.gameObject));
            }
        }
    }

    IEnumerator Trapped(GameObject player)
    {
        move = false;
        player.GetComponent<PlayerBase>().canMove = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.transform.position = gameObject.transform.position;

        yield return new WaitForSeconds(3.0f);


        player.GetComponent<PlayerBase>().canMove = true;
        move = true;

        Destroy(gameObject);
    }
}
