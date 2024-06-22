using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAndArrowScript : MonoBehaviour

{
    public GameObject ArrowPrefab;
    public GameObject bowString;
    //string default is 0, -0.008, 0
    //max is 0, -0.0231, 0

    // public GameObject thisPlayer;
    //private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootArrow(Vector3 playerPos, GameObject thisPlayer)
    {
        GameObject targetPlayer = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = thisPlayer.transform.position;
        PlayerBase[] players = FindObjectsOfType<PlayerBase>();

        foreach (PlayerBase player in players)
        {
            Debug.Log("Found player: " + player.name);
            float dist = Vector3.Distance(player.transform.position, currentPos);
            if (player.gameObject != thisPlayer && dist < minDist)
            {
                targetPlayer = player.gameObject;
                minDist = dist;
            }
        }
        
        Vector3 instantiatePosition = playerPos + new Vector3(0,2,0);
        GameObject arrow = Instantiate(ArrowPrefab, gameObject.transform.position, gameObject.transform.rotation);
        ArrowScript thisArrow = arrow.GetComponent<ArrowScript>();
        thisArrow.thisPlayer = thisPlayer;
        thisArrow.target = targetPlayer;
    }

   
}
