using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController _characterController;
    public float Speed = 10f;
    private Vector2 _velocity;
    public float JumpHeight = 5f;
    private float GroundDistance = 1.05f;
    private bool _isGrounded = false;
    public float GravityMultiplier = 1.5f;

    private Transform _groundcheck;
    public LayerMask Ground;

    
    void Start()
    {
        _characterController= GetComponent<CharacterController>();
        _groundcheck = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundcheck.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
        _characterController.Move(Speed * Time.deltaTime * move);

        _velocity.y += Physics.gravity.y * GravityMultiplier * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
        }
    }
}
