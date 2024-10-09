using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainCollideDown : MonoBehaviour
{
    private GameObject mainObject;
    private Rigidbody mainRigibody;
    private BoxCollider[] boxColliders;
    // Start is called before the first frame update
    void Awake()
    {
        mainObject = GameObject.FindWithTag("MainBall");
        mainRigibody = mainObject.GetComponent<Rigidbody>();
        boxColliders = this.GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // UnityEngine.Debug.Log(mainRigibody.velocity.y.ToString());
        if (mainRigibody.velocity.y > 0){
            foreach(var boxCollider in this.boxColliders){
                boxCollider.enabled = false;
            }
        } else {
            foreach(var boxCollider in this.boxColliders){
                boxCollider.enabled = true;
            }
        }
    }
}
