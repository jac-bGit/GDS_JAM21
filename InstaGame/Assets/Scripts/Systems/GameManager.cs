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

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    private void Awake() {
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
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Loading level from scene manager by name 
    public static void LoadLevelByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
