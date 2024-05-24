using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundsScript : MonoBehaviour
{
    public string Map1SceneName;
    public string Map2SceneName;
    public string Map3SceneName;
    public string CompleteSceneName;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public GameObject CharacterLoader;
    public GameObject UICanvas2Players;
    public GameObject UICanvas3Players;
    public GameObject UICanvas4Players;
    public GameObject EndAnimCanvas;

    public List<GameObject> players;

    public bool newRound = false;

    public ScoreboardScript ScoreboardScript;

    bool twoWinners = false;
    bool threeWinners = false;
    bool fourWinners = false;



    // Start is called before the first frame update
    void Start()
    {
        ScoreboardScript = this.gameObject.GetComponent<ScoreboardScript>();
        CharacterLoader = GameObject.Find("CharacterLoader");
        CharacterLoader.GetComponent<CharacterLoader>().loadCharacters = true;
        int noOfPlayers = CharacterLoader.GetComponent<CharacterLoader>().noOfPlayers;

        UICanvas2Players = GameObject.Find("UICanvas2Players");
        UICanvas3Players = GameObject.Find("UICanvas3Players");
        UICanvas4Players = GameObject.Find("UICanvas4Players");
        EndAnimCanvas = GameObject.Find("EndAnimMenu");
        EndAnimCanvas.SetActive(false);


        switch (noOfPlayers)
        {
            case 2:
                UICanvas3Players.SetActive(false);
                UICanvas4Players.SetActive(false);
                UICanvas2Players.SetActive(true);
                break;
            case 3:
                UICanvas2Players.SetActive(false);
                UICanvas4Players.SetActive(false);
                UICanvas3Players.SetActive(true);
                break;
            case 4:
                UICanvas2Players.SetActive(false);
                UICanvas3Players.SetActive(false);
                UICanvas4Players.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (newRound)
        {
            PlayerPrefs.SetInt("ScoreKey1", 0);
            PlayerPrefs.SetInt("ScoreKey2", 0);
            PlayerPrefs.SetInt("ScoreKey3", 0);
            PlayerPrefs.SetInt("ScoreKey4", 0);
        }
    }

    public void SetPlayer1(GameObject player)
    {
        Player1 = player;
        players.Add(Player1);
    }

    public void SetPlayer2(GameObject player)
    {
        Player2 = player;
        players.Add(Player2);
    }

    public void SetPlayer3(GameObject player)
    {
        Player3 = player;
        players.Add(Player3);
    }

    public void SetPlayer4(GameObject player)
    {
        Player4 = player;
        players.Add(Player4);
    }

    public void EndRound()
    {
        StartCoroutine(EndRoundCR());
    }

    IEnumerator EndRoundCR()
    {
        EndAnimCanvas.SetActive(true);

        EndAnimCanvas.GetComponent<EndAnims>().PlayAnim2();
        yield return new WaitForSeconds(0.2f);
        EndAnimCanvas.GetComponent<EndAnims>().PlayAnim3();
        yield return new WaitForSeconds(0.5f);
        EndAnimCanvas.GetComponent<EndAnims>().PlayAnim1();
        


        /*int[] scores = {PlayerPrefs.GetInt("ScoreKey1"), PlayerPrefs.GetInt("ScoreKey2"),
                        PlayerPrefs.GetInt("ScoreKey3"), PlayerPrefs.GetInt("ScoreKey4")};

        int maxIndex = -1;
        int maxValue = 0;
        for (int i = 0; i < 4; i++)
        {
            if (scores[i] > maxValue)
            {
                maxValue = scores[i];
                maxIndex = i;
            }
        }*/

        List<int> scores = new List<int>();
        int[] playerindex = { 1, 2, 3, 4 };

        scores.Add(PlayerPrefs.GetInt("ScoreKey1"));
        scores.Add(PlayerPrefs.GetInt("ScoreKey2"));
        scores.Add(PlayerPrefs.GetInt("ScoreKey3"));
        scores.Add(PlayerPrefs.GetInt("ScoreKey4"));

        for (int i = 1; i < 4; ++i)
        {
            int key = scores[i];
            int keyIndex = playerindex[i];
            int j = i - 1;

            while (j >= 0 && scores[j] < key)
            {
                scores[j + 1] = scores[j];
                playerindex[j + 1] = playerindex[j];
                j = j - 1;
            }
            scores[j + 1] = key;
            playerindex[j + 1] = keyIndex;
        }

        if (scores[0] == scores[1])
        {
            //two winners
            string winner = "Player" + playerindex[0] + "Wins";
            PlayerPrefs.SetInt(winner, (PlayerPrefs.GetInt(winner) + 1));
            string winner2 = "Player" + playerindex[1] + "Wins";
            PlayerPrefs.SetInt(winner2, (PlayerPrefs.GetInt(winner2) + 1));
            PlayerPrefs.Save();
        }
        else if (scores[0] == scores[1] && scores[0] == scores[2] && scores[1] == scores[2])
        {
            //three winners
            string winner = "Player" + playerindex[0] + "Wins";
            PlayerPrefs.SetInt(winner, (PlayerPrefs.GetInt(winner) + 1));
            string winner2 = "Player" + playerindex[1] + "Wins";
            PlayerPrefs.SetInt(winner2, (PlayerPrefs.GetInt(winner2) + 1));
            string winner3 = "Player" + playerindex[2] + "Wins";
            PlayerPrefs.SetInt(winner3, (PlayerPrefs.GetInt(winner3) + 1));
            PlayerPrefs.Save();
        }
        else if (scores[0] == scores[1] && scores[0] == scores[2] && scores[1] == scores[2] && scores[0] == scores[3])
        {
            //four winners
            string winner = "Player" + playerindex[0] + "Wins";
            PlayerPrefs.SetInt(winner, (PlayerPrefs.GetInt(winner) + 1));
            string winner2 = "Player" + playerindex[1] + "Wins";
            PlayerPrefs.SetInt(winner2, (PlayerPrefs.GetInt(winner2) + 1));
            string winner3 = "Player" + playerindex[2] + "Wins";
            PlayerPrefs.SetInt(winner3, (PlayerPrefs.GetInt(winner3) + 1));
            string winner4 = "Player" + playerindex[3] + "Wins";
            PlayerPrefs.SetInt(winner4, (PlayerPrefs.GetInt(winner4) + 1));
            PlayerPrefs.Save();
        }
        else
        {
            //one winner
            string winner = "Player" + playerindex[0] + "Wins";
            PlayerPrefs.SetInt(winner, (PlayerPrefs.GetInt(winner) + 1));
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt("RoundNo", (PlayerPrefs.GetInt("RoundNo") + 1));
        PlayerPrefs.SetInt("ScoreKey1", 0);
        PlayerPrefs.SetInt("ScoreKey2", 0);
        PlayerPrefs.SetInt("ScoreKey3", 0);
        PlayerPrefs.SetInt("ScoreKey4", 0);

        if (PlayerPrefs.GetInt("RoundNo") > PlayerPrefs.GetInt("noOfRounds"))
        {
            List<int> wins = new List<int>();
            int[] pIndex = { 1, 2, 3, 4 };

            wins.Add(PlayerPrefs.GetInt("Player1Wins"));
            wins.Add(PlayerPrefs.GetInt("Player2Wins"));
            wins.Add(PlayerPrefs.GetInt("Player3Wins"));
            wins.Add(PlayerPrefs.GetInt("Player4Wins"));

            for (int i = 1; i < 4; ++i)
            {
                int key = wins[i];
                int keyIndex = pIndex[i];
                int j = i - 1;

                while (j >= 0 && wins[j] < key)
                {
                    wins[j + 1] = wins[j];
                    pIndex[j + 1] = pIndex[j];
                    j = j - 1;
                }
                wins[j + 1] = key;
                pIndex[j + 1] = keyIndex;
            }

            if (wins[0] == wins[1])
            {
                //Time.timeScale = 0;
                UICanvas2Players.SetActive(false);
                UICanvas3Players.SetActive(false);
                UICanvas4Players.SetActive(false);
                foreach (GameObject player in players)
                {
                    player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    player.GetComponent<Animator>().speed = 0;
                    player.GetComponent<Rigidbody>().isKinematic = true;
                    player.GetComponent<PlayerBase>().acceptInput = false;
                }
                yield return new WaitForSeconds(1f);
                ScoreboardScript.ExtraRound(wins[0]);
                ScoreboardScript.ScoreBoard();
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene(CompleteSceneName);
            }
        }
        else
        {
            //Time.timeScale = 0;
            UICanvas2Players.SetActive(false);
            UICanvas3Players.SetActive(false);
            UICanvas4Players.SetActive(false);
            foreach (GameObject player in players)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                player.GetComponent<Animator>().speed = 0;
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<PlayerBase>().acceptInput = false;
            }
            yield return new WaitForSeconds(1f);
            ScoreboardScript.ScoreBoard();
        }
    }
}
