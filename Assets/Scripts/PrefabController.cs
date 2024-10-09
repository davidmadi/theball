using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour
{
    public GameObject StepPrefab;
    public GameObject StepHorizontalPrefab;

    public GameObject GetStepBasedOn(StepType stepType) {
        switch(stepType){
            case StepType.Fixed:
            return this.StepPrefab;
        default:
            return this.StepHorizontalPrefab;
        }
    }

    public enum StepType {
        Fixed,
        Horizontal
    }

}
