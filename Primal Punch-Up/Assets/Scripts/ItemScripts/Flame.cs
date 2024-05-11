using System.Collections;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public int playerNo;
    public int damage = 5; 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lizard") ||
            other.gameObject.CompareTag("Bear") ||
            other.gameObject.CompareTag("Fox") ||
            other.gameObject.CompareTag("Rabbit"))
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo != playerNo)
            {
                other.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);
            }
        }
    }

    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
    }
}
