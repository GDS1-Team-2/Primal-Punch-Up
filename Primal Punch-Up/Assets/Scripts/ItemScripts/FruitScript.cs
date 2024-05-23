using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerNo;
    public bool canDrop = true;

    public Transform target;
    public float speed = 2f;
    public float radius = 1f;
    public float angle = 0f;

    public float throwForce;
    public float throwUpwardForce;

    void Start()
    {
        canDrop = false;
    }

    public void Drop(int i, float radius)
    {
        Vector3 endPos = new Vector3(0,0,0);
        Vector3 halfPos = new Vector3(0, 0, 0);
        float half = radius / 2;
        
        switch (i)
        {
            case 0:
                endPos = new Vector3(gameObject.transform.position.x + radius, 1, gameObject.transform.position.z);
                halfPos = new Vector3(endPos.x - half, 2, endPos.z);
                break;
            case 1:
                endPos = new Vector3(gameObject.transform.position.x - radius, 1, gameObject.transform.position.z);
                halfPos = new Vector3(endPos.x + half, 2, endPos.z);
                break;
            case 2:
                endPos = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z + radius);
                halfPos = new Vector3(endPos.x, 2, endPos.z - half);
                break;
        }
        StartCoroutine(CRDrop(gameObject.transform.position, endPos, 0.5f, halfPos));
    }

    IEnumerator CRDrop(Vector3 startPos, Vector3 endPos, float totalDuration, Vector3 halfPos)
    {
        float elapsedTime = 0;
        float duration = totalDuration / 2;
        
        while (elapsedTime < duration)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, halfPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            gameObject.transform.position = Vector3.Lerp(halfPos, endPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;                                                                                      
            yield return null;
        }
    }

    void Update()
    {
        /*if (canDrop)
        {
            float x = target.position.x + Mathf.Cos(angle) * radius;
            float y = target.position.y;
            float z = target.position.z + Mathf.Sin(angle) * radius;

            transform.position = new Vector3(x, y, z);
            angle += speed * Time.deltaTime;
        }*/
    }

    public void StartPickupAfterDrop(int number)
    {
        playerNo = number;
        if (canDrop)
        {
            StartCoroutine(PlayerPickupAfterDrop());
        }
    }

    IEnumerator PlayerPickupAfterDrop()
    {
        canDrop = false;
        yield return new WaitForSeconds(1);
        playerNo = 0;
        canDrop = true;
    }
}
