using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHighlight : StateHandler
{
  public Movable block ;
  private bool newTarget ;
  private TargetWall targetWall ;
  public StateHighlight( TargetSphere _targetSphere, TargetWall _wall ) :base( _targetSphere ) {
    targetWall = _wall ;
  }

  public override Context Init( Context context, Context nextContext ) {
    targetSphere.SetEmission("Highlight" ) ;
    block = context.selection.GetComponent<Movable>() ;
    if( block == null ) {
      nextContext.state = TargetSphere.State.Idle ;
      return End( context, nextContext ) ;
    }
    block.SetHiglight( 0.2f );
    return Handle(context, nextContext )  ;
  }
  public override Context Handle( Context context, Context nextContext ) {
    targetSphere.Move( context.hit.point ) ;
    targetWall.Move( context.hit.point ) ;
    nextContext.hasHit = Cast( (int) ~( TargetSphere.Mask.Scene | TargetSphere.Mask.Target ), out nextContext.hit )   ;
    return Next( context, nextContext ) ;
  }
  public override Context Next( Context context, Context nextContext ) {
    // Does the ray intersect any objects excluding the background sceend
    if ( nextContext.hasHit ) {
      nextContext.selection = nextContext.hit.transform.gameObject ;
      if( Input.GetMouseButtonDown (0)  ) {
        Debug.Log( "Highlight > click ") ;
        nextContext.state = TargetSphere.State.Moving ;
      } else {
        nextContext.state = TargetSphere.State.Highlight ;
        if( nextContext.selection != context.selection ) {
          End( context, nextContext ) ;
          return Init( nextContext, nextContext ) ;
        }
        return nextContext ;
      }
    } else {
      nextContext.state = TargetSphere.State.Idle ;
    }
    return End( context, nextContext ) ;
  }
  void Reset() {}
  public override Context End( Context context, Context nextContext ) {
    block.ResetHiglight() ;
    targetSphere.ResetPosition() ;
    return nextContext ;
  }
}
