using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Source link: https://www.youtube.com/watch?v=wcQqYOSteSs&t=39s
public class TerminatorSpawnEvent : MonoBehaviour
{
    public GameObject terminator;
    public GameObject freePowerOrb;
    public GameObject rockExplosionSound;
    public GameObject rockExplosionVisual;
    private AudioSource rockExplosionAudioSource;
    private ParticleSystem rockExplosionVisualParticleSystem;
    private int cubesPerAxis = 6;
    private bool hasSpawned = false;
    private bool hasPlayed = false;
    
    void Start()
    {
        // Terminator has not spawned yet
        terminator.SetActive(false);
        
        // Rock explosion sound and visual effects, not happening at Start() yet
        rockExplosionAudioSource = rockExplosionSound.GetComponent<AudioSource>();
        rockExplosionVisualParticleSystem = rockExplosionVisual.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Gives a more ominous presence if it moves a little
        transform.Rotate(new Vector3(15.0f, 30.0f, 45.0f) * Time.deltaTime);

        if (!freePowerOrb.activeInHierarchy && !hasSpawned)
        {
            // Split cubes into even, equal pieces that add up to the big cube's scale in all dimensions
            for (int x = 0; x < cubesPerAxis; x++)
            {
                for (int y = 0; y < cubesPerAxis; y++)
                {
                    for (int z = 0; z < cubesPerAxis; z++)
                    {
                        // Small cubes should be same material as the big cube
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        Renderer rd = cube.GetComponent<Renderer>();
                        rd.material = GetComponent<Renderer>().material;

                        // Position cubes to be in the collective position of the big cube
                        cube.transform.localScale = transform.localScale / cubesPerAxis;
                        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
                        cube.transform.position = firstCube + Vector3.Scale(new Vector3(x, y, z), cube.transform.localScale);

                        // Explode the Terminator spawn rock
                        Rigidbody rb = cube.AddComponent<Rigidbody>();
                        rb.AddExplosionForce(300.0f, transform.position, 2.0f, 3.0f, ForceMode.Impulse);
                    }
                }
            }

            // The rock should only explode once, at the time of Terminator spawning
            hasSpawned = true;

            // Play rock explosion visual and sound effects
            rockExplosionVisualParticleSystem.Play();
            Destroy(rockExplosionVisual, rockExplosionVisualParticleSystem.duration);

            if (!hasPlayed)
            {
                rockExplosionAudioSource.spatialBlend = 0.80f;
                rockExplosionAudioSource.Play();
                hasPlayed = true;
            }
            
            
            // The terminator has spawned
            terminator.SetActive(true);

            // No need for the giant cube anymore
            Destroy(gameObject);
        }
    }
}
