using UnityEngine;
using UnityEngine.Events;

//To be applied to trigger colliders - allowing them to trigger events
public class TriggerEvent2D : MonoBehaviour
{
	public bool isEntryTrigger;
	public UnityEvent onTriggerEntry;

	public bool isStayTrigger;
	public UnityEvent onTriggerStay;

	public bool isExitTrigger;
	public UnityEvent onTriggerExit;

	protected void OnTriggerEnter2D( Collider2D other )
	{
		if( isEntryTrigger && onTriggerEntry != null )
		{
			onTriggerEntry.Invoke();
		}

	}

	protected void OnTriggerStay2D( Collider2D other )
	{
		if( isStayTrigger && onTriggerStay != null )
		{
			onTriggerStay.Invoke();
		}
	}

	protected void OnTriggerExit2D( Collider2D other )
	{
		if( isExitTrigger && onTriggerExit != null )
		{
			onTriggerExit.Invoke();
		}
	}
}
