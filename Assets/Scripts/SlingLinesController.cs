using UnityEngine;

public class SlingLinesController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineFront;
    [SerializeField] private LineRenderer _lineBack;
    [SerializeField] private Transform _endLine;

    private Vector3 _originalFrontEnd;
    private Vector3 _originalBackEnd;
    private bool _isDrawing = false;

    private void Start()
    {
        SetupLineRenderer();
    }

    private void Update()
    {
        if (_isDrawing)
        {
            UpdateLineRenderer();
        }
    }

    private void SetupLineRenderer()
    {
        _lineFront.sortingLayerName = "Default";
        _lineFront.sortingOrder = 5;

        _lineBack.sortingLayerName = "Default";
        _lineBack.sortingOrder = 2;

        // сохраняем исходные конечные точки линий
        _originalFrontEnd = _lineFront.GetPosition(1);
        _originalBackEnd = _lineBack.GetPosition(1);

        _isDrawing = true;
    }

    private void UpdateLineRenderer()
    {
        _lineFront.SetPosition(1, _endLine.position);
        _lineBack.SetPosition(1, _endLine.position);
    }

    /// <summary>
    /// Вызывается после выстрела
    /// </summary>
    public void ResetLineRenderer()
    {
        _isDrawing = false;

        // возвращаем назад концы резинок
        _lineFront.SetPosition(1, _originalFrontEnd);
        _lineBack.SetPosition(1, _originalBackEnd);

        // полностью выключаем резинки (как в Angry Birds)
        _lineFront.enabled = false;
        _lineBack.enabled = false;
    }
    
    public void StartDrawing()
    {
        _lineFront.enabled = true;
        _lineBack.enabled = true;
        _isDrawing = true;
    }
}