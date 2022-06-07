using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transition_time = 1f;
    
    
    void Update()
    {
        if (Input.GetKeyDown("l")) //buraya o sahneyle ilgili asıl gameobject collider şartının gelmesi gerekiyor, şimdilik bu
        {
            LoadNextLevel();
        }
    }

    public void GameOver()
    {
        StartCoroutine(LoadLevel(9));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transition_time);
        SceneManager.LoadScene(levelIndex);
    }
}