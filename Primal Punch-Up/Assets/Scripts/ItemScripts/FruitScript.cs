using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerNo;
    public bool canDrop = true;
    void Start()
    {
        playerNo = 0;
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
