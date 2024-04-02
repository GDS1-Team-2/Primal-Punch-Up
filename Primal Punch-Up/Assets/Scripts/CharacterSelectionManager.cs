using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private int selectedCharacterIndex = 0; 
    public string playerPrefsKey = "SelectedCharacterIndexPlayer1";

    public void NextCharacter()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= characterPrefabs.Length) selectedCharacterIndex = 0;
 
    }

    public void PreviousCharacter()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0) selectedCharacterIndex = characterPrefabs.Length - 1;
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt(playerPrefsKey, selectedCharacterIndex);
        SceneManager.LoadScene("PvPScene");
    }
}
