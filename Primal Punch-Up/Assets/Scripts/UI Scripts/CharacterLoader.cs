using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public Gamepad P3Controller = null;
    public Gamepad P4Controller = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (loadCharacters == true && SceneManager.GetActiveScene().name != "MapSelect")
        {
            noOfPlayers = PlayerPrefs.GetInt("noOfPlayers");
            P1Spawn = GameObject.Find("HomeBase (1)");
            P2Spawn = GameObject.Find("HomeBase (2)");
            P3Spawn = GameObject.Find("HomeBase (3)");
            P4Spawn = GameObject.Find("HomeBase (4)");

            for (int i = 0; i < noOfPlayers; i++)
            {
                switch (i)
                {
                    case 0:
                        GameObject player1 = Instantiate(convertCharacter(P1Char), P1Spawn.transform.position, Quaternion.identity);
                        PlayerBase player1Script = player1.GetComponent<PlayerBase>();
                        player1Script.playerNo = 1;
                        Camera P1cam = player1.GetComponentInChildren<Camera>();
                        if (noOfPlayers == 1)
                        {
                            P1cam.rect = new Rect(0f, 0f, 1f, 1f);
                        } else if (noOfPlayers == 2)
                        {
                            P1cam.rect = new Rect(0f, 0f, 0.5f, 1f);
                        } else if (noOfPlayers >= 3)
                        {
                            P1cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                        }
                        
                        break;
                    case 1:
                        GameObject player2 = Instantiate(convertCharacter(P2Char), P2Spawn.transform.position, Quaternion.identity);
                        PlayerBase player2Script = player2.GetComponent<PlayerBase>();
                        player2Script.playerNo = 2;
                        Camera P2cam = player2.GetComponentInChildren<Camera>();
                        if (noOfPlayers == 2)
                        {
                            P2cam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
                        }
                        else if (noOfPlayers >= 3)
                        {
                            P2cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                        }
                        break;
                    case 2:
                        GameObject player3 = Instantiate(convertCharacter(P3Char), P3Spawn.transform.position, Quaternion.identity);
                        PlayerBase player3Script = player3.GetComponent<PlayerBase>();
                        player3Script.playerNo = 3;
                        player3Script.P3Controller = P3Controller;
                        Camera P3cam = player3.GetComponentInChildren<Camera>();
                        P3cam.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                        break;
                    case 3:
                        GameObject player4 = Instantiate(convertCharacter(P4Char), P4Spawn.transform.position, Quaternion.identity);
                        PlayerBase player4Script = player4.GetComponent<PlayerBase>();
                        player4Script.playerNo = 4;
                        player4Script.P4Controller = P4Controller;
                        Camera P4cam = player4.GetComponentInChildren<Camera>();
                        P4cam.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
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
                return Fox;
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
