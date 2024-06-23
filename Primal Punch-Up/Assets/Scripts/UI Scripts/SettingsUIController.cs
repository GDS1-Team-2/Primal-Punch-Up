using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsUIController : MonoBehaviour
{
    public GameObject backBtn;
    public GameObject twoPlayersBtn;
    public GameObject threePlayersBtn;
    public GameObject fourPlayersBtn;
    public GameObject twoRoundsBtn;
    public GameObject threeRoundsBtn;
    public GameObject fourRoundsBtn;
    public GameObject nextBtn;
    private int indicatorPos = 1;
    private bool cameFromOpSide = false;
    private List<GameObject> buttons = new List<GameObject>();
    bool leftStickUse = false;
    public GameObject UISFXobj;
    private UISFX uisfxScript;

    // Start is called before the first frame update
    void Start()
    {
        UISFXobj = GameObject.Find("UI SFX");
        uisfxScript = UISFXobj.GetComponent<UISFX>();
        buttons.Add(backBtn);
        buttons.Add(twoPlayersBtn);
        buttons.Add(threePlayersBtn);
        buttons.Add(fourPlayersBtn);
        buttons.Add(twoRoundsBtn);
        buttons.Add(threeRoundsBtn);
        buttons.Add(fourRoundsBtn);
        buttons.Add(nextBtn);
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.leftStick.down.wasPressedThisFrame)
            {
                if (cameFromOpSide)
                {
                    indicatorPos = 4;
                    cameFromOpSide = false;
                } else
                {
                    if (indicatorPos < 7 && indicatorPos != 3)
                    {
                        indicatorPos++;
                        cameFromOpSide = false;
                    }
                    else if (indicatorPos == 3)
                    {
                        indicatorPos = 7;
                        cameFromOpSide = true;
                    }
                }
            }
            else if (gamepad.leftStick.up.wasPressedThisFrame)
            {
                if (cameFromOpSide)
                {
                    indicatorPos = 3;
                    cameFromOpSide = false;
                } else
                {
                    if (indicatorPos > 0 && indicatorPos != 4)
                    {
                        indicatorPos--;
                        cameFromOpSide = false;
                    }
                    else if (indicatorPos == 4)
                    {
                        indicatorPos = 0;
                        cameFromOpSide = true;
                    }
                }
            }
            else if (gamepad.buttonSouth.isPressed)
            {
                uisfxScript.PlaySFX();
                Button selectedBtn = buttons[indicatorPos].GetComponent<Button>();
                selectedBtn.onClick.Invoke();
            } 
            else if (gamepad.leftStick.right.wasPressedThisFrame)
            {
                if (indicatorPos > 0 && indicatorPos < 4)
                {
                    indicatorPos += 3;
                    cameFromOpSide = false;
                }
            } 
            else if (gamepad.leftStick.left.wasPressedThisFrame)
            {
                if (indicatorPos > 3 && indicatorPos < 7)
                {
                    indicatorPos -= 3;
                    cameFromOpSide = false;
                }
            }
        }

        foreach (GameObject button in buttons)
        {
            Color baseColor;
            ColorUtility.TryParseHtmlString("#FFFFFF", out baseColor);
            Image btnImg = button.GetComponent<Image>();
            btnImg.color = baseColor;
        }

        Color newColor;
        ColorUtility.TryParseHtmlString("#80FF80", out newColor);
        Image selectedBtnImg = buttons[indicatorPos].GetComponent<Image>();
        selectedBtnImg.color = newColor;
    }
}
