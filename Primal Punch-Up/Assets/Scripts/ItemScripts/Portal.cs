using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    public GameObject partnerPortal;
    private List<string> playerTags = new List<string> { "Lizard", "Bear", "Rabbit", "Fox" };
    private float teleportCooldown = 1.0f;
    public float proximityThreshold = 0.5f;

    public GameObject blue;
    public GameObject red;
    public GameObject green;
    public GameObject yellow;

    private int playerNo;

    public void SetPartner(GameObject otherPortal)
    {
        partnerPortal = otherPortal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerTags.Contains(other.tag) && partnerPortal != null)
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo == playerNo)
            {
                StartCoroutine(TeleportAfterDelay(other.gameObject));
            }
        }
    }

    public void SetColour(int playernumber)
    {
        switch (playernumber)
        {
            case 1:
                blue.SetActive(true);
                break;
            case 2:
                red.SetActive(true);
                break;
            case 3:
                green.SetActive(true);
                break;
            case 4:
                yellow.SetActive(true);
                break;
        }
        playerNo = playernumber;
    }

    private IEnumerator TeleportAfterDelay(GameObject player)
    {
        yield return new WaitForSeconds(teleportCooldown);

        if (playerTags.Contains(player.tag) && Vector3.Distance(player.transform.position, this.transform.position) <= proximityThreshold)
        {
            Debug.Log("Player teleported from " + transform.position + " to " + partnerPortal.transform.position);
            player.transform.position = partnerPortal.transform.position;
        }
        else
        {
            Debug.Log("Player moved before teleport could occur.");
        }
    }
}



