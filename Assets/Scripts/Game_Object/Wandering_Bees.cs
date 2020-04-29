using System.Collections;
using System.Collections.Generic;
using TMPro ;
using UnityEngine;

public class Wandering_Bees : MonoBehaviour
{
  public TextMeshProUGUI scoreBoard ;
  int score ;
  void start() {
    score = 0 ;
  }
  void OnParticleCollision(GameObject block) {
    Plant target = block.GetComponent<Plant>() ;
    if( target ) {
      score+= target.nbFlower  ;
      scoreBoard.SetText("score " + score ) ;
      StartCoroutine( lightCactus( block )) ;
    }
  }

  IEnumerator lightCactus( GameObject target ) {
    highlight( target, 0.1f ) ;
    yield return new WaitForSeconds(0.2f);
    highlight( target, 0 ) ;
  }

  void highlight( GameObject block, float value ) {
    if( block != null )
    block.GetComponent<Renderer>().material.SetFloat(Shader.PropertyToID("_Vector1_37E861F"), value);
  }
}
