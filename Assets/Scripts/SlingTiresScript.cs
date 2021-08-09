using UnityEngine;

public class SlingTiresScript : MonoBehaviour
{
    [SerializeField] private Transform Sling;
    [SerializeField] private Transform Seat;
    LineRenderer lineRenderer;
    private void Start()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, Sling.position);
        lineRenderer.SetPosition(1, Seat.position);
    }
}
