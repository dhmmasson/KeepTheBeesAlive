using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWall : MonoBehaviour
{
  Material material ;
  public TargetSphere targetSphere ;
  void Awake() {


  }
  void Start() {
    material = gameObject.GetComponent<Renderer>().material  ;
  }
  // Update is called once per frame
  void Update() {
    HighlightWall() ;
    var d = Input.GetAxis("Mouse ScrollWheel") ;
    dMove( d ) ;
  }

  bool Cast( int layerMask, out RaycastHit hit ) {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
    return Physics.Raycast(ray, out hit, 20f, layerMask) ;
  }
  void HighlightWall() {
    float opacity = (targetSphere.context.state == TargetSphere.State.Idle)?0.1f:0.5f  ;
    RaycastHit hit;
    if (Cast( (int) TargetSphere.Mask.Target, out hit ) ) {
      Material targetMaterial = hit.collider.gameObject.GetComponent<Renderer>().material ;
      if( targetMaterial.HasProperty("_Mouse_Coordinate")  ) {
        targetMaterial.SetVector("_Mouse_Coordinate", hit.textureCoord );
        targetMaterial.SetFloat("_Opacity", opacity );
      }
    }
  }

  public void Move( Vector3 position ) {
    dMove( position.z - transform.position.z ) ;
  }
  public void dMove( float z ) {
    transform.position = transform.position + new Vector3( 0,0,z) ;
  }


}
