using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleShooter : MonoBehaviour
{
    [SerializeField] Transform shootingLocation;
    [SerializeField] GameObject particlePrefab;

    public void ShootParticle()
    {
        GameObject particle = Instantiate(particlePrefab, shootingLocation.position, shootingLocation.rotation);
        Destroy(particle, 3f);
    }
}
