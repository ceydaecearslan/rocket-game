using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float upValue = 750f;
    [SerializeField] float rotationValue = 250f;
    [SerializeField] AudioClip flyingRocket;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rbody;
    AudioSource audioSource;

    void Start()
    {
       rbody = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessBoost();
        ProcessRotation();
    }

    void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            StartBoost();
        }
        else
        {
            StopBoost();
        }
    }

    void StartBoost()
    {
        rbody.AddRelativeForce(Vector3.up * upValue * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(flyingRocket, 0.5f);
            mainBooster.Play();
        }
    }

    void StopBoost()
    {
        audioSource.Stop();
        audioSource.loop = false;
        mainBooster.Stop();
    }

    void ProcessRotation()
    { 
        if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationValue);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationValue);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void ApplyRotation(float rotationValue)
    {
        StartRotation(rotationValue);
    }

    void StartRotation(float rotationValue)
    {
        rbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rbody.freezeRotation = false;
    }

    void StopRotation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }
}