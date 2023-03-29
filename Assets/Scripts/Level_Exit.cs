using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Exit : MonoBehaviour
{
    [SerializeField]Animator animator;
    [SerializeField]float levelLoadDelay=3f; 
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator LoadNextLevel()
    { 
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        FindObjectOfType<Scene_persist>().ResetScenePersist();
    }
    public void FadeToLevel1(int currentSceneIndex)
    {
        animator.SetTrigger("FadeOut");
    }
    public void OnClick()
    {
        StartCoroutine(LoadNextLevel());
        FadeToLevel1(1);

    }
    
}
