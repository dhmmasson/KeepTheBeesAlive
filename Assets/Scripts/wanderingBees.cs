using System.Collections;
using System.Collections.Generic;
using TMPro ; 
using UnityEngine;

public class wanderingBees : MonoBehaviour
{
  public TextMeshProUGUI scoreBoard ;
  int score ;
  void start() {
    score = 0 ;
  }
  void OnParticleCollision(GameObject block) {
    plant target = block.GetComponent<plant>() ;
    Debug.Log("Bee hit something") ;
    if( target ) {
      Debug.Log("Bee hit a flower") ;
      score++ ;
      scoreBoard.SetText("score " + score ) ;
    }
  }
}
