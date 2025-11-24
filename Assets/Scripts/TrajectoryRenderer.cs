using UnityEngine;
using System.Collections.Generic;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private GameObject _dotPrefab;
    [SerializeField] private Transform _dotsParent;
    [SerializeField] private int _dotsCount = 25;

    private List<Transform> _dots = new List<Transform>();
    private SlingshotController _controller;

    private void Awake()
    {
        _controller = GetComponent<SlingshotController>();

        for (int i = 0; i < _dotsCount; i++)
        {
            var dot = Instantiate(_dotPrefab, _dotsParent);
            _dots.Add(dot.transform);
        }
    }

    private void Update()
    {
        if (_controller.GrabbedSkull != null)
        {
            Draw();
        }
        else
        {
            Hide();
        }
    }

    private void Draw()
    {
        Vector3 start = _controller.GrabbedSkull.position;
        Vector2 velocity = _controller.SkullForce;

        float angle = Mathf.Atan2(velocity.y, velocity.x);
        float speed = velocity.magnitude;

        float t = 0;

        for (int i = 0; i < _dots.Count; i++)
        {
            float x = speed * t * Mathf.Cos(angle);
            float y = speed * t * Mathf.Sin(angle)
                      - (Physics2D.gravity.magnitude * t * t / 2);

            _dots[i].position = new Vector3(start.x + x, start.y + y, 0);

            t += 0.05f;
        }
    }

    private void Hide()
    {
        foreach (var dot in _dots)
            dot.position = new Vector3(9999, 9999, 0);
    }
}