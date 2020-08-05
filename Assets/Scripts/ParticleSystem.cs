using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyParticleSystem", 2f);
    }

    void DestroyParticleSystem() {
        Destroy(gameObject);
    }
}
