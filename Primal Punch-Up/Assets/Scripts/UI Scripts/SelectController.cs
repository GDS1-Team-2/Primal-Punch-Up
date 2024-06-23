using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{

    public int noOfPlayers = 1;
    public GameObject playerPrefab;
    public CharacterSelect characterScript;
    private List<GameObject> prefabs = new List<GameObject>();
    private bool player1Assigned = false;
    private bool player2Assigned = false;
    private bool player3Assigned = false;
    private bool player4Assigned = false;
    bool allLockedIn = true;
    public GameObject letsGoBtn;
    public GameObject infoTxt;
    public GameObject characterLoader;
    public CharacterLoader characterLoadScript;
    public string P1Char = "";
    public string P2Char = "";
    public string P3Char = "";
    public string P4Char = "";
    private Gamepad player1Controller;
    private Gamepad player2Controller;
    private Gamepad player3Controller;
    private bool startGame = false;
    private bool startBack = false;
    private float startGameTimer = 0.0f;
    private float startBackTimer = 0.0f;
    public GameObject backBtn;

    public Slider backSlider;
    public Slider nextSlider;

    // Start is called before the first frame update
    void Start()
    {
        noOfPlayers = PlayerPrefs.GetInt("noOfPlayers");

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
        if (startGame)
        {
            startGameTimer += Time.deltaTime;
            
        } else
        {
            startGameTimer = 0.0f;
        }

        nextSlider.value = startGameTimer;

        if (startBack)
        {
            startBackTimer += Time.deltaTime;
        } else
        {
            startBackTimer = 0.0f;
        }

        backSlider.value = startBackTimer;

        if (startBackTimer > 1.0f)
        {
            Button backButton = backBtn.GetComponent<Button>();
            backButton.onClick.Invoke();
        } else if (startGameTimer > 1.0f)
        {
            Button letsGoButton = letsGoBtn.GetComponent<Button>();
            letsGoButton.onClick.Invoke();
        }

        //Players 2 - 4 get assigned their controller based on which controller pressed triangle first.
        if (noOfPlayers >= 2)
        {
            if (!player1Assigned || !player2Assigned || !player3Assigned || !player4Assigned)
            {
                var gamepads = Gamepad.all;

                foreach (var gamepad in gamepads)
                {
                    if (allLockedIn && gamepad.buttonNorth.isPressed)
                    {
                        startGame = true;
                    } else if (gamepad.buttonEast.isPressed)
                    {
                        startBack = true;
                    } else if (gamepad.buttonNorth.wasReleasedThisFrame)
                    {
                        startGame = false;
                    } else if (gamepad.buttonEast.wasReleasedThisFrame)
                    {
                        startBack = false;
                    }
                    if (!player1Assigned)
                    {
                        if (gamepad.buttonNorth.isPressed)
                        {
                            CharacterSelect characterScript = prefabs[0].GetComponent<CharacterSelect>();
                            characterScript.Player1 = gamepad;
                            characterScript.player1Activated = true;
                            characterLoadScript.P1Controller = gamepad;
                            player1Controller = gamepad;
                            player1Assigned = true;
                            print(gamepad.name + " is the controller for player 1");
                            break;
                        }
                    }
                    else if (!player2Assigned)
                    {
                        if (gamepad.buttonNorth.isPressed && gamepad != player1Controller)
                        {
                            CharacterSelect characterScript = prefabs[1].GetComponent<CharacterSelect>();
                            characterScript.Player2 = gamepad;
                            characterScript.player2Activated = true;
                            characterLoadScript.P2Controller = gamepad;
                            player2Controller = gamepad;
                            player2Assigned = true;
                            print(gamepad.name + " is the controller for player 2");
                            break;
                        }
                    }
                    else if (!player3Assigned)
                    {
                        if (gamepad.buttonNorth.isPressed && gamepad != player1Controller && gamepad != player2Controller)
                        {
                            CharacterSelect characterScript = prefabs[2].GetComponent<CharacterSelect>();
                            characterScript.Player3 = gamepad;
                            characterScript.player3Activated = true;
                            characterLoadScript.P3Controller = gamepad;
                            player3Controller = gamepad;
                            player3Assigned = true;
                            print(gamepad.name + " is the controller for player 3");
                            break;
                        }
                    }
                    else if (!player4Assigned)
                    {
                        if (gamepad.buttonNorth.isPressed && gamepad != player1Controller && gamepad != player2Controller && gamepad != player3Controller)
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
            infoTxt.SetActive(false);
        } else
        {
            letsGoBtn.SetActive(false);
            infoTxt.SetActive(true);
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
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                SceneManager.LoadScene("Map 1");
                break;
            case 2:
                SceneManager.LoadScene("Map 2");
                break;
            case 3:
                SceneManager.LoadScene("Map 3");
                break;
        }
        
    }
}
