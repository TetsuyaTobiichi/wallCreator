using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public Collision collisinObject;
    public float wallHeight = 2f; 

    public void Initialize(Vector3 start, Vector3 end)
    {
        startPoint = start;
        endPoint = end;
        UpdateWall();
    }

    private void UpdateWall()
    {
        // обновление длины и формы стены
        transform.position = (startPoint + endPoint) / 2;
        transform.LookAt(endPoint);
        float length = Vector3.Distance(startPoint, endPoint);
        transform.localScale = new Vector3(0.1f, wallHeight, length);
    }

    void OnCollisionEnter(Collision col)
    {   
        if(col.gameObject==this.gameObject){Debug.Log("и тут бываю"); return;}
        //collisinObject=col;
        return;
    }
}
