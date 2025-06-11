using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JUTPS.VehicleSystem
{
    public class JuVehicleAnimatedSteerWheel : MonoBehaviour
    {
        public Transform SteeringWheel;
        public WheelCollider ReferenceWheel;

        void Start()
        {   
            CreateSteeringWheelRotationPivot(SteeringWheel);
        }

        void Update()
        {
            SteeringWheel.transform.localEulerAngles = SteeringWheelRotation(SteeringWheel, ReferenceWheel).eulerAngles;
        }

        private void CreateSteeringWheelRotationPivot(Transform SteeringWheel)
        {
            //Create a pivot and setup transformations
            GameObject SteeringWheelRotationAxisFix = new GameObject("Steering Wheel");
            SteeringWheelRotationAxisFix.transform.position = SteeringWheel.position;
            SteeringWheelRotationAxisFix.transform.rotation = SteeringWheel.rotation;
            SteeringWheelRotationAxisFix.transform.parent = SteeringWheel.transform.parent;

            //Steering wheel parenting with the pivot
            SteeringWheel.transform.parent = SteeringWheelRotationAxisFix.transform;
        }

        private Quaternion SteeringWheelRotation(Transform SteeringWheel, WheelCollider WheelToGetSteerAngle, float MultiplySteeringWheelRotation = 1)
        {
            //var SteeringWheelRotation = MaxSteerAngle * (SmoothedHorizontalValue ? _smoothedHorizontal : _horizontal);
            var SteeringWheelRotation = MultiplySteeringWheelRotation * WheelToGetSteerAngle.steerAngle;
            Vector3 RotationEuler = new Vector3(SteeringWheel.localEulerAngles.x, SteeringWheelRotation, SteeringWheel.transform.localEulerAngles.x);
            return Quaternion.Euler(RotationEuler);
        }
    }
}