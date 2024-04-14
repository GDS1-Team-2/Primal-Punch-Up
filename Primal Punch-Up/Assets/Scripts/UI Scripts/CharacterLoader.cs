using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{

    public int noOfPlayers = 1;
    public int P1Char = 0;
    public int P2Char = 0;
    public int P3Char = 0;
    public int P4Char = 0;
    public bool loadCharacters = false;

    public GameObject Lizard;
    public GameObject Bear;
    public GameObject Rabbit;
    public GameObject Tiger;
    public GameObject Fox;

    public GameObject P1Spawn;
    public GameObject P2Spawn;
    public GameObject P3Spawn;
    public GameObject P4Spawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (loadCharacters == true)
        {
            P1Spawn = GameObject.Find("HomeBase (1)");
            P2Spawn = GameObject.Find("HomeBase (2)");
            P3Spawn = GameObject.Find("HomeBase (3)");
            P4Spawn = GameObject.Find("HomeBase (4)");
            for (int i = 0; i < noOfPlayers; i++)
            {
                switch (i)
                {
                    case 0:
                        Instantiate(convertCharacter(P1Char), P1Spawn.transform.position, Quaternion.identity);
                        Camera P1cam = convertCharacter(P1Char).GetComponentInChildren<Camera>();
                        P1cam.rect = new Rect(0f, 0f, 0.5f, 1f);
                        break;
                    case 1:
                        Instantiate(convertCharacter(P2Char), P2Spawn.transform.position, Quaternion.identity);
                        Camera P2cam = convertCharacter(P2Char).GetComponentInChildren<Camera>();
                        P2cam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
                        break;
                    case 2:
                        Instantiate(convertCharacter(P3Char), P3Spawn.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(convertCharacter(P4Char), P4Spawn.transform.position, Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
            loadCharacters = false;
        }
    }

    GameObject convertCharacter(int charIndex)
    {
        switch (charIndex)
        {
            case 0:
                return Lizard;
                break;
            case 1:
                return Bear;
                break;
            case 2:
                return Rabbit;
                break;
            case 3:
                return Tiger;
                break;
            case 4:
                return Fox;
                break;
            default:
                return null;
                break;
        }
    }
}
