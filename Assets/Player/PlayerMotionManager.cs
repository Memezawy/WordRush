using UnityEngine;

[RequireComponent(typeof(PhysicsController))]
public class PlayerMotionManager : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    private PhysicsController _physicsController;
    private float _groundDistance;
    private bool _isGrounded;

    // Input
    private Vector2 movementVector;

    private float _lastInputDir; // For when the player changes dir it's updated

    public bool Moving { get; private set; }

    private void Start()
    {
        _physicsController = GetComponent<PhysicsController>();
        _groundDistance = transform.lossyScale.x / 2;
    }


    private void Update()
    {
        AssignInput();
        HandleJump();
    }

    private void FixedUpdate()
    {
        UpdateGroundState();


        //HandleDash();

        HandleMoving();
    }


    #region Movement

    [Header("Walking")]
    [SerializeField] private float _speed;
    [SerializeField] private float _linerDrag = 1f;
    [SerializeField] private AudioClip _walkingSoundEffect;

    private void HandleMoving()
    {

        if ((movementVector.x != 0 && !Moving) ||
            (_lastInputDir != movementVector.x && movementVector.x != 0)) // Incase of a change in dir without stopping. (not best fix)
        {
            StartMoving();
            _lastInputDir = movementVector.x;
        }
        else if (movementVector.x == 0 && !_isDashing)
        {
            StopMoving();
        }
        if (movementVector.x != 0 && Mathf.Abs(_physicsController.VelocityX) < _speed)
        {
            StartMoving();
        }
    }

    private void StartMoving()
    {
        _physicsController.SetLinerDrag(0);
        _physicsController.SetVelocity(_speed * movementVector.x, _physicsController.VelocityY);
        Moving = true;
    }

    public void StopMoving()
    {
        // only states were we would want to stop the body
        if (Moving || _isDashing)
        {
            _physicsController.SetLinerDrag(_linerDrag);
            _physicsController.StopTheBody();
            Moving = false;
        }
    }

    #endregion

    #region Jump

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private AudioClip _jumpSoundEffect;

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

    #region Dash

    [Header("Dash")]
    [SerializeField] private AnimationCurve _dashCurve;
    [SerializeField] private float _dashDistance, _dashCoolDown, _dashForce;
    [SerializeField] private AudioClip _dashSoundEffect;
    private Vector2 _dashStartPos;
    private float _nextDashTime;
    private bool _isDashing;
    private bool dashInputDown, dashInputUp;


    private void HandleDash()
    {
        if (dashInputDown && Time.time >= _nextDashTime)
        {
            StartDash();
        }
        // Still Didn't Fully dash
        if (_isDashing)// && Vector2.Distance(_transform.position, _dashStartPos) < _dashDistance)
        {
            Dash();
        }
        // Stop Dashing (the _isDashing to make sure we're not always stopping the dash but only stopping it once)
        if (Vector2.Distance(transform.position, _dashStartPos) >= _dashDistance && _isDashing)
        {
            EndDash();
        }
    }

    public void StartDash()
    {
        _isDashing = true;
        _dashStartPos = transform.position;
        _nextDashTime = Time.time + _dashCoolDown; // Apply cooldown (consider the time it will take to dash)
        _physicsController.SetGravity(0); // so that the player can stay mid-air
        //SoundManager.Instance.PlaySound(_dashSoundEffect);
    }

    private void Dash()
    {
        // Basiclly getting the percentage of distance left then evalutaing it.
        float dashForce = _dashCurve.Evaluate(Vector2.Distance(transform.position, _dashStartPos) / _dashDistance) * _dashForce;
        float dashVector;
        if (_physicsController.VelocityX == 0)
        {
            dashVector = dashForce * base.transform.right.x;
        }
        else
        {
            // Dashes in the dir of motion.
            // so that the value is either 1 or -1.
            dashVector = dashForce * (_physicsController.VelocityX / Mathf.Abs(_physicsController.VelocityX));
        }
        _physicsController.AddForce(dashVector, 0f);
    }

    private void EndDash()
    {
        StopMoving();
        _physicsController.SetGravity(_physicsController.DefualtGravity);
        _isDashing = false;
    }

    #endregion



    private void AssignInput()
    {
        movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dashInputDown = Input.GetKeyDown(KeyCode.LeftShift);
        dashInputUp = Input.GetKeyUp(KeyCode.LeftShift);
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