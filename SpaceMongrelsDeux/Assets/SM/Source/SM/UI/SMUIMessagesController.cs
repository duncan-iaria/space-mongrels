using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SNDL;

namespace SM
{
  public class SMUIMessagesController : MonoBehaviour
  {
    public GameObject messageUIPrefab;
    public GameObject messageUIPanel;

    public int messageLimit = 5;
    public float messageDuration = 5f;
    // public UnityEvent onShowMessage;
    public Queue<string> messageQueue = new Queue<string>();

    public void showMessage(StringVariable tMessage)
    {
      messageQueue.Enqueue(tMessage.value);
      buildMessageUI();
    }

    // plain ole string variant
    public void showMessage(string tMessage)
    {
      // GameObject SMUIMessage = Instantiate(messageUIPrefab);
      Debug.Log("showing message...");
      messageQueue.Enqueue(tMessage);
      buildMessageUI();

      if (messageQueue.Count > 5)
      {
        removeOldestMessage();
      }
      else
      {
        Invoke("removeOldestMessage", messageDuration);
      }
    }


    private void removeOldestMessage()
    {
      messageQueue.Dequeue();
      // buildMessageUI();
    }

    private void buildMessageUI()
    {
      // clearMessageUi();
      drawMessageUi();

      Debug.Log("rebuilding UI");
    }

    private void clearMessageUi(bool isInEditMode = false)
    {
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

    private void drawMessageUi()
    {
      foreach (string tempMessage in messageQueue)
      {
        GameObject tempMessageObject = Instantiate(messageUIPrefab);
        tempMessageObject.transform.SetParent(messageUIPanel.transform, false);
        SMUIMessage tempMessageUi = tempMessageObject.GetComponent<SMUIMessage>();

        if (tempMessageUi)
        {
          tempMessageUi.Message = tempMessage;
        }
      }
    }
  }
}
