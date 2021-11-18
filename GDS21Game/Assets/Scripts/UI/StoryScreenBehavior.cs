using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScreenBehavior : MonoBehaviour
{
    [SerializeField] protected GameObject m_Screen;
    [SerializeField] protected Text m_StoryText;
    [SerializeField] protected float m_fFadeSpeed;
    [SerializeField] protected StoryText[] m_aTexts; 

    private bool m_bIsRunning = false;
    private int m_iCurrentText = 0;
    private float m_fTimer;

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    void Start()
    {
        
    }

    //------------------------------------------------------
    void Update()
    {
        // Upadte text displaying 
        if (m_bIsRunning)
            ShowText();
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Stat displaying story text
    public void Display()
    {
        // Setup defaults 
        m_bIsRunning = true;
        m_iCurrentText = 0;
        m_fTimer = m_aTexts[0].m_sTime;
        m_StoryText.text = m_aTexts[0].m_sText;
        m_Screen.SetActive(true);

        // Stop game 
        LevelComponent.SetGameRunning(false);
    }

    //------------------------------------------------------
    // Private functions 
    //------------------------------------------------------

    //------------------------------------------------------
    private void ShowText()
    {
        // Count time 
        if (m_fTimer > 0)
            m_fTimer -= Time.deltaTime;
        else 
        {
            m_iCurrentText++;
            // End 
            if (m_iCurrentText >= m_aTexts.Length)
            {
                m_bIsRunning = false;
                m_Screen.SetActive(false);
                LevelComponent.SetGameRunning(true);
                return;
            }

            // Move to next
            m_fTimer = m_aTexts[m_iCurrentText].m_sTime;
            m_StoryText.text = m_aTexts[m_iCurrentText].m_sText;
        }
    }
}

//------------------------------------------------------
[System.Serializable]
public class StoryText 
{
    [SerializeField] public float m_sTime;

    [TextArea]
    [SerializeField] public string m_sText;
}