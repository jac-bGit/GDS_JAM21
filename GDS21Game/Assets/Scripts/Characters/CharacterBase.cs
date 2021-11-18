/*
 Base for character behavior - player and enemies
 Speed and forward movement, getting hitted
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    // Movement
    [SerializeField] protected float m_fSpeed;
    [SerializeField] protected float m_fSpeedSlowDown;

    // Hit management 
    protected bool m_bHitted;
    [Range(1.0f, 10.0f)]
    [SerializeField] protected float m_fSlowDownTime;
    protected float m_fSlowDownCounter;

    //------------------------------------------------------
    // Protected functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Count time for enemy slowdown
    protected void SlowDownCount()
    {
        // Hitted check 
        if (!m_bHitted)
            return;

        // Count time 
        if (m_fSlowDownCounter > 0)
            m_fSlowDownCounter -= Time.deltaTime;
        else 
            m_bHitted = false;
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------
    public virtual void SetupSlowDown(bool hitted = true)
    {
        m_bHitted = hitted;

        // Start timer 
        if (m_bHitted)
            m_fSlowDownCounter = m_fSlowDownTime;
    }
}
