using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] int occurAfterVelosity;
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody playerRb;

    float counter;
    private void Update()
    {
        counter += Time.deltaTime;

        if (Mathf.Abs(playerRb.velocity.x) > occurAfterVelosity)
        {
            if(counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
}
