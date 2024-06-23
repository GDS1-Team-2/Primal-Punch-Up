using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudio : MonoBehaviour
{

    private bool deleteAudioSource = false;
    public AudioSource audioSource;
    private string currentSceneName;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Find all game objects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // Filter game objects that contain the phrase "Menu Music"
        foreach (GameObject obj in allGameObjects)
        {
            if (obj.name.Contains("Menu Music"))
            {
                if (deleteAudioSource)
                {
                    Destroy(obj);
                } else
                {
                    deleteAudioSource = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Map 1" || currentSceneName == "Map 2" || currentSceneName == "Map 3")
        {
            audioSource.Stop();
        } else
        {
            if (!audioSource.isPlaying)
            {
                print("play the audio source");
                audioSource.Play();
            }
        }
    }
}
