using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler {
  public TargetSphere targetSphere ;
  public StateHandler( TargetSphere _targetSphere  ) {
    targetSphere = _targetSphere ;
  }

  public virtual Context Init( Context context, Context nextContext ) {
    return Handle( context, nextContext ) ;
  }
  public virtual Context Handle( Context context, Context nextContext ) {
    return Next( context, nextContext ) ;
  }
  public virtual Context Next( Context context, Context nextContext ) {
    return End( context, nextContext ) ;
  }
  public virtual Context End( Context context, Context nextContext ) {
    return nextContext ;
  }

  protected bool Cast( int layerMask, out RaycastHit hit ) {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition) ;
    return Physics.Raycast(ray, out hit, 20f, layerMask) ;
  }

  public struct Context {
    public TargetSphere.State state ;
    public GameObject selection ;
    public RaycastHit hit ;
    public bool hasHit ;
  }
}
