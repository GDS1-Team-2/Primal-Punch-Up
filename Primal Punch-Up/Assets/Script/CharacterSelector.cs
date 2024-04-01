using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    private int selectedIndex = 0;

    void Start()
    {
        UpdateCharacterDisplay();
    }

    public void NextCharacter()
    {
        selectedIndex++;
        if (selectedIndex >= characters.Length) selectedIndex = 0;
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        selectedIndex--;
        if (selectedIndex < 0) selectedIndex = characters.Length - 1;
        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[selectedIndex].SetActive(true);
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);
    }

}