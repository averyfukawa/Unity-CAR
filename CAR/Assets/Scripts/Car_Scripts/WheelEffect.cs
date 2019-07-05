using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof (AudioSource))] // For the skidding/"sliding" sound
public class WheelEffect : MonoBehaviour
{
    // Initialize the prefab of the skidrail trail effect/renderer
    public Transform skidTrailPrefab;
    public static Transform SkidTrailsDetachedParent;

    // Burnout smoke
    public ParticleSystem skidParticles;

    // Used to check when the car is skidding
    private bool Skidding { get; set; }

    // Used to start and stop the audio
    public bool PlayingAudio {
        get;
        private set;
    }

    private AudioSource m_AudioSource;
    private Transform m_SkidTrail;
    private WheelCollider m_WheelCollider;

    // Here the correct values are grabbed, audio source set, and checked for any errors
    private void Start()
    {
        // Here is how it finds the smoke trail
        skidParticles = transform.root.GetComponentInChildren<ParticleSystem>();

        // Nullpointer exception
        if (skidParticles == null)
        { throw new Exception("No particle system found on " + gameObject.name); }

        skidParticles.Stop();

        m_WheelCollider = GetComponent<WheelCollider>();
        m_AudioSource = GetComponent<AudioSource>();
        PlayingAudio = false; // Make it so it doesn't play on awake/start

        // If the skidrails don't exist, create a new GameObject 
        if (SkidTrailsDetachedParent == null)
        {
            SkidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
            // Since they wouldn't stay glued to the wheels, they kinda get "planted" as their new transform position
        }
    }

    // These are all called up in Car Manager script, only when the slip threshold is passed
    public void EmitTyreSmoke()
    {
        // Make the transform position be behind the wheel's radius
        skidParticles.transform.position = transform.position - transform.up*m_WheelCollider.radius;
            
        // Particle system
        skidParticles.Emit(1);

        // Self explanatory
        if (!Skidding)
        {
            StartCoroutine(StartSkidTrail());
        }
    }

    // These are all called up in car controller script
    public void PlayAudio()
    {
        m_AudioSource.Play();
        PlayingAudio = true;
    }

    // These are all called up in car controller script
    public void StopAudio()
    {
        m_AudioSource.Stop();
        PlayingAudio = false;
    }

    public IEnumerator StartSkidTrail()
    {
        Skidding = true;

        // Bring up the Skid Trail particle effect prefab
        m_SkidTrail = Instantiate(skidTrailPrefab);

        // When the particle ends, stop it from continuing to do stuff
        while (m_SkidTrail == null)
        {
            yield return null;
        }

        // Calculation not made by me, it just makes it so the local position of the skidrail to be the opposite of the Vector3 of the wheels
        m_SkidTrail.parent = transform;
        m_SkidTrail.localPosition = -Vector3.up*m_WheelCollider.radius;
    }
    
    // These are all called up in car controller script
    public void EndSkidTrail()
    {
        // If it has already stopped skidding, no need to run this
        if (!Skidding)
        {
            return;
        }

        // Disable skidding
        Skidding = false;

        // Seamlessly make it stay on the ground, in the same sense a car would not keep producing black marks on the ground when it stops skidding
        m_SkidTrail.parent = SkidTrailsDetachedParent;
        Destroy(m_SkidTrail.gameObject, 10);
        // Make the SkidTrail disappear after 10 seconds
    }
}
