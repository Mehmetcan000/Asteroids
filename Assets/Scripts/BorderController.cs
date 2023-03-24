 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum Axis
 {
  x,
  y
 }
 
public class BorderController : MonoBehaviour
{
 [SerializeField] private Axis _axis;

 private void OnTriggerEnter2D(Collider2D col)
 {
  var teleport = col.GetComponent<ITeleportable>();
  teleport.OnTeleport();
  

     var current = col.transform.position;
     if (_axis == Axis.x)
     {
      current.x *= -1;
     }
     else
     {
      current.y *= -1;
     }

     col.transform.position = current;
 }
 
 
}
