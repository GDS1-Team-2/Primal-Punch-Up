using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public bool move = true;
    public int playerNo;
    public GameObject model;
    public Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VisibleForSeconds(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator VisibleForSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        model.SetActive(false);
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
                model.SetActive(true);
                gameObject.GetComponent<AudioSource>().Play();
                StartCoroutine(Trapped(other.gameObject));
            }
        }
    }

    IEnumerator Trapped(GameObject player)
    {
        move = false;
        player.GetComponent<PlayerBase>().canMove = false;
        playerRB = player.GetComponent<Rigidbody>();
        playerRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY
                               | RigidbodyConstraints.FreezeRotationX
                               | RigidbodyConstraints.FreezeRotationY
                               | RigidbodyConstraints.FreezeRotationZ;
        //player.transform.position = gameObject.transform.position;
        player.transform.LookAt(gameObject.transform);
        float elapsedTime = 0;
        float duration = 1;
        while (elapsedTime < duration)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, gameObject.transform.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        string s = "Player" + player.GetComponent<PlayerBase>().playerNo + "Model";
        string anim = PlayerPrefs.GetString(s) + "Idle";
        player.GetComponent<Animator>().Play(anim);
        yield return new WaitForSeconds(3f);
        /*player.GetComponent<PlayerBase>().TakeDamage(5);
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerBase>().TakeDamage(5);
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerBase>().TakeDamage(5);*/


        player.GetComponent<PlayerBase>().canMove = true;
        move = true;
        playerRB.constraints = (playerRB.constraints & ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ))
                 | RigidbodyConstraints.FreezeRotationX
                 | RigidbodyConstraints.FreezeRotationY
                 | RigidbodyConstraints.FreezeRotationZ;

        Destroy(gameObject);
    }

}
