using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Collider2D groundCollider;

    private float cameraOrthographicSize;

    private void Start()
    {
        if (target == null || groundCollider == null)
        {
            Debug.LogError("Camera target or ground collider not set!");
            return;
        }

        cameraOrthographicSize = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        // Restrict the camera within the ground collider bounds
        Vector3 targetPosition = target.position;
        Vector3 clampedPosition = groundCollider.bounds.ClosestPoint(targetPosition);

        float cameraHalfWidth = cameraOrthographicSize * ((float)Screen.width / Screen.height);
        float cameraHalfHeight = cameraOrthographicSize;

        float minX = groundCollider.bounds.min.x + cameraHalfWidth;
        float maxX = groundCollider.bounds.max.x - cameraHalfWidth;

        float minY = groundCollider.bounds.min.y + cameraHalfHeight;
        float maxY = groundCollider.bounds.max.y - cameraHalfHeight;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = new Vector3(clampedPosition.x, clampedPosition.y, transform.position.z);
    }
}
