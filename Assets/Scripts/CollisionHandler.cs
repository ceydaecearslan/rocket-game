using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip levelUp;
    [SerializeField] ParticleSystem levelUpParticles;
    [SerializeField] ParticleSystem explosionParticles;
    AudioSource audioSource;
    Movement movement;
    
    int currentLevel;
    bool isTransitioning = false;
    bool collision = true;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollisionDisable();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
    }

    private void CollisionDisable()
    {
        collision = !collision;
        Debug.Log("Pressed C!");
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            
            case "Finish":
                Success();
                isTransitioning = true;
                Invoke("NextLevel", 1);
                break;

            default:
                if (collision)
                {
                    Crash();
                    isTransitioning = true;
                    Invoke("ReloadLevel",1);
                    //ReloadLevel();
                }
                break;

        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    void NextLevel()
    {
        int nextLevel = currentLevel + 1;

        if(nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }

        SceneManager.LoadScene(nextLevel);
    }

    void Crash()
    {
        isTransitioning = true;
        movement.enabled = false;
        if(audioSource.clip != explosion)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.PlayOneShot(explosion, 0.5f);
            explosionParticles.Play();
        }       
    }

    void Success()
    {
        isTransitioning = true;
        movement.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelUp);
        levelUpParticles.Play();
    }

}
