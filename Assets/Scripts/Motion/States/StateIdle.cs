using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : StateGeneric
{
  public StateIdle( TargetSphere _targetSphere  ) :base( _targetSphere ) {
  }

  public override Context Handle( Context context, Context nextContext ) {
    if( context.hasHit = Cast( (int) ~( TargetSphere.Mask.Scene | TargetSphere.Mask.Target ), out context.hit ) ) {
      context.selection = context.hit.transform.gameObject ;
    }
    return Next( context, nextContext ) ;
  }
  public override Context Next( Context context, Context nextContext ) {
    // Does the ray intersect any objects excluding the background sceend
    if ( context.hasHit ) {
      if( Input.GetMouseButtonDown (0)  ) {
        Debug.Log( "Idle > click ") ;
        nextContext.state = TargetSphere.State.Moving ;
        nextContext.selection =   context.selection ;
      } else {
        nextContext.state = TargetSphere.State.Highlight ;
        nextContext.selection = context.selection ;
      }
    } else {
      nextContext.state = TargetSphere.State.Idle ;
    }
    return End( context, nextContext ) ;
  }
}
