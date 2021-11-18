/*
Base enemy behavior
Chase player, move sides, dealing with dmg effects 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : CharacterBase
{
    [SerializeField] protected float m_fCatchDistance;
    protected AudioManagerComponent m_AudioManager;

    // Components 
    protected Rigidbody m_Rigidbody;
    protected Transform m_PlayerTrans;

    const string SOUND_HIT = "Hit";

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    void Start()
    {
        // Get components 
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerTrans = LevelComponent.GetPlayerTransformation();
        m_AudioManager = GetComponent<AudioManagerComponent>();

        // Register enemy transform 
        LevelComponent.m_aEnemies.Add(this.transform);
    }

    //------------------------------------------------------
    void Update()
    {
        if (!LevelComponent.m_bGameRunnig)
            return;

        // Hit management
        SlowDownCount();
        CatchPlayer();
    }

    //------------------------------------------------------
    void FixedUpdate()
    {
        if (!LevelComponent.m_bGameRunnig)
            return;

        // Movement 
        Move();
    }

    //------------------------------------------------------
    // Private functions 
    //------------------------------------------------------
    
    //------------------------------------------------------
    // Move straigh and adjust speed by hit 
    protected void Move()
    {
        // Pick speed
        float speed = m_fSpeed;
        if (m_bHitted)
            speed = m_fSpeedSlowDown;

        // Direction to player 
        //Vector3 speeds = transform.forward * speed;
        Vector3 dir = m_PlayerTrans.position - transform.position;
        dir *= speed;
        dir = new Vector3(dir.x, 0, dir.z);

        // Set velocity 
        m_Rigidbody.MovePosition(m_Rigidbody.position + dir * Time.fixedDeltaTime);
    }

    //------------------------------------------------------
    protected void CatchPlayer()
    {
        if (transform.position.z >= m_PlayerTrans.position.z - m_fCatchDistance)
        {
            PlayerBehavior playerScr = m_PlayerTrans.GetComponentInChildren<PlayerBehavior>();
            playerScr.Die();
            LevelComponent.FailLevel("You've been caught");
        }

    }
    
    public void PlayHitSound() { m_AudioManager.PlaySound(SOUND_HIT); }
}
 