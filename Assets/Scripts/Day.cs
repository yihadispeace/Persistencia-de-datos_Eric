using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day : MonoBehaviour
{
    public float z;
   
    void Update()
    {

        //Que rote el transform alrededor en el eje Y hacía la derecha(ponerse el sol) y en el eje Z para la velocidad y que no se mueva en el eje X
        this.transform.RotateAround (Vector3.zero, Vector3.right, z);
        
    }


}
