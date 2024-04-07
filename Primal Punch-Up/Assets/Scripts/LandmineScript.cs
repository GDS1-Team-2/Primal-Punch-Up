using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineScript : MonoBehaviour
{
    public float explosionForce = 10.0f;
    public float explosionRadius = 5.0f;
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
            other.gameObject.CompareTag("Bear"))
        {
            Debug.Log("explode");
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 3.0f, ForceMode.Impulse);
        }
    }
}
