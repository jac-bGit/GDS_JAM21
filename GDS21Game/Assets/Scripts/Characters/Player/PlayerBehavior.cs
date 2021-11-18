/*
Base player behavior
Side movement, roting, picking and throwing objects
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : CharacterBase
{

    // Movement
    [SerializeField] private float m_fSideSpeed;
    
    [SerializeField] private float m_fRotationSpeed;
    [SerializeField] private float m_fLeanAmount;
    
    private bool m_bIsMoving = false;

    private bool m_bRotated;
    private Quaternion m_RotationOriginal;

    // Throwing
    [SerializeField] private Transform m_Hand;
    [SerializeField] private ThrowableBehavior m_ThrowableObject;
    [SerializeField] private float m_fThrowSpeed;
    [Range(-1, 1)]
    [SerializeField] private float m_fThrowFlatness;

    // UI
    [SerializeField] private Image m_DistateIndicator;
    [SerializeField] private float m_fEnemyDistMax;
    [SerializeField] private float m_fEnemyDistMin;
    [SerializeField] private float m_fDistUISpeed;

    // Inputs 
    protected float m_fInputHorizontal;

    // Components
    private Rigidbody m_Rigidbody;
    protected AudioManagerComponent m_AudioManager;
    protected Animator m_Animator;
    public AudioSource m_StepAudio;

    const string SOUND_STEP = "Step";
    const string SOUND_THROW = "Throw";
    const string SOUND_DIE = "Die";
    const string ANIM_DIE = "Die";

    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    void Start()
    {
        // Get components 
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioManager = GetComponent<AudioManagerComponent>();
        m_Animator = GetComponent<Animator>();

        // Set default values 
        m_RotationOriginal = transform.rotation;

        // Start step coroutine 
        StartCoroutine("PlayStep");
    }

    //------------------------------------------------------
    private IEnumerator PlayStep()
    {
        if (LevelComponent.m_bGameRunnig)
            m_AudioManager.PlaySound(SOUND_STEP);

        yield return new WaitForSeconds(0.4f);

        StartCoroutine("PlayStep");
    }

    //------------------------------------------------------
    void Update()
    {
        if (!LevelComponent.m_bGameRunnig)
            return;

        if (!m_bIsMoving)
        {
            m_StepAudio.Play();
            m_bIsMoving = true;
        }

        // Movements 
        Rotate();
        FixedUpdateInputs();
        SlowDownCount();

        // Object throwing on input - allow only if is turned back 
        if (Input.GetAxis(GameConstLib.INPUT_FIRE) != 0)
            ThrowObject();

        // UI
        DistanceCheckUI();
    }

    //------------------------------------------------------
    void FixedUpdate() 
    {
        if (!LevelComponent.m_bGameRunnig)
            return;

        // Movements 
        Move();
    }

    //------------------------------------------------------
    // Public functions 
    //------------------------------------------------------

    //------------------------------------------------------
    public void SetThrowableObject(ThrowableBehavior throwable)
    {
        // Check already has throwable
        if (m_ThrowableObject != null)
            return;

        m_ThrowableObject = throwable;

        // Set transformations  
        Transform trans = m_ThrowableObject.transform;
        trans.parent = m_Hand;
        trans.localPosition = Vector3.zero;
        trans.rotation = m_Hand.rotation;

        // Set behavior 
        m_ThrowableObject.ActivatePhysics(false);
    }

    //------------------------------------------------------
    // Private functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Register all inputs for fixed update 
    private void FixedUpdateInputs()
    {
        m_fInputHorizontal = Input.GetAxis(GameConstLib.INPUT_HORIZONTAL);
    }

    //------------------------------------------------------
    // Move in straight line 
    private void Move()
    {
        // Running straight 
        float speed = m_fSpeed;
        if (m_bHitted || m_bRotated)
        {
            Debug.Log("use slowed speed!");
            speed = m_fSpeedSlowDown;   
        }

        Vector3 straight = Vector3.forward;

        // Move sides
        float h = m_fInputHorizontal; 
        h = m_bRotated? -h : h;

        Vector3 side = Vector3.right * h;

        // Move
        Vector3 move = new Vector3(h, 0, 1);
        Vector3 dir = move.normalized;
        Vector3 speeds = new Vector3(m_fSideSpeed, 0, speed);

        // Apply move 
        m_Rigidbody.MovePosition(m_Rigidbody.position + Vector3.Scale(dir, speeds) * Time.fixedDeltaTime);

        // Lean body with movement 
        Vector3 rot = transform.eulerAngles;
        rot = new Vector3(rot.x, rot.y, m_fLeanAmount * h);
        transform.eulerAngles = rot;
    }

    //------------------------------------------------------
    // Rotate front and backwards
    private void Rotate()
    {
        // Inputs 
        float rotInput = Input.GetAxis(GameConstLib.INPUT_ROTATE);
        m_bRotated = rotInput != 0;

        Quaternion rot = transform.rotation;

        // Set target rotation 
        Quaternion rotTarget = new Quaternion(0, rotInput, 0, 0);
        if (rotInput == 0)   
            rotTarget = m_RotationOriginal;

        // Apply rotation
        transform.rotation = Quaternion.Slerp(
            rot, 
            rotTarget, 
            m_fRotationSpeed * Time.deltaTime
        );
    }

    //------------------------------------------------------
    // Throw objects 
    private void ThrowObject()
    {
        // Check throwable object 
        if (m_ThrowableObject == null)
            return;

        // Remove from holder 
        m_ThrowableObject.transform.parent = null;

        // Add force 
        Vector3 force = transform.forward * m_fThrowSpeed;
        force = new Vector3(force.x, m_fThrowSpeed * m_fThrowFlatness, force.z);
        m_ThrowableObject.ApplyThrowForce(force);

        // Remove 
        m_ThrowableObject = null;

        // Play sound 
        m_AudioManager.PlaySound(SOUND_THROW);
    }

    private float m_fIntensity = 0;

    //------------------------------------------------------
    // Based on closer enemy setup distance UI
    private void DistanceCheckUI()
    {
        // Check enemies 
        if (LevelComponent.m_aEnemies == null || LevelComponent.m_aEnemies.Count == 0)
        {
            m_DistateIndicator.enabled = false;
            return;
        }

        // Get closest 
        float dist = Vector3.Distance(transform.position, LevelComponent.m_aEnemies[0].position);
        foreach(Transform enemy in LevelComponent.m_aEnemies)
        {
            float nextDist = Vector3.Distance(transform.position, enemy.position);

            if (dist > nextDist)
                dist = nextDist;
        }

        // Set ui
        m_DistateIndicator.enabled = true;

        // Get intensity 
        float intensity = (dist - 2) / (m_fEnemyDistMax - m_fEnemyDistMin);
        intensity = 1 - intensity;
        intensity = Mathf.Clamp(intensity, 0, 1);
        intensity *= 255;

        m_fIntensity = Mathf.Lerp(m_fIntensity, intensity, m_fDistUISpeed * Time.deltaTime);

        // Set color 
        Color col = m_DistateIndicator.color;
        m_DistateIndicator.color = new Color(col.r, col.g, col.b, m_fIntensity);
    }

    //------------------------------------------------------
    public void Die() 
    {
        m_Animator.SetBool(ANIM_DIE, true);
        m_AudioManager.PlaySound(SOUND_DIE);

        StartCoroutine(LevelComponent.TriggerFail(0.25f));

        // Stop sound 
        AudioSource[] sounds = GetComponents<AudioSource>();
        foreach(AudioSource snd in sounds)
        {
            snd.enabled = false;
        }
    }
}
