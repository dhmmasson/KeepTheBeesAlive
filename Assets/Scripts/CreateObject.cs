using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public int width = 10;
    public int height = 4;
    public buildingBlocks level ;
    public void onStartLevel() {
      createObject() ;
    }
    void createObject() {
      foreach (buildingBlocks.elementSettings element in level.elements) {
        for( int i = 0 ; i < element.repetition ; i++ ){

          float x = Random.Range(-2, 2) ;
          float y = 1f+i ;

          Instantiate(element.asset, element.dropCenter + new Vector3(x,y,-0.5f), Quaternion.identity);
        }
      }
    }

}
