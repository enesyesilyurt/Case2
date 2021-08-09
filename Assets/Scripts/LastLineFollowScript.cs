using UnityEngine;
using Trajectory;
public class LastLineFollowScript : MonoBehaviour
{
    [SerializeField] private TrajectoryRendererScript _trajectory;
    void Update()
    {
        Vector3 desiredPosition = new Vector3(_trajectory.LastLine.x, Mathf.Clamp(_trajectory.LastLine.y, -0.1f, 0.1f), Mathf.Clamp(_trajectory.LastLine.z, 1, float.PositiveInfinity));
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.03f);
        transform.position = smoothedPosition;
    }
}
