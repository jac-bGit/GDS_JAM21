/*
Base game handling manager class for basic functinalities.
Manages levels and game data handling.
*/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Base variables
    public static GameManager m_GameManager; 

    protected AudioManagerComponent m_AudioManager;


    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    private void Awake() 
    {
        if (m_GameManager != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            m_GameManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //------------------------------------------------------
    private void Start() {
        // Play music 
            m_AudioManager = GetComponent<AudioManagerComponent>();

            if (m_AudioManager != null)
                m_AudioManager.PlaySound("Music");
    }

    //------------------------------------------------------
    private void Update() 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadLevelByName("MainMenu");
        }
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Loading level from scene manager by name 
    public static void LoadLevelByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    //------------------------------------------------------
    // Loading level from scene manager by name 
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //------------------------------------------------------
    public static void QuitGame() { Application.Quit(); }

    //------------------------------------------------------
    public static void ShowStory(bool show) { LevelComponent.m_bShowStory = show; }
}
