using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour
{
  private Material material ;
  public NamedColor[] colors ;
  public Dictionary< string, Color> stateColor ;

  public Dictionary < State, StateHandler > handlers ;
  public StateHandler.Context context ;
  private bool newState ;
  public TargetWall targetWall ;
  void Awake() {
    material = GetComponent<Renderer>().material ;
    material.EnableKeyword("_EMISSION");
    stateColor = new Dictionary< string, Color>();
    foreach (NamedColor color in colors) {
      stateColor.Add( color.state, color.color ) ;
    }

    material = GetComponent<Renderer>().material ;
    material.EnableKeyword("_EMISSION");

    handlers = new Dictionary< State, StateHandler >() ;
    handlers.Add( State.Idle, new StateIdle( this ) ) ;
    handlers.Add( State.Moving, new StateMoving( this ) ) ;
    handlers.Add( State.Highlight, new StateHighlight( this, targetWall ) ) ;
  }
  void Start() {
    transform.position = Camera.main.transform.position ;
    context.state = State.Idle ;
    newState = true ;
    ResetPosition( ) ;
  }

  // Update is called once per frame
  void Update() {
    StateHandler.Context nextContext ;
    if( newState ) nextContext = handlers[ context.state ].Init( context, context ) ;
    else nextContext = handlers[ context.state ].Handle( context, context ) ;
    newState = ( nextContext.state != context.state ) ;
    context = nextContext ;

  }

  public void SetEmission( string stateName ) {
    material.SetColor(Shader.PropertyToID("_EmissionColor"), stateColor[ stateName ] );
  }
  public void ResetPosition( ) {
    transform.position = Camera.main.transform.position ;
  }
  public void Move( Vector3 position ) {
    transform.position = position ;
  }

  [System.Serializable]
  public struct NamedColor {
    public string state ;
    [ColorUsage(true, true)]
    public Color color ;
  }
  public enum State
  { Idle
  , Moving
  , Highlight
  }

  [System.Flags]
  public enum Mask {
    Scene = ( 1 << 2 )
  , Target = ( 1 << 9 )
  }
}
