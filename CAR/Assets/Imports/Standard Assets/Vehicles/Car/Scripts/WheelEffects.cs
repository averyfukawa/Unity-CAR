using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (AudioSource))] // For the skidding/"sliding" sound
    public class WheelEffects : MonoBehaviour
    {
        // Initialize the prefab of the skidrail trail effect/renderer
        public Transform SkidTrailPrefab;
        public static Transform skidTrailsDetachedParent;

        // Burnout smoke
        public ParticleSystem skidParticles;

        // Used to check when the car is skidding
        public bool skidding {
            get;
            private set;
        }

        // Used to start and stop the audio
        public bool PlayingAudio {
            get;
            private set;
        }

        private AudioSource m_AudioSource;
        private Transform m_SkidTrail;
        private WheelCollider m_WheelCollider;


        private void Start()
        {
            // Here is how it finds the smoke trail
            skidParticles = transform.root.GetComponentInChildren<ParticleSystem>();

            // Nullpointer exception
            if (skidParticles == null)
            {
                Debug.LogWarning(" no particle system found on car to generate smoke particles", gameObject);
            }
            else
            {
                skidParticles.Stop();
            }

            m_WheelCollider = GetComponent<WheelCollider>();
            m_AudioSource = GetComponent<AudioSource>();
            PlayingAudio = false; // Make it so it doesn't play on awake/start

            // If the skidrails don't exist, create a new GameObject 
            if (skidTrailsDetachedParent == null)
            {
                skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
                // Since they wouldn't stay glued to the wheels, they kinda get "planted" as their new transform position
            }
        }


        public void EmitTyreSmoke()
        {
            // Make the transform position be behind the wheel's radius
            skidParticles.transform.position = transform.position - transform.up*m_WheelCollider.radius;
            
            // Particle system
            skidParticles.Emit(1);

            // Self explanatory
            if (!skidding)
            {
                StartCoroutine(StartSkidTrail());
            }
        }

        public void PlayAudio()
        {
            m_AudioSource.Play();
            PlayingAudio = true;
        }


        public void StopAudio()
        {
            m_AudioSource.Stop();
            PlayingAudio = false;
        }


        public IEnumerator StartSkidTrail()
        {
            skidding = true;

            // Bring up the Skid Trail particle effect prefab
            m_SkidTrail = Instantiate(SkidTrailPrefab);

            // When the particle ends, stop it from continuing to do stuff
            while (m_SkidTrail == null)
            {
                yield return null;
            }

            // Calculation not made by me, it just makes it so the local position of the skidrail to be the opposite of the Vector3 of the wheels
            m_SkidTrail.parent = transform;
            m_SkidTrail.localPosition = -Vector3.up*m_WheelCollider.radius;
        }
        
        public void EndSkidTrail()
        {
            // If it has already stopped skidding, no need to run this
            if (!skidding)
            {
                return;
            }

            // Disable skidding
            skidding = false;

            // Seamlessly make it stay on the ground, in the same sense a car would not keep producing black marks on the ground when it stops skidding
            m_SkidTrail.parent = skidTrailsDetachedParent;
            Destroy(m_SkidTrail.gameObject, 10);
            // Make the SkidTrail disappear after 10 seconds
        }
    }
}
