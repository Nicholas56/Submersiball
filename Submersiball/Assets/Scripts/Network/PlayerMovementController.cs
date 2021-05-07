using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float boostSpeed = 12f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float boostTime = 2;
    [SerializeField] float maxBoostTime = 2;
    [SerializeField] Rigidbody controller = null;

    Vector2 previousInput;
    bool accel;
    bool boost;
    PlayerControls controls; 
    PlayerControls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new PlayerControls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;
        Controls.Gameplay.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Gameplay.Move.canceled += ctx => ResetMovement();
        controls.Gameplay.Accelerate.performed += ctx => Accelerate();
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    [ClientCallback]
    private void Update() => Move();
    [Client]
    void SetMovement(Vector2 movement) => previousInput = movement;
    [Client]
    void ResetMovement() => previousInput = Vector2.zero;
    [Client]
    void Accelerate() => accel = !accel;
    [Client]
    void Boost() => boost = !boost;
    [Client]
    void Move()
    {
        Vector2 m = new Vector2(previousInput.x, previousInput.y) * Time.deltaTime * 100;
        transform.Rotate(new Vector3(m.y, m.x));

        if (accel)
        {
            controller.AddForce(transform.forward * movementSpeed, ForceMode.Force);
            if (controller.velocity.magnitude > maxSpeed)
            {
                controller.AddForce(transform.forward * -movementSpeed, ForceMode.Force);
            }
        }
        if (boostTime > 0 && boost)
        {
            //boostParticles.Play();
            controller.AddForce(transform.forward * boostSpeed, ForceMode.Acceleration);
            boostTime = Mathf.Max(boostTime - Time.deltaTime, 0);
            if (boostTime <= 0.2f) { boost = false; }
        }
        else { boostTime = Mathf.Min(boostTime + Time.deltaTime, maxBoostTime); }
    }
}
