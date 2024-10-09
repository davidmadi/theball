using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MasterStart();
        
    }

    static void MasterStart(){

    }
    public static GameObject MainBall { get; set; }
    public static GameObject CurrentStep { get; set; }
    public static CameraFollow CameraFollow { get; set; }
    public static void SetCameraFollow(CameraFollow cameraFollow){
        CameraFollow = cameraFollow;
    }
    public static void SetMainBall(GameObject mainBall){
        MainBall = mainBall;
    }
    public static void SetCurrentStep(GameObject step) {
        CurrentStep = step;
        if (CameraFollow != null) {
            CameraFollow.UpdateFollow(step);
        }
    }
}
