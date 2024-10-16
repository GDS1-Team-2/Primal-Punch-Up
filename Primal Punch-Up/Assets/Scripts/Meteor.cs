using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public Rigidbody rbody;
    public float speed = 10.0f;
    public GameObject meteorExplosion;
    public float floorLevel;
    public GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = Vector3.down * speed;
        indicator.transform.position = new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Floor")
        {
            Instantiate(meteorExplosion, this.transform.position, Quaternion.Euler(90, 0, 0));
            StartCoroutine(DestroyAfterTime());
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
