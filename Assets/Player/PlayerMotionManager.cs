using UnityEngine;

[RequireComponent(typeof(PhysicsController))]
public class PlayerMotionManager : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundDistance;
    private PhysicsController _physicsController;
    private Animator _animator;
    private bool _isGrounded;

    // Input
    private Vector2 _movementVector;
    private float _lastInputDir; // For when the player changes dir it's updated
    public bool IsMoving { get; private set; }

    private void Start()
    {
        _physicsController = GetComponent<PhysicsController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AssignInput();
        HandleJump();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        UpdateGroundState();

        HandleMoving();
    }


    #region Movement

    [Header("Walking")]
    [SerializeField] private float _speed;
    [SerializeField] private float _linerDrag = 1f;

    private void HandleMoving()
    {

        if ((_movementVector.x != 0 && !IsMoving) ||
            (_lastInputDir != _movementVector.x && _movementVector.x != 0)) // Incase of a change in dir without stopping. (not best fix)
        {
            StartMoving();
            _lastInputDir = _movementVector.x;
        }
        else if (_movementVector.x == 0)
        {
            StopMoving();
        }
        if (_movementVector.x != 0 && Mathf.Abs(_physicsController.VelocityX) < _speed)
        {
            StartMoving();
        }
    }

    private void StartMoving()
    {
        _physicsController.SetLinerDrag(0);
        _physicsController.SetVelocity(_speed * _movementVector.x, _physicsController.VelocityY);
        IsMoving = true;
    }

    public void StopMoving()
    {
        // only states were we would want to stop the body
        if (IsMoving)
        {
            _physicsController.SetLinerDrag(_linerDrag);
            _physicsController.StopTheBody();
            IsMoving = false;
        }
    }

    #endregion

    #region Jump

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallSpeed;

    private bool jumpInputDown, jumpInputUp;

    private void HandleJump()
    {
        if (jumpInputDown && _isGrounded)
        {
            Jump();
        }
        if (jumpInputUp && _physicsController.VelocityY > 0.1f) // Let go while in-air
        {
            // The fall force is then handled by the system normally
            _physicsController.SetVelocity(_physicsController.VelocityX, 0f);
        }
        if (_physicsController.IsFalling && !_isGrounded) // Falling
        {
            _physicsController.SetLinerDrag(0);
            // Bec this state is continuios.
            _physicsController.AddForce(0, -_fallSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        _physicsController.SetLinerDrag(0);
        _physicsController.AddForce(0f, _jumpForce);
        //SoundManager.Instance.PlaySound(_jumpSoundEffect);
    }
    #endregion

    private void HandleAnimation()
    {
        _animator.SetBool("IsRunning", IsMoving);
        _animator.SetBool("IsAirborne", _physicsController.VelocityY > 0);
        _animator.SetBool("IsFalling", _physicsController.VelocityY < 0);

        if (_movementVector.x > 0)
            LookRight();
        else if (_movementVector.x < 0) // Else if so that when we're not moving it doesn't just set to a default rotation.
            LookLeft();

    }

    private void AssignInput()
    {
        _movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpInputDown = Input.GetKeyDown(KeyCode.Space);
        jumpInputUp = Input.GetKeyUp(KeyCode.Space);
    }

    private void UpdateGroundState()
    {
        _isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - _groundDistance),
                new Vector2(0.5f, 0.2f), 0f, _groundMask) && _physicsController.VelocityY == 0;
    }

    public void LookRight()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
    }

    public void LookLeft()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
    }

    private void OnDisable()
    {
        StopMoving();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - _groundDistance),
            new Vector2(0.5f, 0.2f));
    }
}