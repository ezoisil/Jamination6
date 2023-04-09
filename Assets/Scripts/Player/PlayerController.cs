using System;
using System.Collections;
using System.Collections.Generic;
using Combat_System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float _dashPower = 10;
    private CharacterController controller;
    private DamageReceiver _damageReceiver;
    private bool groundedPlayer;
    private Vector3 playerVelocity;

    private float _jumpCharge;
    private float _jumpChargeTime = 1.5f;
    private float _jumpMin = .3f;
    private float _jumpTimer;
    private bool _isJumping;
    private bool _canMove = true;

    private bool _isDashing;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        _damageReceiver = GetComponent<DamageReceiver>();
    }

    private void Start()
    {
        _damageReceiver.OnDeath += Die;
    }

    private void Die()
    {
        _canMove = false;
    }

    void Update()
    {
        if(!_canMove) return;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && _isJumping)
        {
            _isJumping = false;
            playerVelocity = Vector3.zero;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = CameraRelatedMovement(move);
        controller.Move(move * (Time.deltaTime * playerSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButton("Jump") && groundedPlayer)
        {
            _jumpTimer += Time.deltaTime;
            _jumpCharge = Mathf.Lerp(_jumpMin, 1, _jumpTimer / _jumpChargeTime);
        }
        if (Input.GetButtonUp("Jump") && groundedPlayer)
        {
            _isJumping = true;
            playerVelocity.y = 0;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue * (_jumpCharge));
            playerVelocity.x = move.x * 10 * _jumpCharge;
            playerVelocity.z = move.z * 10 * _jumpCharge;
            Debug.Log(playerVelocity);
            _jumpCharge = 0;
            _jumpTimer = 0;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)) && _isJumping)
        {
            playerVelocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _isJumping)
        {
            _isDashing = true;
            playerVelocity = (transform.forward * (_dashPower ));
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private Vector3 CameraRelatedMovement(Vector3 movementInput)
    {
        var camForward = Camera.main.transform.forward;
        var camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        var adjustedMovement = camRight.normalized * movementInput.x + camForward.normalized * movementInput.z;
        return adjustedMovement;
    }
}
