using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityScale = 1.0f;
    public float globalGravity = -9.81f;
    private Rigidbody rb;
    public float force = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 gravity = new Vector3(0, globalGravity * gravityScale, 0);
        //rb.AddForce(gravity, ForceMode.Acceleration);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
