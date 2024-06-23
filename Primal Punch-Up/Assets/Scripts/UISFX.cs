using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour
{

    private bool deleteAudioSource = false;
    public AudioSource uisfx;
    List<GameObject> uisfxGameObjects = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        uisfx = GetComponent<AudioSource>();
        // Find all game objects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // Filter game objects that contain the phrase "Menu Music"
        foreach (GameObject obj in allGameObjects)
        {
            if (obj.name.Contains("UI SFX"))
            {
                uisfxGameObjects.Add(obj);
            }
        }

        if (uisfxGameObjects.Count > 1)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX()
    {
        uisfx.Play();
    }
}
