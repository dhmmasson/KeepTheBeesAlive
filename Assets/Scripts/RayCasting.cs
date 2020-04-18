using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    private GameObject selection = null ;
    private State currentState ;


    public static int sceneLayer = 2 ;
    public static int targetLayer = 9 ;
    private int sceneMask  ;
    private int targetMask ;

    // Start is called before the first frame update
    void Start()
    {
      transform.position = Camera.main.transform.position ;
      sceneMask = ( 1 << sceneLayer ) ;
      targetMask = ( 1 << targetLayer ) ;
    }

    // Update is called once per frame
    void Update() {
      switch( currentState ) {
        case State.idle: handleIdle() ; break ;
        case State.moving: handleMoving() ; break ;
      }
    }

    void handleIdle( ) {
      //On left click on an object select it
      if ( Input.GetMouseButtonDown (0)){
        int layerMask = ~(sceneMask + targetMask);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the background sceend
        if (Physics.Raycast(ray, out hit, 20f, layerMask))
        {
           Debug.Log("You selected the " + hit.transform.name);
           Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow,2f, true);
           getTargetPosition( hit.point ) ;
           currentState = State.moving ;
           selection = hit.transform.gameObject ;
        }
      }
    }

    void handleMoving() {
      if ( Input.GetMouseButton(0) ){
        getTargetPosition( transform.position ) ;
      } else {
        currentState = State.idle ;
          transform.position = Camera.main.transform.position ;
      }
    }

    Vector3 getTargetPosition( Vector3 point ) {
      //Layer 9 is the building area collider

      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      // Does the ray intersect any objects excluding the player layer
      if (Physics.Raycast(ray, out hit, 20f, ~sceneMask)) {
        Debug.Log("You are pointing at" + hit.transform.name);
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red,0.1f, true);
        transform.position = hit.point ;
        return hit.point ;
      }
      return point ;
    }

   public enum State
   {
     idle, moving
   }
}
