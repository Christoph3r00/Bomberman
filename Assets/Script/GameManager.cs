using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] players;
    private int[] playerScores;

    // Reference to a UI Text component to display the scores
    public UnityEngine.UI.Text[] scoreTexts;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize player scores
            playerScores = new int[players.Length];
        }
    }

    public void CheckWinState()
    {
        int aliveCount = 0;
        int alivePlayerIndex = -1;

        // Check for the last surviving player
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf)
            {
                aliveCount++;
                alivePlayerIndex = i;
            }
        }

        if (aliveCount <= 1)
        {
            if (aliveCount == 1)
            {
                // Increase the score of the surviving player
                playerScores[alivePlayerIndex] += 10; // Adjust the score as needed
            }

            // Update the UI scores
            UpdateScoreTexts();

            // Start a new round after a delay
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update the UI scores
    private void UpdateScoreTexts()
    {
        for (int i = 0; i < scoreTexts.Length && i < playerScores.Length; i++)
        {
            scoreTexts[i].text = "Player " + (i + 1) + ": " + playerScores[i];
        }
    }
}

