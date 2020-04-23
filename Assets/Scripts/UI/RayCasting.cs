using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    private GameObject selection = null ;
    private GameObject highlighted = null ;
    private State currentState ;


    public static int sceneLayer = 2 ;
    public static int targetLayer = 9 ;
    private int sceneMask  ;
    private int targetMask ;

    [ColorUsage(true, true)]
    public Color highlightColor ;
    [ColorUsage(true, true)]
    public Color moveColor ;

    private Material material ;
    public Rigidbody anchor ;
    private SpringJoint spring ;
    // Start is called before the first frame update
    public AudioSource click ;
    //
    void Start()
    {
      transform.position = Camera.main.transform.position ;
      sceneMask = ( 1 << sceneLayer ) ;
      targetMask = ( 1 << targetLayer ) ;
      material = GetComponent<Renderer>().material ;
      material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update() {
      switch( currentState ) {
        case State.idle: handleIdle() ; break ;
        case State.moving: handleMoving() ; break ;
      }
      HighlightWall() ;
    }

    void handleIdle( ) {
      //On left click on an object select it
      int layerMask = ~(sceneMask + targetMask);
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      // Does the ray intersect any objects excluding the background sceend
      if (Physics.Raycast(ray, out hit, 20f, layerMask)) {

        if ( Input.GetMouseButtonDown (0) ){
           getTargetPosition( hit.point ) ;
           currentState = State.moving ;
           selection = hit.transform.gameObject ;
           material.SetColor(Shader.PropertyToID("_EmissionColor"), moveColor);

           spring = selection.AddComponent( typeof( SpringJoint)) as SpringJoint ;
           spring.connectedBody = anchor ;
           spring.autoConfigureConnectedAnchor = false ;
           spring.anchor = hit.transform.InverseTransformPoint( hit.point ) ;
           spring.connectedAnchor = new Vector3(0,0,0) ;
           spring.maxDistance = .1f ;
           spring.spring = 10000 ;
           spring.damper = 2000 ;
           click.Play();
        } else {
          highlight( highlighted, 0 ) ;
          transform.position = hit.point   ;
          material.SetColor(Shader.PropertyToID("_EmissionColor"), highlightColor);
          highlighted = hit.transform.gameObject ;
          highlight( highlighted, 0.2f ) ;
        }
      } else {
        //unhilight element
        if( highlighted != null ) {
          highlight( highlighted, 0 ) ;
          transform.position = Camera.main.transform.position ;
          highlighted = null ;
        }
      }
    }

    void handleMoving() {
      if ( Input.GetMouseButton(0) ){
        getTargetPosition( transform.position ) ;
        highlight( selection, 0.51f ) ;
      } else {
        currentState = State.idle ;
        transform.position = Camera.main.transform.position ;
        highlight( selection, 0 ) ;
        Destroy(spring);
        selection = null ;
      }
    }

    Vector3 getTargetPosition( Vector3 point ) {
      //Layer 9 is the building area collider
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 20f, ~sceneMask)) {
        transform.position = hit.point ;
              return hit.point ;
      }
      return point ;
    }

    void highlight( GameObject block, float value ) {
      if( block != null )
      block.GetComponent<Renderer>().material.SetFloat(Shader.PropertyToID("_Vector1_37E861F"), value);
    }

    void HighlightWall() {
      float opacity = (currentState==State.idle)?0.1f:0.5f  ;
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit, 20f, targetMask)) {
        Material targetMaterial = hit.collider.gameObject.GetComponent<Renderer>().material ;
        if( targetMaterial.HasProperty("_Mouse_Coordinate")  ) {
          targetMaterial.SetVector("_Mouse_Coordinate", hit.textureCoord );
          targetMaterial.SetFloat("_Opacity", opacity );
        }
      }
    }

   public enum State
   {
     idle, moving
   }
}
