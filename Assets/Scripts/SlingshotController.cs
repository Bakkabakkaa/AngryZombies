using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _rangeCollider;
    [SerializeField] private float _powerMultiplier = 10f;
    [SerializeField] private Rigidbody2D _skullRb;
    [SerializeField] private Transform _skull;
    [SerializeField] private AudioClip _audioClip;
    
    private SlingLinesController _slingLinesController;
    public Vector2 SkullForce { get; private set; }
    public Transform GrabbedSkull { get; private set; }

    private Vector2 _center;
    private bool _isFlying;
    

    private void Start()
    {
        _slingLinesController = GetComponent<SlingLinesController>();
        _center = _rangeCollider.transform.position + (Vector3)_rangeCollider.offset;
        ResetSkull();
    }

    private void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Возврат черепа
        if (_isFlying && _skullRb.velocity.magnitude < 0.01f)
        {
            ResetSkull();
        }

        // Захват
        if (Input.GetMouseButtonDown(0))
        {
            if ((mouse - (Vector2)_skull.position).magnitude < 0.5f)
            {
                GrabbedSkull = _skull;

                // ► ВКЛЮЧАЕМ РЕЗИНКИ
                _slingLinesController.StartDrawing();
                PlayAudioClip();
            }
        }

        // Перетягивание
        if (Input.GetMouseButton(0) && GrabbedSkull != null)
        {
            Vector2 dir = mouse - _center;
            float radius = _rangeCollider.radius * _rangeCollider.transform.lossyScale.x;

            dir = Vector2.ClampMagnitude(dir, radius);
            _skull.position = _center + dir;

            Vector2 velocity = (_center - (Vector2)_skull.position) * _powerMultiplier;
            SkullForce = velocity;
        }

        // Отпускание
        if (Input.GetMouseButtonUp(0) && GrabbedSkull != null)
        {
            ThrowSkull();
            GrabbedSkull = null;
        }
    }

    private void ThrowSkull()
    {
        _skullRb.isKinematic = false;
        _skullRb.AddForce(SkullForce, ForceMode2D.Impulse);
        _isFlying = true;

        // ► ОТКЛЮЧАЕМ РЕЗИНКИ ПОСЛЕ ВЫСТРЕЛА
        _slingLinesController.ResetLineRenderer();
    }
    public void ResetSkull()
    {
        _skullRb.velocity = Vector2.zero;
        _skullRb.angularVelocity = 0;
        _skullRb.isKinematic = true;

        _skull.position = _center;
        _skull.rotation = Quaternion.identity;

        _isFlying = false;
    }

    private void PlayAudioClip()
    {
        AudioSource.PlayClipAtPoint(_audioClip, transform.position);
    }
}
