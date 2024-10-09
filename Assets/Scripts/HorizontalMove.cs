using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class HorizontalMove : MonoBehaviour
{
    public float Speed = 2;
    private float Direction = 1;
    // Update is called once per frame
    void Update()
    {
        var newX = this.transform.position.x + (this.Speed * Time.deltaTime * this.Direction);
        this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);

        if ((newX + this.transform.localScale.x) >= 10) {
            this.Direction = -1;
        } else if (newX <= -6 ){
            this.Direction = 1;
        }
        
    }
}
