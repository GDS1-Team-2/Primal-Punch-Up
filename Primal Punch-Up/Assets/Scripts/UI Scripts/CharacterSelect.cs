using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterSelect : MonoBehaviour
{
    public int playerNo = 0;

    private int index = 0;
    public int lockIndex = 0;
    private Vector3[] positions;
    bool leftStickUse = false;
    float increaseHeightPerPlayer = 0.0f;
    public bool lockedIn = false;

    public Image imgCol;

    public RectTransform rectTrans;

    public Gamepad Player1 = null;
    public Gamepad Player2 = null;
    public Gamepad Player3 = null;
    public Gamepad Player4 = null;
    public bool player1Activated = false;
    public bool player2Activated = false;
    public bool player3Activated = false;
    public bool player4Activated = false;

    public AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        imgCol.enabled = false;
        //print(playerNo);
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        clip = audioSource.clip;

        switch (playerNo)
        {
            case 1:
                imgCol.color = Color.blue;
                Player1 = Gamepad.current;
                break;
            case 2:
                imgCol.color = Color.red;
                Player2 = Gamepad.current;
                increaseHeightPerPlayer = 50.0f;
                break;
            case 3:
                imgCol.color = Color.green;
                Player3 = Gamepad.current;
                //print(Player3.name);
                increaseHeightPerPlayer = 100.0f;
                break;
            case 4:
                imgCol.color = Color.yellow;
                Player4 = Gamepad.current;
                //print(Player4.name);
                increaseHeightPerPlayer =150.0f;
                break;
            default:
                break;
        }

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            transform.SetParent(canvas.transform, false);
        }
        else
        {
            Debug.LogError("Canvas not found");
        }

        rectTrans = gameObject.GetComponent<RectTransform>();

        positions = new Vector3[5];
        positions[0] = new Vector3(Screen.width * -0.35f, Screen.height * 0.2f + increaseHeightPerPlayer, 0);
        positions[1] = new Vector3(Screen.width * -0.15f, Screen.height * 0.2f + increaseHeightPerPlayer, 0);
        positions[2] = new Vector3(Screen.width * 0.1f, Screen.height * 0.2f + increaseHeightPerPlayer, 0);
        //positions[3] = new Vector3(350, 250 + increaseHeightPerPlayer, 0);
        positions[3] = new Vector3(Screen.width * 0.35f, Screen.height * 0.2f + increaseHeightPerPlayer, 0);
        gameObject.GetComponent<RectTransform>().anchoredPosition = positions[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNo == 1 && player1Activated)
        {
            imgCol.enabled = true;
            if (leftStickUse == false)
            {
                if (Player1.leftStick.right.isPressed && !lockedIn)
                {
                    MoveRight();
                }
                else if (Player1.leftStick.left.isPressed && !lockedIn)
                {
                    MoveLeft();
                }
                else if (Player1.buttonSouth.wasPressedThisFrame)
                {
                    LockInToggle();
                }
            }

            if (Player1.leftStick.x.ReadValue() == 0)
            {
                leftStickUse = false;
            }
        }
        else if (playerNo == 2 && player2Activated)
        {
            imgCol.enabled = true;
            if (leftStickUse == false)
            {
                if (Player2.leftStick.right.isPressed && !lockedIn)
                {
                    MoveRight();
                }
                else if (Player2.leftStick.left.isPressed && !lockedIn)
                {
                    MoveLeft();
                }
                else if (Player2.buttonSouth.wasPressedThisFrame)
                {
                    LockInToggle();
                }
            }

            if (Player2.leftStick.x.ReadValue() == 0)
            {
                leftStickUse = false;
            }
        } else if (playerNo == 3 && player3Activated)
        {
            imgCol.enabled = true;
            if (leftStickUse == false)
            {
                if (Player3.leftStick.right.isPressed && !lockedIn)
                {
                    MoveRight();
                }
                else if (Player3.leftStick.left.isPressed && !lockedIn)
                {
                    MoveLeft();
                } else if (Player3.buttonSouth.wasPressedThisFrame)
                {
                    LockInToggle();
                }
            }

            if (Player3.leftStick.x.ReadValue() == 0)
            {
                leftStickUse = false;
            }
        } else if (playerNo == 4 && player4Activated)
        {
            imgCol.enabled = true;
            if (leftStickUse == false)
            {
                if (Player4.leftStick.right.isPressed && !lockedIn)
                {
                    MoveRight();
                }
                else if (Player4.leftStick.left.isPressed && !lockedIn)
                {
                    MoveLeft();
                } else if (Player4.buttonSouth.wasPressedThisFrame)
                {
                    LockInToggle();
                }
            }

            if (Player4.leftStick.x.ReadValue() == 0)
            {
                leftStickUse = false;
            }
        }
    }

    void MoveRight()
    {
        if (index < 3)
        {
            if (rectTrans != null)
            {
                index += 1;
                rectTrans.anchoredPosition = positions[index];
                leftStickUse = true;
            }
            else
            {
                Debug.LogError("RectTransform is null.");
            }
        }
    }

    void MoveLeft()
    {
        if (index > 0)
        {
            if (rectTrans != null)
            {
                index -= 1;
                rectTrans.anchoredPosition = positions[index];
                leftStickUse = true;
            }
            else
            {
                Debug.LogError("RectTransform is null.");
            }
        }
    }
    
    void LockInToggle()
    {
        lockIndex = index;

        if (lockedIn)
        {
            lockedIn = false;
            rectTrans.sizeDelta = new Vector2(100, 100);
        } else
        {
            lockedIn = true;
            audioSource.PlayOneShot(clip);
            rectTrans.sizeDelta = new Vector2(50, 50);
        }
    }

    public void AssignController(Gamepad gamepad, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                Player1 = gamepad;
                player1Activated = true;
                break;
            case 2:
                Player2 = gamepad;
                player2Activated = true;
                break;
            case 3:
                Player3 = gamepad;
                player3Activated = true;
                break;
            case 4:
                Player4 = gamepad;
                player4Activated = true;
                break;
            default:
                break;
        }
    }
}
