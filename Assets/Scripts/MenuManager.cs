using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public string CurrentLevel;
    public string NextLevel;
    public bool isALoadingScreen;
    public float TimeDelay = 3;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }
    public void ProceedToAnotherLevel()
    {
        if(NextLevel != null)
        SceneManager.LoadScene(NextLevel);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    public void LoadALevel(string levelName)
    { 
        SceneManager.LoadScene(levelName);
    }
    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(TimeDelay);
        ProceedToAnotherLevel();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isALoadingScreen)
        {
            StartCoroutine(StartScreen());
        }
    }
}
