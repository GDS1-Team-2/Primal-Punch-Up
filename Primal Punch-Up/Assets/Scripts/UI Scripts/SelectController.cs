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
    public GameObject letsGoAudio;
    public AudioSource letsGoSFX;
    public Slider backSlider;
    public Slider nextSlider;


    // Start is called before the first frame update
    void Start()
    {
        letsGoSFX = letsGoAudio.GetComponent<AudioSource>();

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
        }
        else
        {
            startGameTimer = 0.0f;
        }

        nextSlider.value = startGameTimer;

        if (startBack)
        {
            startBackTimer += Time.deltaTime;
        }
        else
        {
            startBackTimer = 0.0f;
        }

        backSlider.value = startBackTimer;

        if (startBackTimer > 1.0f)
        {
            Button backButton = backBtn.GetComponent<Button>();
            backButton.onClick.Invoke();
        }
        else if (startGameTimer > 1.0f)
        {
            Button letsGoButton = letsGoBtn.GetComponent<Button>();
            letsGoButton.onClick.Invoke();
        }

        if (noOfPlayers >= 2)
        {
            var gamepads = Gamepad.all;

            foreach (var gamepad in gamepads)
            {
                if (allLockedIn && gamepad.buttonNorth.wasPressedThisFrame)
                {
                    startGame = true;
                    letsGoSFX.Play();
                }
                else if (gamepad.buttonEast.isPressed)
                {
                    startBack = true;
                }
                else if (gamepad.buttonNorth.wasReleasedThisFrame)
                {
                    startGame = false;
                    letsGoSFX.Stop();
                }
                else if (gamepad.buttonEast.wasReleasedThisFrame)
                {
                    startBack = false;
                }

                if (!player1Assigned && gamepad.buttonNorth.isPressed)
                {
                    AssignPlayer(gamepad, 0);
                    player1Assigned = true;
                    player1Controller = gamepad;
                }
                else if (!player2Assigned && gamepad.buttonNorth.isPressed && gamepad != player1Controller)
                {
                    AssignPlayer(gamepad, 1);
                    player2Assigned = true;
                    player2Controller = gamepad;
                }
                else if (!player3Assigned && gamepad.buttonNorth.isPressed && gamepad != player1Controller && gamepad != player2Controller)
                {
                    AssignPlayer(gamepad, 2);
                    player3Assigned = true;
                    player3Controller = gamepad;
                }
                else if (!player4Assigned && gamepad.buttonNorth.isPressed && gamepad != player1Controller && gamepad != player2Controller && gamepad != player3Controller)
                {
                    AssignPlayer(gamepad, 3);
                    player4Assigned = true;
                }
            }
        }

        allLockedIn = true;
        foreach (GameObject prefab in prefabs)
        {
            CharacterSelect characterScript = prefab.GetComponent<CharacterSelect>();
            if (!characterScript.lockedIn)
            {
                allLockedIn = false;
                break;
            }
        }

        letsGoBtn.SetActive(allLockedIn);
        infoTxt.SetActive(!allLockedIn);
    }

    private void AssignPlayer(Gamepad gamepad, int index)
    {
        CharacterSelect characterScript = prefabs[index].GetComponent<CharacterSelect>();
        characterScript.AssignController(gamepad, index + 1);
        characterLoadScript.AssignController(gamepad, index + 1);
        print(gamepad.name + " is the controller for player " + (index + 1));
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
