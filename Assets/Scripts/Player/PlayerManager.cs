using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public GameObject newRecordPanel;

    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI newRecordText;

    public static bool isGamePaused;
    public bool flagCheckNewScore = false;
    public GameObject[] characterPrefabs;


    private void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedCharacter");
        GameObject go = Instantiate(characterPrefabs[index], new Vector3(0f,1.4f,0), Quaternion.identity);

    }

    void Start()
    {
        score = 0;
        Time.timeScale = 1;
        gameOver = isGameStarted = isGamePaused = false;

    }

    void Update()
    {

        coinText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
        scoreText.text = score.ToString();


        if (gameOver)
        {
            Time.timeScale = 0;
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                newRecordPanel.SetActive(true);
                newRecordText.text = score.ToString();
                PlayerPrefs.SetInt("HighScore", score);
                flagCheckNewScore= true;
            }
            else
            {

            }
            if (flagCheckNewScore == false)
            {
                gameOverPanel.SetActive(true);
                Destroy(gameObject);
            }
        }


        if (SwipeManager.tap && !isGameStarted) //Tap to play
        {
            isGameStarted = true;
            Destroy(startingText);
        
        }
    }

}
