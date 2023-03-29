using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    public static GameSession instance;
    [SerializeField] float loadDelay = 2f;
    int currentSceneIndex;
    public Text scoreText;
    [SerializeField]int score=0;
    [SerializeField]Image[] heart;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        for(int i=0;i<heart.Length;i++)
        {
            if(i<playerLives)
            {
                heart[i].enabled=true;
            }
            else
            {
                heart[i].enabled=false;
            }
        }
    }
 
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            StartCoroutine(LoadLevel());

        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        FindObjectOfType<Scene_persist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject); 
    }
    void TakeLife()
    {
        playerLives=playerLives-1;
        //int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void Start() 
    {    
        if(instance == null)
        {
            instance=this; 
        }
        scoreText.text=score.ToString();
    }
    public void AddToScore(int pointsToAdd)
    {
        score+=pointsToAdd;
        scoreText.text=score.ToString();
    }
    public IEnumerator LoadLevel()
    {
        playerLives=playerLives-1;
        yield return new WaitForSecondsRealtime(loadDelay);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void OnClick()
    {
        ResetGameSession();
    }
    
}
