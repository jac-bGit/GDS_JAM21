using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    [SerializeField] protected bool m_bIsDeathTrap;

    protected AudioManagerComponent m_AudioManager;

    const string SOUND_DESTROY = "Destroy";

    //------------------------------------------------------
    void Start()
    {
        // Get components 
        m_AudioManager = GetComponent<AudioManagerComponent>();
    }

    //---------------------------------------------------------
    private void OnTriggerEnter(Collider other) 
    {
        // Hit player
        if(other.gameObject.tag == GameConstLib.TAG_PLAYER)
        {
            PlayerBehavior playerScr = other.transform.parent.gameObject.GetComponentInChildren<PlayerBehavior>();
           
            // Apply effect 
            if (m_bIsDeathTrap)
            {
                playerScr.Die();
                LevelComponent.FailLevel("You've stepped on trap");
                m_AudioManager.PlaySound(SOUND_DESTROY);
            }
            else
                playerScr.SetupSlowDown();
        }
    }
}
