using UnityEngine;

namespace SM
{
  public class SMProjectile : MonoBehaviour
  {
    public Rigidbody2D rb;
    public Projectile projectile;

    [HideInInspector]
    public float speed, lifeTimeInSeconds;

    [HideInInspector]
    public int damage;

    [HideInInspector]
    public bool isPenetrating = false;

    [HideInInspector]
    public GameObject source;

    private Animator animator;
    private int onCollisionAnimatorHash = Animator.StringToHash("onCollision");

    void Start()
    {
      projectile.initialize(this.gameObject);
      rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
      animator = gameObject.GetComponent<Animator>();

      // Destroy these objects after awhile (Recycle some day)
      Invoke("onCollision", lifeTimeInSeconds);
    }

    //when a projectile collids with anything
    void OnTriggerEnter2D(Collider2D tOther)
    {
      GameObject tempTargetGameObject = tOther.gameObject;
      IDamageable tempTarget = tOther.GetComponent<IDamageable>();

      if (tempTarget == null || tempTargetGameObject == source)
      {
        return;
      }


      tempTarget.applyDamage(damage);

      //if it's not a penetrable round
      if (!isPenetrating)
      {
        onCollision();
      }
    }

    private void onCollision()
    {
      if (animator != null)
      {
        rb.velocity = Vector3.zero;
        animator.SetTrigger(onCollisionAnimatorHash);
      }
      else
      {
        Destroy(this.gameObject, .2f);
      }
    }

    public void onCollisionAnimationComplete()
    {
      Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
      CancelInvoke("onCollision");
    }
  }
}
