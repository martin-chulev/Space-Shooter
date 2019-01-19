using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject player1;
    public GameObject player2;

    public Text scoreTextP1;
    public Text scoreTextP2;
    public Text restartText;
    public Text gameOverText;

    private int scoreP1;
    private int scoreP2;
    private bool joinedP1;
    private bool joinedP2;
    private bool gameOver;

    void Start()
    {
        scoreP1 = 0;
        scoreP2 = 0;

        restartText.text = string.Empty;

        joinedP1 = false;
        joinedP2 = false;
        gameOver = false;
        gameOverText.text = "Press Fire to join";

        UpdateScore(1);
        UpdateScore(2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2) == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (joinedP1 == false && gameOver == false)
        {
            if (Input.GetButton("Fire1 P1") == true)
            {
                player1.SetActive(true);
                joinedP1 = true;
                gameOverText.text = string.Empty;
                if (joinedP2 == false) StartCoroutine(SpawnWaves());
            }
        }
        if (joinedP2 == false && gameOver == false)
        {
            if (Input.GetButton("Fire1 P2") == true)
            {
                player2.SetActive(true);
                joinedP2 = true;
                gameOverText.text = string.Empty;
                if (joinedP1 == false) StartCoroutine(SpawnWaves());
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver == true)
            {
                restartText.text = "Press 'F2' to Restart";
                break;
            }
        }
    }

    public void AddScore(int points, int player)
    {
        if (player == 1) scoreP1 += points;
        else if (player == 2) scoreP2 += points;
        UpdateScore(player);
    }

    void UpdateScore(int player)
    {
        if (player == 1) scoreTextP1.text = "Player1 Score:" + System.Environment.NewLine + scoreP1;
        else if (player == 2) scoreTextP2.text = "Player2 Score:" + System.Environment.NewLine + scoreP2;
    }

    public void GameOver(int player)
    {
        if (player == 1) joinedP1 = false;
        if (player == 2) joinedP2 = false;

        if (joinedP1 == false && joinedP2 == false)
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }
    }
}
