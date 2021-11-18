/*
Simple component for triggerring level actions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehavior : MonoBehaviour
{
    // Attributes 
    [SerializeField] protected bool m_bTriggerMultiple = false;
    [SerializeField] protected float m_fDelay = 0;
    [SerializeField] protected UnityEvent m_Action;

    // Variables 
    protected bool m_bTriggered = false;

    //---------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //---------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------------------------
    private void OnTriggerEnter(Collider other) 
    {
        if (m_bTriggered)
            return;

        Debug.Log("invoke: " + other.gameObject.tag);

        // Trigger event 
        if(other.gameObject.tag == GameConstLib.TAG_PLAYER)
        {
            Debug.Log("invoke!");
            m_Action.Invoke();
        }
    }

    //---------------------------------------------------------
    protected IEnumerator TriggerAction()
    {
        m_bTriggered = true;
        yield return new WaitForSeconds(m_fDelay);
        m_Action.Invoke();

        if (!m_bTriggerMultiple)
            gameObject.SetActive(false);
        else
            m_bTriggered = false;
    }
}