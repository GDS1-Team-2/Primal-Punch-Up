using UnityEngine;

public class LoadSelectedCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);
        GameObject selectedCharacterPrefab = characterPrefabs[selectedCharacterIndex];
        Instantiate(selectedCharacterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
