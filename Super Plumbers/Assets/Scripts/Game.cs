using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Game : MonoBehaviour
{
    public int lives, score, highscore;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private Spawner[] spawners;
    private int level;
    [SerializeField]
    private TextMeshProUGUI scoreText, livesText, bestText;

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        Load();
        bestText.text = "Best: " + highscore;
        UpdateHUD();
    }

    public void LoseLife()
    {
        if(lives > 0)
        {
            //respawn player
            StartCoroutine(Respawn());
            //deduct life
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if(score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        StartNewGame();
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        lives--;
        Instantiate(player.gameObject, playerSpawnPoint.position, Quaternion.identity);
        // update hud show new life count
    }

    public void AddPoints(int amount)
    {
        score += amount;
        //update hud
        //is level complete?
    }

    void CheckLevelCompletion()
    {
        if(!FindObjectOfType<Enemy>())
        {
            foreach(Spawner spawner in spawners)
            {
                if (!spawner.completed)
                {
                    return;
                }
            }
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        level++;
        Save();
        //load new level
        if (SceneManager.GetSceneByBuildIndex(level) != null)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.Log("Game Won!");
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("Level", level);
    }

    private void Load()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        lives = PlayerPrefs.GetInt("Lives", 3);
        level = PlayerPrefs.GetInt("Level", 0);
    }

    void StartNewGame()
    {
        level = 0;
        SceneManager.LoadScene(level);
        PlayerPrefs.DeleteKey("Score"); 
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Level");
    }

    void UpdateHUD()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
    }
}
