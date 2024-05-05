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

    public Gamepad Player3 = null;
    public Gamepad Player4 = null;
    public bool player3Activated = false;
    public bool player4Activated = false;

    KeyCode? moveRight = null;
    KeyCode? moveLeft = null;
    KeyCode? select = null;

    public AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        //print(playerNo);
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        clip = audioSource.clip;

        switch (playerNo)
        {
            case 1:
                moveRight = KeyCode.D;
                moveLeft = KeyCode.A;
                select = KeyCode.C;
                break;
            case 2:
                imgCol.color = Color.red;
                moveRight = KeyCode.RightArrow;
                moveLeft = KeyCode.LeftArrow;
                select = KeyCode.O;
                increaseHeightPerPlayer = 75.0f;
                break;
            case 3:
                imgCol.color = Color.yellow;
                Player3 = Gamepad.current;
                //print(Player3.name);
                increaseHeightPerPlayer = 150.0f;
                break;
            case 4:
                imgCol.color = Color.green;
                Player4 = Gamepad.current;
                //print(Player4.name);
                increaseHeightPerPlayer = 225.0f;
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
        positions[0] = new Vector3(-700, 200 + increaseHeightPerPlayer, 0);
        positions[1] = new Vector3(-250, 200 + increaseHeightPerPlayer, 0);
        positions[2] = new Vector3(200, 200 + increaseHeightPerPlayer, 0);
        //positions[3] = new Vector3(350, 250 + increaseHeightPerPlayer, 0);
        positions[3] = new Vector3(600, 200 + increaseHeightPerPlayer, 0);
        gameObject.GetComponent<RectTransform>().anchoredPosition = positions[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNo == 1 || playerNo == 2)
        {
            if (moveRight.HasValue && Input.GetKeyDown(moveRight.Value) && !lockedIn)
            {
                MoveRight();
            } else if (moveLeft.HasValue && Input.GetKeyDown(moveLeft.Value) && !lockedIn)
            {
                MoveLeft();
            } else if (select.HasValue && Input.GetKeyDown(select.Value))
            {
                LockInToggle();
            }
        } else if (playerNo == 3 && player3Activated)
        {
            if (leftStickUse == false)
            {
                if (Player3.leftStick.right.isPressed && !lockedIn)
                {
                    MoveRight();
                }
                else if (Player3.leftStick.left.isPressed && !lockedIn)
                {
                    MoveLeft();
                } else if (Player3.buttonEast.wasPressedThisFrame)
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
            if (leftStickUse == false)
            {
                if (Player4.leftStick.right.isPressed && !lockedIn)
                {
                    MoveRight();
                }
                else if (Player4.leftStick.left.isPressed && !lockedIn)
                {
                    MoveLeft();
                } else if (Player4.buttonEast.wasPressedThisFrame)
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
}
