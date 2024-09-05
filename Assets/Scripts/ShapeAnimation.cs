using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShapeAnimation : MonoBehaviour
{
        public void Update()
        {
            transform.RotateAround(transform.position, Vector3.forward, 50 * Time.deltaTime);
        }
}