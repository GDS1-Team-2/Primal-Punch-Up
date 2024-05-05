using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineScript : MonoBehaviour
{
    public float explosionForce = 10.0f;
    public float explosionRadius = 5.0f;
    public int damage = 1;
    public float knockBackForce = 10;
    public ParticleSystem particleSystem;
    public AudioClip explodeSound;
    public int playerNo;
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        //particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        //particleSystem = gameObject.GetComponent<ParticleSystem>();
        //particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lizard") ||
            other.gameObject.CompareTag("Bear") ||
            other.gameObject.CompareTag("Cat") ||
            other.gameObject.CompareTag("Rabbit") ||
            other.gameObject.CompareTag("Fox"))
        {
            if (other.gameObject.GetComponent<PlayerBase>().playerNo != playerNo)
            {
                Debug.Log("explode");
                //particleSystem.Play();
                gameObject.GetComponent<AudioSource>().PlayOneShot(explodeSound);
                //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, explosionRadius, 3.0F);
                StartCoroutine(other.gameObject.GetComponent<PlayerBase>().TakeDamage(damage));
                Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
                other.gameObject.GetComponent<Rigidbody>().AddForce(direction * knockBackForce);
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                model.SetActive(false);
            }
        }
    }
}
