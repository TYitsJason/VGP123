using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    public static GameManager Instance => instance;
    private int _lives = 3;
    public int maxLives = 16;
    public int lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            OnLivesValueChanged?.Invoke(_lives);
        }
    }

    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score -= value;
        }
    }

    public PlayerController playerPrefab;
    public PlayerController playerInstance;
    public UnityEvent<int> OnLivesValueChanged;

    [HideInInspector] public Transform spawnPoint;

    // Start is called before the first frame update
    public void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {

        if (lives <= 0 && SceneManager.GetActiveScene().name != "GameOver")
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
        spawnPoint = spawnLocation;
    }
    public void Respawn()
    {
        playerInstance.transform.position = spawnPoint.position;
    }
    public void UpdateSpawnPoint(Transform updatedPoint)
    {
        spawnPoint = updatedPoint;
    }
}
