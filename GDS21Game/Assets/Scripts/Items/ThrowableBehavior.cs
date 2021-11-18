/*
Component for throwable items behavior.
Picking, throwing, influence enemy
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{
    // Basic values 
    protected bool m_bIsPicked = false;
    protected bool m_bApplyPhysics;

    // Physics 

    // Components 
    protected Rigidbody m_Rigidbody;
    protected Animator m_Animator;
    protected Collider m_Collider;
    protected AudioManagerComponent m_AudioManager;

    const string ANIM_MOVE = "Move";

    const string SOUND_PICK = "Pick";
    //const string SOUND_THROW = "Throw";
    const string SOUND_DESTROY = "Destroy";

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    void Awake()
    {
        // Get components
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider>();
        m_AudioManager = GetComponent<AudioManagerComponent>();

        // Default setup 
        ActivatePhysics(false);
        m_Animator.SetBool(ANIM_MOVE, true);
    }

    //------------------------------------------------------
    void Update()
    {
    }

    //------------------------------------------------------
    void OnCollisionEnter(Collision other)
    {
        // Player hit
        PlayerBehavior playerScr = ComponentByCollisionTag<PlayerBehavior>(other, GameConstLib.TAG_PLAYER);
        if (playerScr != null)
        {
            // Set throwable
            playerScr.SetThrowableObject(this);
            m_bIsPicked = true;
            m_Animator.SetBool(ANIM_MOVE, false);
            m_Animator.enabled = false;
            m_Collider.enabled = false;

            m_AudioManager.PlaySound(SOUND_PICK);
        }

        // Enemy hit
        EnemyBehavior enemyScr = ComponentByCollisionTag<EnemyBehavior>(other, GameConstLib.TAG_ENEMY);
        if (enemyScr != null && m_bApplyPhysics && m_bIsPicked)
        {
            enemyScr.SetupSlowDown();
            enemyScr.PlayHitSound();
        }

        Debug.Log(other.gameObject.tag + " to tag: " + GameConstLib.TAG_OBSTACLE);

        // Obstacle hit 
        if (other.gameObject.tag == GameConstLib.TAG_OBSTACLE && m_bApplyPhysics)
        {
            Destroy(other.gameObject);
        }

        // Get destroyed on each hit 
        /*if (playerScr == null)
        {
            Destroy(this.gameObject);
            m_AudioManager.PlaySound(SOUND_DESTROY);
        }*/
    }

    //------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        // Obstacle hit 
        if (other.gameObject.tag == GameConstLib.TAG_OBSTACLE && m_bApplyPhysics)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            m_AudioManager.PlaySound(SOUND_DESTROY);
        }
    }


    //------------------------------------------------------
    protected T ComponentByCollisionTag<T>(Collision collision, string tag)
    {   
        if (collision.gameObject.tag == tag)
            return collision.gameObject.GetComponent<T>();

        return default(T);
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Apply forces on throwable object
    public void ApplyThrowForce(Vector3 forcePower)
    {
        // Activate 
        ActivatePhysics(true);

        // Apply force 
        m_Rigidbody.AddForce(forcePower);
        m_Collider.enabled = true;
        //m_AudioManager.PlaySound(SOUND_THROW);
    }

    //------------------------------------------------------
    public void ActivatePhysics(bool activate)
    {   
        m_bApplyPhysics = true;

        // Set rigidbody
        m_Rigidbody.isKinematic = !activate;
    }
}
