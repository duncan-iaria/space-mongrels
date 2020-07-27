using System.Collections.Generic;
using UnityEngine;

namespace SM
{
  public class SMUIMessagesController : MonoBehaviour
  {
    public GameObject messageUIPrefab;
    public GameObject messageUIPanel;

    public int messageLimit = 5;
    public float messageDuration = 3f;

    public Queue<SMUIMessage> messageQueue = new Queue<SMUIMessage>();

    // plain ole string variant
    public void showMessage(string tMessage)
    {
      // Checke if the message already exists
      if (messageQueue.Count > 0)
      {
        foreach (SMUIMessage tempMessage in messageQueue)
        {
          if (tempMessage.Message == tMessage)
          {
            // Cancel adding if the same message already exists
            return;
          }
        }
      }

      // Add a new message
      SMUIMessage tempMessageUi = createMessage();
      tempMessageUi.Message = tMessage;
      messageQueue.Enqueue(tempMessageUi);

      if (messageQueue.Count > 5)
      {
        // Remove oldest message from queue ASAP
        removeOldestMessage();
      }
      else
      {
        Invoke("removeOldestMessage", messageDuration);
      }
    }

    private SMUIMessage createMessage()
    {
      GameObject tempMessageObject = Instantiate(messageUIPrefab);
      tempMessageObject.transform.SetParent(messageUIPanel.transform, false);
      SMUIMessage tempMessageUi = tempMessageObject.GetComponent<SMUIMessage>();

      return tempMessageUi;
    }


    private void removeOldestMessage()
    {
      SMUIMessage tempMessageUi = messageQueue.Dequeue();
      tempMessageUi.remove();
    }

    public void clearMessageUi(bool isInEditMode = false)
    {
      // Clear message queue
      messageQueue = new Queue<SMUIMessage>();

      // Clear current UI
      SMUIMessage[] allUiItems = messageUIPanel.GetComponentsInChildren<SMUIMessage>(true);

      foreach (SMUIMessage uiMessage in allUiItems)
      {
        if (isInEditMode)
        {
          DestroyImmediate(uiMessage.gameObject);
        }
        else
        {
          Destroy(uiMessage.gameObject);
        }
      }
    }
  }
}
