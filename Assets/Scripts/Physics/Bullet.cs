using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] SoundEffectsData _soundEffectsData;
    [SerializeField] float _bulletSpeed = 20f;
    [SerializeField] float dragCoefficient = 0.1f;

    Vector3 gravity = new Vector3(0, -9.81f, 0);
    Vector3 velocity;

    private WindController windController;

    private void Start()
    {
        velocity = transform.up * _bulletSpeed;
        windController = FindObjectOfType<WindController>();
    }

    private void Update()
    {
        if (PhysicsManager.Instance.Gravity)
        {
            velocity += gravity * Time.deltaTime;
        }

        // Apply wind force
        if (windController != null)
        {
            Vector3 windDirection= windController.GetWindDirection();
            Vector3 windDirectionWorld = transform.TransformDirection(windDirection);
            velocity += windDirectionWorld * Time.deltaTime;
        }

        ApplyDrag();
        transform.position += velocity * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, velocity.magnitude * Time.deltaTime))
        {
            int groundLayer = 6;
            int enemyLayer = 3;

            if (hit.collider.gameObject.layer == groundLayer)
            {
                velocity = Vector3.zero;
                Destroy(gameObject);
            }

            if (hit.collider.gameObject.layer == enemyLayer)
            {
                int enemyPopSound = 2;
                SoundManager.Instance.PlaySound(_soundEffectsData.soundEffects[enemyPopSound]);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void ApplyDrag()
    {
        // Simplified drag - proportional to the square of the velocity
        Vector3 drag = -dragCoefficient * velocity * velocity.magnitude;
        velocity += drag * Time.deltaTime;

        // For a more linear drag, you could simply use:
        // velocity -= dragCoefficient * velocity * Time.deltaTime;
    }
}