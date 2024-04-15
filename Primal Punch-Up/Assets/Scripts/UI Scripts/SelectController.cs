using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{

    public int noOfPlayers = 1;
    public GameObject playerPrefab;
    public CharacterSelect characterScript;
    private List<GameObject> prefabs = new List<GameObject>();
    private bool player3Assigned = false;
    private bool player4Assigned = false;
    Gamepad player3Controller;
    bool allLockedIn = true;
    public GameObject letsGoBtn;
    public GameObject characterLoader;
    public CharacterLoader characterLoadScript;
    public string P1Char = "";
    public string P2Char = "";
    public string P3Char = "";
    public string P4Char = "";

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            print(Gamepad.all[i].name + ": " + i);
        }

        //Creates number of player selectors based on number of players playing.
        for (int i = 0; i < noOfPlayers; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            characterScript = newPlayer.GetComponent<CharacterSelect>();
            characterScript.playerNo = i + 1;
            prefabs.Add(newPlayer);
        }

        characterLoader = GameObject.Find("CharacterLoader");
        characterLoadScript = characterLoader.GetComponent<CharacterLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        //Players 3 and 4 get assigned their controller based on which controller pressed triangle first.
        if (noOfPlayers > 2)
        {
            if (!player3Assigned || !player4Assigned)
            {
                var gamepads = Gamepad.all;
                foreach (var gamepad in gamepads)
                {
                    if (!player3Assigned)
                    {
                        if (gamepad.buttonNorth.isPressed)
                        {
                            CharacterSelect characterScript = prefabs[2].GetComponent<CharacterSelect>();
                            characterScript.Player3 = gamepad;
                            characterScript.player3Activated = true;
                            player3Controller = gamepad;
                            characterLoadScript.P3Controller = gamepad;
                            player3Assigned = true;
                            print(gamepad.name + " is the controller for player 3");
                            break;
                        }
                    }
                    else if (!player4Assigned)
                    {
                        if (gamepad.buttonNorth.isPressed && gamepad != player3Controller)
                        {
                            CharacterSelect characterScript = prefabs[3].GetComponent<CharacterSelect>();
                            characterScript.player4Activated = true;
                            characterScript.Player4 = gamepad;
                            characterLoadScript.P4Controller = gamepad;
                            player4Assigned = true;
                            print(gamepad.name + " is the controller for player 4");
                            break;
                        }
                    }
                }
            }
        }

        //Once every player has locked in, the lets go! button is able to be pressed
        allLockedIn = true;
        foreach (GameObject prefab in prefabs)
        {
            CharacterSelect characterScript = prefab.GetComponent<CharacterSelect>();
            if (!characterScript.lockedIn)
            {
                allLockedIn = false;
            }
        }

        if (allLockedIn)
        {
            letsGoBtn.SetActive(true);
        } else
        {
            letsGoBtn.SetActive(false);
        }
    }

    public void LetsGoBtn()
    {
        CharacterLoader charLoadScript = characterLoader.GetComponent<CharacterLoader>();
        charLoadScript.noOfPlayers = noOfPlayers;
        charLoadScript.loadCharacters = true;
        foreach (GameObject prefab in prefabs)
        {
            CharacterSelect characterScript = prefab.GetComponent<CharacterSelect>();
            switch (characterScript.playerNo)
            {
                case 1:
                    charLoadScript.P1Char = characterScript.lockIndex;
                    break;
                case 2:
                    charLoadScript.P2Char = characterScript.lockIndex;
                    break;
                case 3:
                    charLoadScript.P3Char = characterScript.lockIndex;
                    break;
                case 4:
                    charLoadScript.P4Char = characterScript.lockIndex;
                    break;
                default:
                    break;
            }
        }
        DontDestroyOnLoad(characterLoader);
        SceneManager.LoadScene("MapTesting");
    }
}
