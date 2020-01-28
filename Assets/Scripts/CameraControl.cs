using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] [Range(0f, 1f)] private float smoothMove;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool enableRotation;

    private Vector3 deltaOffset;
    private Vector3 currentVelocity;
    private float yRot = 0;
    private float xRot = 0;

    private void Awake()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enableRotation || Input.GetMouseButton(1))
        {
            yRot += Input.GetAxis("Mouse X") * rotationSpeed;
            xRot -= Input.GetAxis("Mouse Y") * rotationSpeed;
            xRot = Mathf.Clamp(xRot, -25, 75);

            Vector3 rotation = new Vector3(xRot, yRot, 0);

            transform.eulerAngles = rotation;
        }

        deltaOffset = transform.rotation * offset;

        Vector3 desiredPos = target.position + deltaOffset;

        transform.position = Vector3.Slerp(transform.position, desiredPos, smoothMove);

        //transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref currentVelocity, smoothMove);
        //transform.position = desiredPos;
    }
}