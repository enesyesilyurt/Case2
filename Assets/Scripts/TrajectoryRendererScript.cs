using UnityEngine;
namespace Trajectory
{
    public class TrajectoryRendererScript : MonoBehaviour
    {
        private Vector3 _lastLine;
        private LineRenderer _lineRenderer;
        public Vector3 LastLine => _lastLine;
        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lastLine = new Vector3(0, 1, 0);
        }
        public void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            Vector3[] points = new Vector3[100];
            _lineRenderer.positionCount = points.Length;
            for (int i = 0; i < points.Length; i++)
            {
                float time = i * 0.1f;
                points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
                if (points[i].y < -0.4)
                {
                    _lineRenderer.positionCount = i + 1;
                    break;
                }
                _lastLine = points[i];
            }
            _lineRenderer.SetPositions(points);
        }
    }
}