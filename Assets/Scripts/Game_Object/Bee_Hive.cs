using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Hive : MonoBehaviour
{
    public GameObject normalBee ;
    public GameObject angryBee ;
    public SpringJoint joint ;
    public AudioSource BeeSound ;
    public int magnitude = 20 ;
    public void SetAnger() {
      BeeSound.Play();
      normalBee.SetActive( false ) ;
      angryBee.SetActive( true ) ;
    }

    public void setCalm() {
      normalBee.SetActive( true ) ;
      angryBee.SetActive( false ) ;
    }

    void FixedUpdate() {
      if( joint.currentForce.magnitude > magnitude ){
          StartCoroutine(angerForSomeTime(5)) ;
      }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2) {

            StartCoroutine(angerForSomeTime(5)) ;
        }

    }

    IEnumerator angerForSomeTime( int time )
    {
        SetAnger() ;
        yield return new WaitForSeconds(time);
        setCalm() ;
    }
}
