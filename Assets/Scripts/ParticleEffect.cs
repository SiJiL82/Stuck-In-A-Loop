using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class ParticleEffect : MonoBehaviour
{
    //Config
    //Variables
    VisualEffect visualEffect;
    private float timeAlive;
    //References
    // Start is called before the first frame update
    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        timeAlive = Time.time;
    }

    
    void Update()
    {
        if(visualEffect.aliveParticleCount == 0 && Time.time - timeAlive > 1)
        {
            Destroy(gameObject);
        }
    }
}
