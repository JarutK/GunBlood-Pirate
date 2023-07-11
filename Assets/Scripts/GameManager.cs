using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    Idle,
    Start,
    Play,
    End
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private TextMeshProUGUI announceText;
    [SerializeField] private float gameStartCountDown;
    private float gameStartTimer;

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Time.timeScale = 1;
        gameState = GameState.Idle;
        gameStartTimer = gameStartCountDown;
    }

    private void Update()
    {
        GetInput();
        CheckState();
    }

    private void CheckState()
    {
        switch (gameState)
        {
            case GameState.Idle:
                break;

            case GameState.Start:
                CountdownToPlay();
                break;

            case GameState.Play:
                break;

            case GameState.End:
                GameOver();
                break;
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.Idle)
            {
                announceText.gameObject.SetActive(true);
                gameState = GameState.Start;
            }
            else if (gameState == GameState.End)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void CountdownToPlay()
    {
        gameStartTimer -= Time.deltaTime;
        announceText.text = gameStartTimer.ToString("0");

        if (Mathf.RoundToInt(gameStartTimer) <= 0)
        {
            announceText.gameObject.SetActive(false);
            gameState = GameState.Play;
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        announceText.gameObject.SetActive(true);

        if (!player1 && !player2)
            announceText.text = "DRAW";
        else if (!player2)
            announceText.text = "P1 WIN!";
        else if (!player1)
            announceText.text = "P2 WIN!";

    }

}
