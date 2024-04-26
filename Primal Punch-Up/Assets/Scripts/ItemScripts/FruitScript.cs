using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerNo;
    void Start()
    {
        playerNo = 0;
    }

    public void StartPickupAfterDrop(int number)
    {
        playerNo = number;
        StartCoroutine(PlayerPickupAfterDrop());
    }

    IEnumerator PlayerPickupAfterDrop()
    {
        yield return new WaitForSeconds(1);
        playerNo = 0;
    }
}
