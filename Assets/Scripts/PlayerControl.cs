using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform relativeTransform;
    [SerializeField] private float rotationSpeed;
    public bool enableRotation;

    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private Vector3 horizontalMove;
    private Vector3 verticalMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (relativeTransform == null)
            relativeTransform = transform;
    }

    void Update()
    {
        #region Movement
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        horizontalMove = (Vector3.ProjectOnPlane(relativeTransform.forward, Vector3.up).normalized * vertical
                          + Vector3.ProjectOnPlane(relativeTransform.right, Vector3.up).normalized * horizontal) * (speed * Time.deltaTime);
        verticalMove = Vector3.up * rb.velocity.y;

        rb.velocity = horizontalMove + verticalMove;
        #endregion

        #region Rotation
        if (enableRotation)
        {
            float y = Input.GetAxis("Mouse X") * rotationSpeed;

            Vector3 rotation = new Vector3(0, -y, 0);

            transform.eulerAngles -= rotation;
        }
        #endregion

        #region Loading
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene("Level0");
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene("Level1");
        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (rb != null)
        {
            Gizmos.DrawRay(transform.position, rb.velocity);
        }
    }
}
