using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SNDL;

namespace SM
{
  public class SMUIMessagesController : MonoBehaviour
  {
    public UnityEvent onShowMessage;
    public Queue<string> messageQueue;

    public void showMessage(StringVariable tMessage)
    {
      messageQueue.Enqueue(tMessage.value);
      rebuildMessageUi();
    }

    // plain ole string variant
    public void showMessage(string tMessage)
    {
      messageQueue.Enqueue(tMessage);
      rebuildMessageUi();
    }


    private void removeOldestMessage()
    {
      messageQueue.Dequeue();
      rebuildMessageUi();
    }

    private void rebuildMessageUi()
    {
      Debug.Log("rebuilding UI");
    }
  }
}
