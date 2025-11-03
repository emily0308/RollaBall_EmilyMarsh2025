using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;

    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}


public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform lookAtTransform;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float maximumOrbitDistance = 10f;
    [SerializeField] private float minimumOrbitDistance = 2f;

    private float orbitRadius = 5f;

    private bool isOrbitCameraActive = false;
    private float mouseX = 0f;
    private float mouseY = 0f;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            isOrbitCameraActive = !isOrbitCameraActive;

            if (!isOrbitCameraActive)
            {
                transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                transform.rotation = Quaternion.identity;
            }

            else
            {
                orbitRadius = 5f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            transform.LookAt(lookAtTransform);

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
        }

        orbitRadius -= Input.mouseScrollDelta.y / sensitivity;
        orbitRadius = Mathf.Clamp(orbitRadius, minimumOrbitDistance, maximumOrbitDistance);

        transform.position = lookAtTransform.position - transform.forward * orbitRadius;
    }
}