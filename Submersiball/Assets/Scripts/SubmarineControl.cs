using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//tutorial used:https://www.google.com/search?q=unity+how+to+add+controller+support&oq=unity+how+to+add+controller+support&aqs=chrome..69i57.12469j0j1&sourceid=chrome&ie=UTF-8#kpvalbx=_REyNYO-OHb2W1fAPyqC5gAw23
public class SubmarineControl : MonoBehaviour
{
    PlayerControls controls;
    Rigidbody rb;
    CameraControl cc;

    Vector2 move;
    Vector2 move2;
    Vector2 look;

    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float boostSpeed = 2f;
    [SerializeField] float boostTime = 2;
    [SerializeField] float maxBoostTime = 2;

    [Header("Game")]
    [SerializeField][Range(1,2)] int team = 1;

    public float currentSpeed;
    public float currentBoost;

    bool accel = false;
    bool boost = false;

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

        rb = GetComponent<Rigidbody>();
        cc = GetComponentInChildren<CameraControl>(); 
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

        cc.SetDeltaPos(new Vector2(look.x, look.y) * 50);

        if (accel)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.AddForce(transform.forward * -moveSpeed, ForceMode.Force);
            }
        }
        if (boostTime > 0 && boost)
        {
            //boostParticles.Play();
            rb.AddForce(transform.forward * boostSpeed, ForceMode.Acceleration);
            boostTime = Mathf.Max(boostTime - Time.deltaTime, 0);
            if (boostTime <= 0.0f) { boost = false; }
        }
        else //{ boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime); }

        currentSpeed = rb.velocity.magnitude;

        currentBoost = boostTime;
    }

    void Accelerate()
    {
        if(team==1)
        accel = !accel;
    }
    void Accelerate2() { if (team == 2) accel = !accel; }
    void Boost()
    {
        if(team==1)
        boost = !boost;
    }
    void Boost2() { if (team == 2) boost = !boost; }
    void ResetCamera()
    {
        cc.Revert();
    }
    void Pause()
    {
        SinglePlayerSetup.current.Pause();
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
}
