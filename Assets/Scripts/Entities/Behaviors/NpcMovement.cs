using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    private SpriteRenderer _npcSpriteRenderer;
    private Rigidbody2D _npcRigidbody2D;
    private NpcAnimationController _npcAnimationController;
    private Vector2 _currentRandomDirection = Vector2.zero;

    public float moveSpeed = 1f; // 이동 속도
    public float changeDirectionInterval = 5f; // 방향 변경 간격 (초)
    public float stopDuration = 3f; // 멈춤 지속 시간 (초)
    public Vector2 movementBoundsMin; // 이동 가능한 최소 좌표
    public Vector2 movementBoundsMax; // 이동 가능한 최대 좌표

    private float _timeSinceLastDirectionChange = 0f;
    private float _timeSinceLastStop = 0f;
    private bool _isMoving = false;

    private void Awake()
    {
        _npcRigidbody2D = GetComponent<Rigidbody2D>();
        _npcSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _npcAnimationController = GetComponent<NpcAnimationController>();
    }

    private void Start()
    {
        ChangeRandomDirection();
    }

    private void Update()
    {

        _timeSinceLastDirectionChange += Time.deltaTime;
        if (_timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeRandomDirection();
        }

        _timeSinceLastStop += Time.deltaTime;
        if (_timeSinceLastStop >= stopDuration)
        {
            _isMoving = !_isMoving; // 움직임 상태 토글
            _timeSinceLastStop = 0f; // 타이머 리셋
        }

        CheckBounds();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            ApplyMovement(_currentRandomDirection);
            _npcAnimationController.UpdateAnimation(_currentRandomDirection);
        }
        else
        {
            ApplyMovement(Vector2.zero); // 움직임 멈춤
            _npcAnimationController.UpdateAnimation(Vector2.zero);
        }
    }

    private void ChangeRandomDirection()
    {
        if (_isMoving) // 움직이는 상태일 때만 방향 변경
        {
            _currentRandomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _npcSpriteRenderer.flipX = _currentRandomDirection.x > 0;
        }

        _timeSinceLastDirectionChange = 0f;
    }

    private void ApplyMovement(Vector2 direction)
    {
        _npcRigidbody2D.velocity = direction * moveSpeed;
    }

    private void CheckBounds()
    {
        Vector2 position = transform.position;
        // 좌표 범위 제한
        position.x = Mathf.Clamp(position.x, movementBoundsMin.x, movementBoundsMax.x);
        position.y = Mathf.Clamp(position.y, movementBoundsMin.y, movementBoundsMax.y);
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            _isMoving = false; // "Level" 레이어와 충돌 시 움직임을 멈춥니다.
        }
    }
}