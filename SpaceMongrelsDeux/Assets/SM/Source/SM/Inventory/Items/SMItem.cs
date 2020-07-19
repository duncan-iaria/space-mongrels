using UnityEngine;

namespace SM
{
  public class SMItem : MonoBehaviour
  {
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
    public float dropSpeed = 2f;
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
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.gameSprite;
        gameObject.name = item.displayName;
      }
    }

    // Gives the items a little direction/push and spin
    public void drop()
    {
      transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
      rb.AddForce(transform.up * dropSpeed);
      rb.AddTorque(dropSpeed * .25f);
    }
  }

}
