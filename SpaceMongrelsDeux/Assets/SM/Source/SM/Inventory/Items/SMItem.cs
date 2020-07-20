using UnityEngine;
using SNDL;

namespace SM
{
  public class SMItem : MonoBehaviour
  {
    [SerializeField]
    private InventoryItem item;
    public InventoryItem Item
    {
      get { return item; }
      set
      {
        item = value;
        init();
      }
    }
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public FloatVariable dropSpeed;
    public FloatVariable pullSpeed;

    // Pull probably not right word... but
    // It relates to gravitating toward the player
    private Transform pullTarget;
    public void Start()
    {
      init();
    }

    public void init()
    {
      if (item)
      {
        // Set mass
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.mass = item.itemMass;

        // Set art
        if (spriteRenderer == null)
        {
          spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        spriteRenderer.sprite = item.gameSprite;
        gameObject.name = item.displayName;
      }
    }

    // Gives the items a little direction/push and spin
    public void onDrop()
    {
      transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
      rb.AddForce(transform.up * dropSpeed.value);
      rb.AddTorque(dropSpeed.value * .25f);
    }

    public void collect()
    {
      // function when the item is collected
    }

    // ===============
    // Collisions
    // ===============
    void OnTriggerEnter2D(Collider2D tOtherCollider)
    {
      if (isColliderPlayer(tOtherCollider))
      {
        pullTarget = tOtherCollider.transform;
      }
    }

    void OnTriggerExit2D(Collider2D tOtherCollider)
    {
      if (isColliderPlayer(tOtherCollider))
      {
        pullTarget = null;
      }
    }

    void OnCollisionEnter2D(Collision2D tOtherCollision)
    {
      if (isColliderPlayer(tOtherCollision.collider))
      {
        SMPawnShip tempPawn = tOtherCollision.collider.GetComponent<SMPawnShip>();

        // Add to inventory
        tempPawn.inventory.addToInventory(Item);

        // Cleanup
        Destroy(this.gameObject);
      }
    }

    private bool isColliderPlayer(Collider2D tOtherCollider)
    {
      SMPawnShip tempPawn = tOtherCollider.GetComponent<SMPawnShip>();
      if (tempPawn != null && tempPawn.isCurrentPlayerPawn)
      {
        return true;
      }

      return false;
    }

    void FixedUpdate()
    {
      if (pullTarget != null)
      {
        rb.AddForce((pullTarget.position - transform.position).normalized * pullSpeed.value * Time.smoothDeltaTime);
      }
    }
  }
}
