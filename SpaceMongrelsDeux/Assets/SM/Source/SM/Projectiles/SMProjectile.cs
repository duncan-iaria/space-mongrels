using UnityEngine;

namespace SM
{
    public class SMProjectile : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Projectile projectile;
        public float speed;
        public int damage;
        public bool isPenetrating = false;

        [HideInInspector]
        public GameObject source;

        void Start()
        {
            projectile.initialize(this.gameObject);
            rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        //when a projectile collids with anything
        void OnTriggerEnter2D(Collider2D tOther)
        {
            GameObject tempTargetGO = tOther.gameObject;
            IDamageable tempTarget = tOther.GetComponent<IDamageable>();

            if (tempTarget != null && tempTargetGO != source)
            {
                tempTarget.applyDamage(damage);
            }
            else
            {
                return;
            }

            //if it's not a penetrable round
            if (!isPenetrating)
            {
                rb.velocity = Vector3.zero;
                Destroy(this.gameObject, .2f);
            }
        }
    }
}
