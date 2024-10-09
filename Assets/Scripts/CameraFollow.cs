using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CameraFollow : MonoBehaviour
{
    public GameObject Follow;
    public GameObject Wall;
    private Vector3 cameraDistance;
    private Vector3 wallDistance;
    private MeshRenderer wallMeshRenderer;
    // Start is called before the first frame update
    private float followYOffset;
    private Vector3 cameraMoveDestination;
    private Vector2 wallMoveDestination;
    private int cameraMoveSpeed = 5;
    //////////// same memory position
    private float verticalMoveCameraY;
    void Start(){
      MasterController.SetCameraFollow(this);
    }
    void Awake()
    {
      this.cameraDistance = Follow.transform.position - this.transform.position;
      this.followYOffset =  this.transform.position.y - Follow.transform.position.y;
      this.wallDistance = this.Wall.transform.position - this.transform.position;
      this.wallMeshRenderer = Wall.GetComponent<MeshRenderer>();
      UpdateFollow(Follow);
    }

    // Update is called once per frame
    void Update()
    {
      if (this.transform.position.y < cameraMoveDestination.y){
        verticalMoveCameraY = (this.transform.position.y + cameraMoveSpeed * Time.deltaTime);
      } else if (this.transform.position.y > cameraMoveDestination.y) {
        verticalMoveCameraY = (this.transform.position.y - cameraMoveSpeed * Time.deltaTime);
      }
      if (math.abs(verticalMoveCameraY - this.transform.position.y) < .05){
        verticalMoveCameraY = Mathf.Round(verticalMoveCameraY * 100f) / 100f;
      }
      this.transform.position = new Vector3(this.transform.position.x, this.verticalMoveCameraY, this.transform.position.z);
      //Wall follow
      this.Wall.transform.position = this.transform.position + this.wallDistance;
      //Roll wall texture
      wallMeshRenderer.materials[0].mainTextureOffset = new Vector2(this.Wall.transform.position.x / -100, this.Wall.transform.position.y / -100);
    }

    public void UpdateFollow(GameObject toFollow) {
      this.Follow = toFollow;
      this.cameraMoveDestination = new Vector3(Follow.transform.position.x, Follow.transform.position.y + followYOffset, this.transform.position.z);
      this.wallMoveDestination = new Vector2(this.transform.position.x / -100, Follow.transform.position.y / -100);
    } 
}
