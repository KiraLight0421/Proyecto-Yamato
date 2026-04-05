using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatHeight = 0.3f;
    [SerializeField] private float pickupRadius = 0.8f;

    private Vector3 startPosition;
    private CircleCollider2D pickupCollider;
    private bool wasPickedUp = false;

    void Start()
    {
        startPosition = transform.position;

        pickupCollider = GetComponent<CircleCollider2D>();
        if (pickupCollider == null)
        {
            pickupCollider = gameObject.AddComponent<CircleCollider2D>();
        }

        pickupCollider.radius = pickupRadius;
        pickupCollider.isTrigger = true;

        Debug.Log("❤️ HeartPickup listo en " + transform.position);
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (wasPickedUp)
            return;

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            HealthSystem healthSystem = player.GetComponent<HealthSystem>();
            if (healthSystem == null)
            {
                Debug.LogWarning("⚠️ El jugador no tiene HealthSystem");
                return;
            }

            wasPickedUp = true;
            healthSystem.IncreaseMaxHealth(1);
            Debug.Log("💖 Corazón recogido. Vida máxima aumentada en 1.");
            Destroy(gameObject);
        }
    }
}
