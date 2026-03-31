using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D pickupCollider;
    [SerializeField] private float pickupRadius = 1f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatHeight = 0.5f;

    private Vector3 startPosition;
    private bool wasPickedUp = false;

    void Start()
    {
        startPosition = transform.position;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (pickupCollider == null)
        {
            pickupCollider = GetComponent<CircleCollider2D>();
            if (pickupCollider == null)
            {
                pickupCollider = gameObject.AddComponent<CircleCollider2D>();
            }
        }

        pickupCollider.radius = pickupRadius;
        pickupCollider.isTrigger = true;

        if (itemData != null && itemData.itemSprite != null)
        {
            spriteRenderer.sprite = itemData.itemSprite;
        }

        Debug.Log($"📦 ItemPickup creado: {itemData.itemName}");
    }

    void Update()
    {
        // Animación de flotación
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
            PickUp(player.gameObject);
        }
    }

    /// <summary>
    /// Recoge el item
    /// </summary>
    public void PickUp(GameObject playerGO)
    {
        if (wasPickedUp)
            return;

        Inventory inventory = playerGO.GetComponent<Inventory>();
        if (inventory != null)
        {
            if (inventory.AddItem(itemData))
            {
                wasPickedUp = true;
                Debug.Log($"✅ {itemData.itemName} recogido");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("❌ Inventario lleno");
            }
        }
    }

    public ItemData GetItemData() => itemData;
    public void SetItemData(ItemData data) => itemData = data;
}