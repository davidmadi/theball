using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using UnityEngine;

public class StepController : MonoBehaviour
{
    public bool NextStepTriggered {get; private set;}
    private GameObject thisStep;
    void Awake(){
        this.thisStep = this.gameObject;
        if (StepQueue.Queue.Count == 0) { //Starting
            StepQueue.Queue.Add(this.thisStep);
            MasterController.SetCurrentStep(this.thisStep);
        }
        // StepQueue.Queue.Add(this.thisStep);
    }
    public void TriggerNextStep(PrefabController prefabController)
    {
        if (this.NextStepTriggered) return;
        MasterController.SetCurrentStep(thisStep);
        this.NextStepTriggered = true;
        // this.gameObject.layer = 0;

        // StepQueue.AddStep(this.gameObject, prefab);
        StepQueue.AddStep(prefabController);
    }
}

public static class StepQueue
{
    private static float distanceToNext = 3.5f;
    public static List<GameObject> Queue = new List<GameObject>();
    // public static GameObject CurrentStep;
    private static Vector3 freshPosition = new Vector3(0, 0, 0);

    private static List<PrefabController.StepType> phaseSteps = new List<PrefabController.StepType>(){
        PrefabController.StepType.Fixed,
        PrefabController.StepType.Fixed,
        PrefabController.StepType.Fixed,
        PrefabController.StepType.Horizontal,
        PrefabController.StepType.Fixed,
        PrefabController.StepType.Fixed,
        PrefabController.StepType.Horizontal,
        PrefabController.StepType.Fixed,
        PrefabController.StepType.Horizontal,
        PrefabController.StepType.Fixed,
    };

    public static void AddStep(PrefabController prefabController) {
        var lastStep = StepQueue.Queue.Last();
        Vector3 lastPosition = lastStep.transform.position;
        var thirdBeforeLast = Queue.Count - 3;
        var count = 0;
        while (count < 3 && (thirdBeforeLast < 0 || Queue.IndexOf(MasterController.CurrentStep) > thirdBeforeLast)){ //there might be a better way
            var newPosition = new Vector3(freshPosition.x, lastPosition.y + distanceToNext, freshPosition.z);
            addStep(prefabController, newPosition, lastStep.transform.rotation);
            lastPosition = newPosition;
            thirdBeforeLast = Queue.Count - 3;
            count++;
        }
    }

    private static void addStep(PrefabController prefabController, Vector3 position, Quaternion rotation) {
        var prefab = prefabController.StepPrefab;
        if (StepQueue.Queue.Count < phaseSteps.Count){
            prefab = prefabController.GetStepBasedOn(phaseSteps[StepQueue.Queue.Count-1]);
        }

        var newStep = Object.Instantiate(prefab, position, rotation);
        if (newStep == null){
            Debug.Log("Not able to create new step");
        } 
        else {
            Queue.Add(newStep);
        }
    }
}