using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosiiton;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 6f;
    
    void Start()
    {
        startingPosiiton = transform.position;
    }

    
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        else
        {
        float cycles = Time.time / period; //continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWawe = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWawe + 1f) /2f; // recalculated to go from 0 to 1 
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosiiton + offset;
        }
    }
}
