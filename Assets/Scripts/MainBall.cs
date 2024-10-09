using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainBall : MonoBehaviour
{
  private Rigidbody ballRigidBody;
  public LayerMask FloorMask;
  private Vector3 initialPosition;
  private Quaternion initialRotation;
  private Jumper jumper;
  private PrefabController prefabController;
  void Start(){
    MasterController.SetMainBall(this.gameObject);
    this.ballRigidBody = this.GetComponent<Rigidbody>();
    this.jumper = this.GetComponent<Jumper>();
    this.prefabController = this.GetComponent<PrefabController>();
    this.initialPosition = this.ballRigidBody.position;
    this.initialRotation = this.ballRigidBody.transform.rotation;
  }

  void Update(){

    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,0);
    this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
    if (Math.Abs(this.ballRigidBody.velocity.y) < 0.5){
      var colliders = Physics.OverlapSphere(this.ballRigidBody.transform.position, 0.5f, FloorMask);
      foreach(var collider in colliders) {
        if (collider.tag == "Step") {
          var stepController = collider.GetComponent<StepController>();
          if (stepController != null){
            stepController.TriggerNextStep(this.prefabController);
            this.jumper.Ground();
            MasterController.SetCurrentStep(collider.gameObject);
          }
        } else if (collider.tag == "Ground") {
          this.transform.position = new Vector3(this.initialPosition.x, this.initialPosition.y, this.initialPosition.z);
          this.transform.rotation = new Quaternion(this.initialRotation.x, this.initialRotation.y, this.initialRotation.z, this.initialRotation.w);
          this.jumper.Ground();
        }
      }
    }
  }
  
}
