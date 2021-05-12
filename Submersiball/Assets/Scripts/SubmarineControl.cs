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
    Vector2 look;

    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float boostSpeed = 2f;
    [SerializeField] float boostTime = 2;
    [SerializeField] float maxBoostTime = 2;

    public float currentSpeed;
    public float currentBoost;

    bool accel = false;
    bool boost = false;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Accelerate.performed += ctx => Accelerate();
        controls.Gameplay.Boost.performed += ctx => Boost();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => look = Vector2.zero;
        controls.Gameplay.Reset.performed += ctx => ResetCamera();
        controls.Gameplay.Pause.performed += ctx => Pause();

        rb = GetComponent<Rigidbody>();
        cc = GetComponentInChildren<CameraControl>(); 
    }
    private void Start()
    {
        GetComponent<SubMarineColor>().ChangeColors(GameManager.current.team1Mat);
    }

    private void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * 100;
        cc.SetDeltaPos(new Vector2(look.x, look.y)*50);
        transform.Rotate(new Vector3(m.y, m.x));

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
            if (boostTime <= 0.2f) { boost = false; }
        }
        else { boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime); }

        currentSpeed = rb.velocity.magnitude;

        currentBoost = boostTime;
    }

    void Accelerate()
    {
        accel = !accel;
    }
    void Boost()
    {
        boost = !boost;
    }
    void ResetCamera()
    {
        cc.Revert();
    }
    void Pause()
    {
        SinglePlayerSetup.current.Pause();
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
