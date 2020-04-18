using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public int width = 10;
    public int height = 4;
    public buildingBlocks level ;
    void Start()
    {
      foreach (buildingBlocks.elementSettings element in level.elements) {
        for( int i = 0 ; i < element.repetition ; i++ ){
          float x = Random.Range(-2, 2) ;
          float y = Random.Range(1, 4) ;
          Instantiate(element.asset, new Vector3(x,y,0), Quaternion.identity);
        }
      }
    }


    // Update is called once per frame
    void Update()
    {

    }


}
