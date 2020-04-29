using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class buildingBlocks : ScriptableObject
{
  public elementSettings[] elements ;

  [System.Serializable]
  public class elementSettings {
    public GameObject asset ;
    [Range(0,20)]
    public int repetition = 1 ;
    public Vector3 dropCenter = new Vector3(0,2,0);
    public float spreadRadius = 0 ;
  }

}
