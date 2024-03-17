using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    #region Variables

    [SerializeField] private float _sprintSpeed, _walkSpeed, _smoothTime, _mouseSensitivity, _jumpForce;
    [SerializeField] LayerMask _groundLayers;
    [SerializeField] Transform _playerModel;
    [SerializeField] private CinemachineVirtualCamera fpsCam;
    [SerializeField] private CinemachineVirtualCamera tpsCam;
    [SerializeField] private GameObject _cinemachineCameraTarget;
    [SerializeField] private float _speedChangingSmoothness = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GroundChecker groundChecker;
    private Camera cam;

    private Vector3 stopPl = Vector3.zero;
    private Rigidbody _rigidbody;
    private float _speed;
    private Vector3 _spawnPosition;
    private bool _isMovable = true;
    private bool _isGrounded;
    private Vector3 smoothMoveVelocity;
    private Vector3 moveAmount;
    private bool isFpsEnabled;

    //tps cam rotation
    private float _cinemachineTargetYaw = 0;
    private float _cinemachineTargetPitch = 0;
    private float TopClamp = 70.0f;
    private float BottomClamp = -30.0f;
    private float CameraAngleOverride = 0.0f;

    // animation IDs
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDSpeed;

    private bool isJumped;

    //interactions
    private bool interacted;
    private I_Interactable lastInteracted;
    private bool facedAlready;

    #endregion

    public Vector3 firstSpawnPos;
    public Vector3 foodPlacePos;

    private void Awake()
    {
        Instance = this;
        _spawnPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Utils.SetMouseLockedState(true);
        cam = Camera.main;
        AssignAnimationIDs();
        GameInput.Instance.OnChangeCam += GameInput_OnChangeCam;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        switch (e.CurrentState)
        {
            case GameManager.State.GameStarted:
                _rigidbody.MovePosition(firstSpawnPos);
                break;
            case GameManager.State.EnteredFoodPlace:
                _rigidbody.MovePosition(foodPlacePos);
                break;
        }
    }

    private void Update()
    {
        Jump();
        HandleInteractions();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnChangeCam -= GameInput_OnChangeCam;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameInput_OnChangeCam(object sender, System.EventArgs e)
    {
        if (EventSystem.current.currentSelectedGameObject)
            return;
        SwitchCameraMode();
    }

    private void SwitchCameraMode()
    {
        fpsCam.enabled = !fpsCam.isActiveAndEnabled;
        tpsCam.enabled = !tpsCam.isActiveAndEnabled;
        int layer = tpsCam.enabled ? LayerFinder.GetIndex(Layer.Default) : LayerFinder.GetIndex(Layer.Unvisible);
        //Make player unvisible on fps camera for better result
        Utils.SetRenderLayerInChildren(_playerModel, layer);
    }

    private void AssignAnimationIDs()
    {
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDSpeed = Animator.StringToHash("Speed");
    }

    private void Move()
    {
        if (EventSystem.current.currentSelectedGameObject || !_isMovable)
        {
            moveAmount = stopPl;
            _animator.SetFloat(_animIDSpeed, 0);
            return;
        }

        Vector2 movement = GameInput.Instance.GetMovementVectorNormalized();


        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDir = (forward * movement.y + right * movement.x).normalized;

        moveDir.y = 0f;

        if (GameInput.Instance.GetInputActions().Player.Sprint.IsPressed())
        {
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * _sprintSpeed, ref smoothMoveVelocity, _smoothTime);
        }
        else
        {
            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * _walkSpeed, ref smoothMoveVelocity, _smoothTime);
        }

        _speed = Mathf.Lerp(_speed, moveAmount.magnitude, Time.fixedDeltaTime * _speedChangingSmoothness);
        _animator.SetFloat(_animIDSpeed, _speed);

        //Rotation
        if (tpsCam.isActiveAndEnabled)
        {
            if (movement != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                _playerModel.transform.rotation =
                    Quaternion.Lerp(_playerModel.transform.rotation, rotation, Time.deltaTime * 4f);
            }
        }
        else if (fpsCam.isActiveAndEnabled)
        {
            Quaternion rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
            _playerModel.transform.rotation =
                Quaternion.Lerp(_playerModel.transform.rotation, rotation, Time.deltaTime * 4f);
        }

        _rigidbody.MovePosition(transform.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        CheckFallRespawn();
    }

    private void HandleInteractions()
    {
        Ray ray = cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        float interactDistance = fpsCam.isActiveAndEnabled ? 5 : 10;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactDistance))
        {
            if (raycastHit.transform.TryGetComponent(out I_Interactable interactableObj))
            {
                Debug.Log("gördüm");
                //interactable object has found
                interacted = true;
                lastInteracted = interactableObj;
                if (!facedAlready)
                {
                    interactableObj.OnFaced();
                    facedAlready = true;
                }

                if (GameInput.Instance.IsInteractKeyPressed())
                {
                    Debug.Log("bastım");
                    interactableObj.Interact();
                }
            }
            else
            {
                if (interacted && lastInteracted != null)
                {
                    lastInteracted.OnInteractEnded();
                    interacted = false;
                    lastInteracted = null;
                }

                facedAlready = false;
            }
        }
        else
        {
            if (interacted && lastInteracted != null)
            {
                lastInteracted.OnInteractEnded();
                interacted = false;
                lastInteracted = null;
            }

            facedAlready = false;
        }
    }

    private void CheckFallRespawn()
    {
        if (transform.position.y < -12)
        {
            Teleport(_spawnPosition);
        }
    }

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }

    private void CameraRotation()
    {
        _cinemachineTargetYaw += GameInput.Instance.GetMouseLook().x * _mouseSensitivity;
        _cinemachineTargetPitch += -GameInput.Instance.GetMouseLook().y * _mouseSensitivity;
        // clamp our rotations so our values are limited to 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
        _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, Time.deltaTime * 4f);
    }

    private void Jump()
    {
        _isGrounded = groundChecker.isGrounded;
        if (_isGrounded)
        {
            // Jump
            if (GameInput.Instance.IsJumpButtonPressed())
            {
                isJumped = true;
                // apply jump force
                _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

                _animator.SetBool(_animIDJump, true);
            }
            else if (isJumped)
            {
                // player jumped and also grounded so set the jumped bool to false
                _animator.SetBool(_animIDJump, false);
                isJumped = false;
            }
        }

        _animator.SetBool(_animIDGrounded, _isGrounded);
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}