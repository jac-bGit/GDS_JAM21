/*
Base component class for each level behavior.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComponent : MonoBehaviour
{
    LevelComponent m_Level;

    // Basic references 
    public static bool m_bGameRunnig = true;
    public Transform m_PlayerTransSet;
    public static Transform m_PlayerTrans;

    // UI
    public GameObject m_FailUISet;

    protected static GameObject m_FailUI;

    // Story UI
    public static bool m_bShowStory = false;
    public StoryScreenBehavior m_StoryStart;
    

    public static List<Transform> m_aEnemies = new List<Transform>();

    //------------------------------------------------------


    //------------------------------------------------------
    void Awake()
    {
        m_Level = this;
        m_bGameRunnig = true;
        m_PlayerTrans = m_PlayerTransSet;
        m_FailUI = m_FailUISet;

        if (m_bShowStory)
        {
            m_StoryStart.Display();
            m_bShowStory = false;
        }

        m_aEnemies.Clear();
    }

    //------------------------------------------------------
    void Update()
    {
        // UI updates 
        UpdateFailUI();
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    protected void UpdateFailUI()
    {
        // Check fail screen active
        if (!m_FailUI.activeSelf)
            return;

        // Restart on menu 
        if (Input.GetAxis(GameConstLib.INPUT_FIRE) != 0)
            GameManager.RestartLevel();
    }    

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    public static Transform GetPlayerTransformation() { return m_PlayerTrans; }  

    //------------------------------------------------------
    public static void FailLevel(string failStr)
    {   
        // Show fail screen 
        //m_FailUI.SetActive(true);
        m_bGameRunnig = false;

        // Set text 
        Text failText = m_FailUI.GetComponentInChildren<Text>();
        failText.text = failStr;
    }

    //------------------------------------------------------
    public static IEnumerator TriggerFail(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_FailUI.SetActive(true);
    }

    //------------------------------------------------------
    public static void SetGameRunning(bool runnig) { m_bGameRunnig = runnig; }

    //------------------------------------------------------

}
