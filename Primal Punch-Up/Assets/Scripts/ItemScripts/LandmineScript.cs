using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineScript : MonoBehaviour
{
    public float explosionForce = 10.0f;
    public float explosionRadius = 5.0f;
    public int damage = 25;
    public float knockBackForce;
    public ParticleSystem particleSystem;
    public AudioClip explodeSound;
    public int playerNo;
    public GameObject model;
    public GameObject mineBase;
    public Material RedMaterial;
    public Material BlueMaterial;
    public Material GreenMaterial;
    public Material YellowMaterial;
    public SpriteRenderer ring;
    public SpriteRenderer area;
    // Start is called before the first frame update
    void Start()
    {
        //particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        //particleSystem = gameObject.GetComponent<ParticleSystem>();
        //particleSystem.Stop();
        switch (playerNo)
        {
            case 1:
                mineBase.GetComponent<MeshRenderer>().material = BlueMaterial;
                ring.color = BlueMaterial.color;
                area.color = new Color(BlueMaterial.color.r, BlueMaterial.color.g, BlueMaterial.color.b, 0.1843137f);
                break;
            case 2:
                mineBase.GetComponent<MeshRenderer>().material = RedMaterial;
                ring.color = RedMaterial.color;
                area.color = new Color(RedMaterial.color.r, RedMaterial.color.g, RedMaterial.color.b, 0.1843137f);
                break;
            case 3:
                mineBase.GetComponent<MeshRenderer>().material = GreenMaterial;
                ring.color = GreenMaterial.color;
                area.color = new Color(GreenMaterial.color.r, GreenMaterial.color.g, GreenMaterial.color.b, 0.1843137f);
                break;
            case 4:
                mineBase.GetComponent<MeshRenderer>().material = YellowMaterial;
                ring.color = YellowMaterial.color;
                area.color = new Color(YellowMaterial.color.r, YellowMaterial.color.g, YellowMaterial.color.b, 0.1843137f);
                break;
        }
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
                particleSystem.Play();
                gameObject.GetComponent<AudioSource>().PlayOneShot(explodeSound);
                //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, explosionRadius, 3.0F);
                other.gameObject.GetComponent<PlayerBase>().DamagePlayer(damage);
                Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
                other.gameObject.GetComponent<Rigidbody>().AddForce(direction * knockBackForce);
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                model.SetActive(false);
                ring.enabled = false;
                area.enabled = false;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
