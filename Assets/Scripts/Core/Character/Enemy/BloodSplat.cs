using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    
    public void Initialize(Transform enemy)
    {
        transform.position = enemy.position;
        _particle.Play();
    }
    
}
