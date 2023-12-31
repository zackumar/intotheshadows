using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour, IInteractable
{

  public UnityEvent onInteract;

  void IInteractable.Interact()
  {
    onInteract.Invoke();
  }
}
