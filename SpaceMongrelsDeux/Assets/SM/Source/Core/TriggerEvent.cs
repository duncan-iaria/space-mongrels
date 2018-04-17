using UnityEngine;
using UnityEngine.Events;

//To be applied to trigger colliders - allowing them to trigger events
public class TriggerEvent : MonoBehaviour
{
	public bool isEntryTrigger;
	public UnityEvent onTriggerEntry;

	public bool isStayTrigger;
	public UnityEvent onTriggerStay;

	public bool isExitTrigger;
	public UnityEvent onTriggerExit;

	protected void OnTriggerEnter( Collider other )
	{
		if( isEntryTrigger && onTriggerEntry != null )
		{
			onTriggerEntry.Invoke();
		}

	}

	protected void OnTriggerStay( Collider other )
	{
		if( isStayTrigger && onTriggerStay != null )
		{
			onTriggerStay.Invoke();
		}
	}

	protected void OnTriggerExit( Collider other )
	{
		if( isExitTrigger && onTriggerExit != null )
		{
			onTriggerExit.Invoke();
		}
	}
}
