using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMoving : StateGeneric {
  private SpringJoint spring ;
  public Movable block ;
  public StateMoving( TargetSphere _targetSphere  ) :base( _targetSphere ) {}

  public override Context Init( Context context, Context nextContext ) {
    targetSphere.SetEmission("Moving" ) ;

    block = context.selection.GetComponent<Movable>() ;
    try{
      block.SetUpSpring( context.hit, targetSphere.gameObject.GetComponent<Rigidbody>() ) ;
    } catch( Exception e) {
      nextContext.state = TargetSphere.State.Idle ;
      return End( context, nextContext ) ;
    }
    block.SetHiglight( .51f ) ;
    return Handle( context, nextContext ) ;
  }
  public override Context Handle( Context context, Context nextContext ) {
    if( context.hasHit = Cast( (int) TargetSphere.Mask.Target, out context.hit ) ) {
      context.selection = context.hit.transform.gameObject ;
      targetSphere.Move( context.hit.point ) ;
    }
    return Next( context, nextContext ) ;
  }
  public override Context Next( Context context, Context nextContext ) {
    //Let go of the LMB
    nextContext = context ;
    if( !Input.GetMouseButton(0)  ) {
      nextContext.state = TargetSphere.State.Idle ;
    }
    return End( context, nextContext ) ;
  }
  public override Context End( Context context, Context nextContext ) {
    if( context.state != nextContext.state ) {
      block.ResetHiglight( ) ;
      block.RemoveSpring() ;
      targetSphere.ResetPosition() ;
    }
    return nextContext ;
  }



}
