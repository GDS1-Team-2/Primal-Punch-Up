using UnityEngine;

public class PvPCharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;

    void Start()
    {
        int player1CharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndexPlayer1", 0);
        int player2CharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndexPlayer2", 0);

        Instantiate(characterPrefabs[player1CharacterIndex], spawnPointPlayer1.position, Quaternion.identity);
        Instantiate(characterPrefabs[player2CharacterIndex], spawnPointPlayer2.position, Quaternion.identity);
    }
}


