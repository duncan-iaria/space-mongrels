using UnityEngine;

namespace SNDL
{
	public class View : MonoBehaviour
	{
		public Camera cam;
		public Transform currentTarget;

		protected Animator animator;

		protected virtual void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public virtual void setTarget( Transform tTarget, bool isImmediate = false )
		{
			currentTarget = tTarget;

			//set the camera to center on the target immediately ( keeping current z though )
			if( isImmediate )
			{
				transform.position = new Vector3( tTarget.position.x, tTarget.position.y, transform.position.z );
			}
		}

		public virtual void setZoom( float tZoom )
		{
			cam.orthographicSize = tZoom;
		}

		//======================
		//	Animation
		//======================

		//play animation by string name
		public virtual void playAnimation( string tAnimation )
		{
			animator.Play( tAnimation );
		}

		//play animation by hash
		public virtual void playAnimation( int tAnimationHash )
		{
			animator.Play( tAnimationHash );
		}
	}
}
