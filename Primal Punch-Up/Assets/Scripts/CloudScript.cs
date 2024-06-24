using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.01f;
    public float max;
    public float min;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(Vector3.zero, Vector3.up, 100 * Time.deltaTime);
        //transform.localPosition = new Vector3(-10, 0, 0);
        transform.localPosition = new Vector3(transform.localPosition.x + speed, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.x > max)
        {
            transform.localPosition = new Vector3(min, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
