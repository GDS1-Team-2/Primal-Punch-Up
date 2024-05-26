using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    public int playerNo;
    public List<GameObject> players;
    public bool ending = false;
    public bool playSound = true;
    // Start is called before the first frame update

    private void Start()
    {
        playSound = true;
        ending = false;
    }
    public void SetEnding(bool state)
    {
        ending = state;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && playSound)
        {
            gameObject.GetComponent<AudioSource>().Play();
            playSound = false;
        }
        if (ending)
        {
            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    player.GetComponent<PlayerBase>().speed = player.GetComponent<PlayerBase>().baseSpeed;
                    player.GetComponent<PlayerBase>().currentSpeed = player.GetComponent<PlayerBase>().baseSpeed;
                    player.GetComponent<PlayerBase>().usingIce = false;
                }
            }
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lizard") ||
            other.gameObject.CompareTag("Bear") ||
            other.gameObject.CompareTag("Fox") ||
            other.gameObject.CompareTag("Rabbit"))
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo != playerNo)
            {
                players.Add(other.gameObject);
                other.gameObject.GetComponent<PlayerBase>().speed /= 2;
                other.gameObject.GetComponent<PlayerBase>().currentSpeed /= 2;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lizard") ||
            other.gameObject.CompareTag("Bear") ||
            other.gameObject.CompareTag("Fox") ||
            other.gameObject.CompareTag("Rabbit"))
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo != playerNo)
            {
                players.Remove(other.gameObject);
                other.gameObject.GetComponent<PlayerBase>().speed = other.gameObject.GetComponent<PlayerBase>().baseSpeed;
                other.gameObject.GetComponent<PlayerBase>().currentSpeed = other.gameObject.GetComponent<PlayerBase>().baseSpeed;
            }
        }
    }
}
