using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{

    public GameObject firstBtn;
    public GameObject secondBtn;
    public GameObject thirdBtn;
    private int indicatorPos = 0;
    private List<GameObject> buttons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        buttons.Add(firstBtn);
        if (secondBtn != null)
        {
            buttons.Add(secondBtn);
        } else if (thirdBtn != null)
        {
            buttons.Add(thirdBtn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.leftStick.down.wasPressedThisFrame)
            {
                if (indicatorPos < buttons.Count - 1)
                {
                    indicatorPos++;
                }
            } else if (gamepad.leftStick.up.wasPressedThisFrame)
            {
                if (indicatorPos > 0)
                {
                    indicatorPos--;
                }
            } else  if (gamepad.buttonEast.isPressed)
            {
                Button selectedBtn = buttons[indicatorPos].GetComponent<Button>();
                selectedBtn.onClick.Invoke();
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
