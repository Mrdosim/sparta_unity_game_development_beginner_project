using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    private SpriteRenderer _npcSpriteRenderer;
    private Rigidbody2D _npcRigidbody2D;
    private NpcAnimationController _npcAnimationController;
    private Vector2 _currentRandomDirection = Vector2.zero;

    public float moveSpeed = 1f; // �̵� �ӵ�
    public float changeDirectionInterval = 5f; // ���� ���� ���� (��)
    public float stopDuration = 3f; // ���� ���� �ð� (��)
    public Vector2 movementBoundsMin; // �̵� ������ �ּ� ��ǥ
    public Vector2 movementBoundsMax; // �̵� ������ �ִ� ��ǥ

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
            _isMoving = !_isMoving; // ������ ���� ���
            _timeSinceLastStop = 0f; // Ÿ�̸� ����
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
            ApplyMovement(Vector2.zero); // ������ ����
            _npcAnimationController.UpdateAnimation(Vector2.zero);
        }
    }

    private void ChangeRandomDirection()
    {
        if (_isMoving) // �����̴� ������ ���� ���� ����
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
        // ��ǥ ���� ����
        position.x = Mathf.Clamp(position.x, movementBoundsMin.x, movementBoundsMax.x);
        position.y = Mathf.Clamp(position.y, movementBoundsMin.y, movementBoundsMax.y);
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            _isMoving = false; // "Level" ���̾�� �浹 �� �������� ����ϴ�.
        }
    }
}