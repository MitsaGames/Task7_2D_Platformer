using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class KnightMove : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 2f;
    [SerializeField] private float _jumpingHorizontalSpeed = 5f;
    [SerializeField] private float _jumpingForce = 10f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private ContactFilter2D _contactFilterGround = default;
    private List<ContactPoint2D> _contacts = new List<ContactPoint2D>();
    private bool _readyToJump = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _contactFilterGround.useTriggers = false;
        _contactFilterGround.SetLayerMask(1 << LayerMask.NameToLayer("Ground"));
    }

    private void FixedUpdate()
    {
        bool isWalking = false;
        bool isGrounded = IsGrounded();

        float velocityY = _rigidbody.velocity.y;
        int direction = 0;
        float speed = (isGrounded) ? _baseSpeed : _jumpingHorizontalSpeed;

        if (!isGrounded)
        {
            _readyToJump = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
            isWalking = true;
        }
        
        _rigidbody.velocity = new Vector2(speed * direction, velocityY);

        if (isGrounded && _readyToJump == true && Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * _jumpingForce, ForceMode2D.Impulse);
            _readyToJump = false;
        }

        _animator.SetBool(KnightAnimatorController.Params.Walk, isWalking);
        _animator.SetBool(KnightAnimatorController.Params.Jump, !isGrounded);
    }

    private bool IsGrounded()
    {
        _rigidbody.GetContacts(_contactFilterGround, _contacts);

        foreach (var contactPoint in _contacts)
        {
            if (contactPoint.otherCollider.GetComponent<GroundDetector>())
            {
                return true;
            }
        }

        return false;
    }
}
