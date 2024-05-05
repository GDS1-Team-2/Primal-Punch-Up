using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public ParticleSystem particles;
    public SphereCollider sphereCol;
    private float randTime;
    private float timeUntilStart = 0f;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        sphereCol = GetComponent<SphereCollider>();
        randTime = Random.Range(30f, 70f);
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilStart = timeUntilStart + 1 * Time.deltaTime;

        if (timeUntilStart == randTime)
        {
            particles.Play();
            sphereCol.enabled = true;
        }
    }
}
