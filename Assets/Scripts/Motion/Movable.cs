using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
  private Material material ;
  private SpringJoint spring ;
  public Dictionary< string, Color> stateColor ;



  void Awake() {
    material = GetComponent<Renderer>().material ;
    material.EnableKeyword("_EMISSION");
  }
  void Start() {
  }
  public void SetHiglight( float value ) {
    material.SetFloat(Shader.PropertyToID("_Vector1_37E861F"), value);
  }
  public void ResetHiglight(  ) {
    SetHiglight( 0 ) ;
  }
  public void SetUpSpring( RaycastHit hit, Rigidbody targetSphere  ) {
    spring = gameObject.AddComponent( typeof( SpringJoint)) as SpringJoint ;
    spring.maxDistance = .1f ;
    spring.spring = 10000 ;
    spring.damper = 2000 ;
    spring.connectedBody = targetSphere ;
    spring.autoConfigureConnectedAnchor = false ;
    spring.anchor = hit.transform.InverseTransformPoint( hit.point ) ;
    spring.connectedAnchor = new Vector3(0,0,0) ;
  }
  public void RemoveSpring( ) {
    Destroy( spring ) ;
  }
}
