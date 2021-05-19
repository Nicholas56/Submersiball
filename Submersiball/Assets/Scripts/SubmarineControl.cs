using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//tutorial used:https://www.google.com/search?q=unity+how+to+add+controller+support&oq=unity+how+to+add+controller+support&aqs=chrome..69i57.12469j0j1&sourceid=chrome&ie=UTF-8#kpvalbx=_REyNYO-OHb2W1fAPyqC5gAw23
public class SubmarineControl : MonoBehaviour
{
    PlayerControls controls;
    Rigidbody rb;
    //CameraControl cc;

    Vector2 move;
    Vector2 move2;
    Vector2 look;

    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float maxSpeed = 15.0f;
    [SerializeField] float boostSpeed = 8f;
    [SerializeField] float boostTime = 2;
    [SerializeField] float maxBoostTime = 2;

    [Header("Game")]
    [SerializeField][Range(1,2)] int team = 1;
    [SerializeField] ParticleSystem boostEffects;

    public float currentSpeed;
    public float currentBoost;

    bool accel = false;
    bool boost = false;
    bool refilled = true;

    public bool accelUI = false;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Accelerate.performed += ctx => Accelerate();
        controls.Gameplay.Accelerate2.performed += ctx => Accelerate2();
        controls.Gameplay.Boost.performed += ctx => Boost();
        controls.Gameplay.Boost2.performed += ctx => Boost2();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Move2.performed += ctx => move2 = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move2.canceled += ctx => move2 = Vector2.zero;
        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
        controls.Gameplay.Reset.performed += ctx => ResetCamera();
        controls.Gameplay.Pause.performed += ctx => Pause();
        controls.Gameplay.Item.performed += ctx => UseItem();
        controls.Gameplay.Item2.performed += ctx => UseItem2();

        rb = GetComponent<Rigidbody>();
        //cc = GetComponentInChildren<CameraControl>(); 
    }
    private void Start()
    {
        if (team == 1)
        {
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team1Mat);
        }
        else
        {
            GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team2Mat);
        }
    }

    private void Update()
    {
        if (team == 1)
        {
            Turn(move);
        }
        else
        {
            Turn(move2);
        }

        //cc.SetDeltaPos(new Vector2(look.x, look.y) * 50);

        if (accel&&SinglePlayerSetup.current.inPlay)
        {
            if (rb.velocity.magnitude <= maxSpeed)
            {
                rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
            }

            Debug.Log(rb.velocity.magnitude);

            accelUI = true;
        }
        else
        {
            accelUI = false;
        }

        
        if (boostTime > 0 && boost)
        {
            rb.AddForce(transform.forward * boostSpeed, ForceMode.Acceleration);
            boostTime = Mathf.Max(boostTime - Time.deltaTime, 0);
            if (boostTime <= 0.0f) { boost = false;ToggleBoostEffect(); }
            currentSpeed = rb.velocity.magnitude;
        }
        else //{ boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime); }
        {
            boost = false;
            ToggleBoostEffect();
        }

        if(rb.velocity.magnitude > 14.5f && !boost)
        {
            currentSpeed = 14.5f;
        }
        else
        {
            currentSpeed = rb.velocity.magnitude;
        }

        currentBoost = boostTime;

        RefillBoost();
    }
    void UseItem()
    {
        //code to use pickups goes here
    }
    void UseItem2()
    {
        //and here (player 2)
    }

    void Accelerate()
    {
        if(team==1&&SinglePlayerSetup.current.inPlay)
        accel = !accel;
    }
    void Accelerate2() { if (team == 2&&SinglePlayerSetup.current.inPlay) accel = !accel; }
    void Boost()
    {
        if(team==1&& SinglePlayerSetup.current.inPlay)
        boost = !boost;
        ToggleBoostEffect();
    }
    void Boost2() 
    {
        if (team == 2&& SinglePlayerSetup.current.inPlay) boost = !boost;
        ToggleBoostEffect();
    }
    void ToggleBoostEffect()
    {
        if (!boost) { boostEffects.Stop(true, ParticleSystemStopBehavior.StopEmitting); }
        else { boostEffects.Play(); }
    }
    void ResetCamera()
    {
        //cc.Revert();
    }
    void Pause()
    {
        if (SinglePlayerSetup.current.inPlay) { SinglePlayerSetup.current.Pause(); }
    }
    private void Turn(Vector2 input)
    {
        Vector2 m = new Vector2(input.x, input.y) * Time.deltaTime * 100;
        transform.Rotate(new Vector3(m.y, m.x));
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void StartBoostRefill()
    {
        refilled = false;
        StartCoroutine("StopRefill");
    }

    void RefillBoost()
    {
        if (refilled == false)
        {
            boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime);
        }
    }

    IEnumerator StopRefill()
    {
        yield return new WaitForSeconds(2f);

        refilled = true;   
    }


}
