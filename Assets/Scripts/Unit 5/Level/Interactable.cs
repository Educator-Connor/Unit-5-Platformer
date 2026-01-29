using System;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// This is a class that is bound to the player's interact action. When performed it sets a true false value. This is
/// just to show how we can use action in our Input Action System in other scripts.
/// </summary>
public class Interactable : MonoBehaviour
{
   private bool interactPressed;
   public bool switchPulled = false;
   private bool playerOnSwitch = false;
   private PlayerInputActions input;

   private void Awake()
   { 
      input = new PlayerInputActions();
      input.Player.Interact.performed += context => interactPressed = true;
   }
   private void OnEnable()
   {
      input.Player.Enable();
   }

   private void OnDisable()
   {
      input.Player.Disable();
   }
   private void Update()
   {
      if (interactPressed)
      { 
         Debug.Log("Switch Pulled");
         switchPulled = true; 
      }
      
      if (interactPressed && playerOnSwitch)
      { 
         Debug.Log("Switch Pulled");
         switchPulled = true; 
      }
      
      interactPressed = false;
   }

   public void OnTriggerEnter2D(Collider2D trig)
   {
      if (trig.CompareTag("Player"))
      {
         Debug.Log("Player on Switch");
         playerOnSwitch =  true;
      }
   }
   
   public void OnTriggerExit2D(Collider2D trig)
   {
      
      if (trig.CompareTag("Player"))
      {
         Debug.Log("Player Left Switch");
         playerOnSwitch = false;
      }
   }
}
