using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] character1Prefabs;
    private int selectedCharacter1Index = 0;
    public string playerPrefsKey1 = "SelectedCharacterIndexPlayer1";

    public GameObject[] character2Prefabs;
    private int selectedCharacter2Index = 0;
    public string playerPrefsKey2 = "SelectedCharacterIndexPlayer2";

    public void NextCharacter1()
    {
        selectedCharacter1Index++;
        if (selectedCharacter1Index >= character1Prefabs.Length) selectedCharacter1Index = 0;
    }

    public void PreviousCharacter1()
    {
        selectedCharacter1Index--;
        if (selectedCharacter1Index < 0) selectedCharacter1Index = character1Prefabs.Length - 1;
    }

    public void NextCharacter2()
    {
        selectedCharacter2Index++;
        if (selectedCharacter2Index >= character2Prefabs.Length) selectedCharacter2Index = 0;
    }

    public void PreviousCharacter2()
    {
        selectedCharacter2Index--;
        if (selectedCharacter2Index < 0) selectedCharacter2Index = character2Prefabs.Length - 1;
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt(playerPrefsKey1, selectedCharacter1Index);
        PlayerPrefs.SetInt(playerPrefsKey2, selectedCharacter2Index);
        PlayerPrefs.Save();
        SceneManager.LoadScene("PvPScene");
    }

}


