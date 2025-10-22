using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Character Input")]
    public PlayerInput _playerInput;
    public Vector2 move;
    public Vector2 look;

    [Header("Player")]
    public float MoveSpeed = 10.0f;
    public float AccelRate = 10.0f;
    private float _speed;

    [Header("Interaction")]
    public float _interactDistance = 3f;
    private Interactable _curInteractable;
    public Image _crosshair;
    public Sprite _defaultCrosshair;
    public Sprite _interactCrosshair;

    [Header("Camera")]
    public Camera _playerCam;
    [SerializeField] float _topClamp = 90f;
    [SerializeField] float _bottomClamp = -90f;
    [SerializeField] float _cameraSens = 1;
    float _cameraPitch;
    float _playerYaw;

    public bool _canControl = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        _cameraPitch = 0f;
        _playerCam.transform.localRotation = Quaternion.identity;

        StoryController.Instance.StartGame();
    }

    void LateUpdate()
    {
        if (_canControl)
        {
            CameraRotation();
        }
    }

    void Update()
    {
        if (_canControl)
        {
            Move();
            InteractableCheck();
        }
    }

    private void InteractableCheck()
    {
        Ray ray = new Ray(GetEyePos().position, GetLookDir());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (interactable._canInteract)
                {
                    _curInteractable = interactable;
                    _crosshair.sprite = _interactCrosshair;
                    return;
                }
            }
        }
        _curInteractable = null;
        _crosshair.sprite = _defaultCrosshair;
    }

    private void TryInteract()
    {
        if (_curInteractable != null)
        {
            _curInteractable.Interact();
            //get bool for crosshair maybe
        }
    }

    private void Move()
    {
        float targetSpeed = MoveSpeed;
        if (move == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = _speed;

        float speedOffset = 0.1f;
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * AccelRate);
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = transform.right * move.x + transform.forward * move.y;
        rb.MovePosition(inputDirection.normalized * (Time.deltaTime * _speed) + rb.position);
    }

    private void CameraRotation()
    {
        _cameraPitch += look.y * -_cameraSens;
        _playerYaw += look.x * _cameraSens;

        _cameraPitch = Mathf.Clamp(_cameraPitch, _bottomClamp, _topClamp);

        _playerCam.transform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, _playerYaw, 0f);
    }
    public Vector3 GetLookDir()
    {
        return _playerCam.transform.forward;
    }
    public Transform GetEyePos()
    {
        return _playerCam.transform;
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.sqrMagnitude < 0.001f)
            input = Vector2.zero;

        look = input;
    }

    public void OnInteract(InputValue value)
    {
        TryInteract();
    }

    public void OnInventory(InputValue value)
    {
        Inventory.Instance.OpenInv(value.isPressed);
    }
}
