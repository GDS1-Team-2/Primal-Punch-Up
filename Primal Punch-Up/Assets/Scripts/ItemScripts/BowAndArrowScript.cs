using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAndArrowScript : MonoBehaviour

{
    public GameObject ArrowPrefab;
    public GameObject bowString;
    public GameObject otherHand;
    private bool isShooting = false;
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
        if (isShooting)
        {
            bowString.transform.position = otherHand.transform.position;
        }
    }

    public void ShootArrow(Vector3 playerPos, GameObject thisPlayer)
    {
        StartCoroutine(ShootArrowCR(playerPos, thisPlayer));
        
    }

    IEnumerator ShootArrowCR(Vector3 playerPos, GameObject thisPlayer)
    {
        isShooting = true;
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

        yield return new WaitForSeconds(0.7f);

        Vector3 instantiatePosition = playerPos + new Vector3(0, 2, 0);
        GameObject arrow = Instantiate(ArrowPrefab, gameObject.transform.position, gameObject.transform.rotation);
        ArrowScript thisArrow = arrow.GetComponent<ArrowScript>();
        thisArrow.thisPlayer = thisPlayer;
        thisArrow.target = targetPlayer;
        isShooting = false;
        bowString.transform.localPosition = new Vector3(0, -0.008f, 0);
    }
   
}
