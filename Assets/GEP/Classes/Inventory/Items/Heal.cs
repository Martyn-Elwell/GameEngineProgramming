using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, IUseable
{
    public GameObject particle;
    public void Use()
    {
        particle = GameObject.Find("Aura");
        if (particle != null )
        {
            particle.GetComponent<ParticleSystem>().Play();
        }
        StartCoroutine(timerCoroutine(particle));

    }

    private IEnumerator timerCoroutine(GameObject particle)
    {

        yield return new WaitForSeconds(2f);
        particle.GetComponent<ParticleSystem>().Stop();
        Destroy(this);
    }
}
