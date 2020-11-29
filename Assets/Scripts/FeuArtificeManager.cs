using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FeuArtificeManager : MonoBehaviour
{
    private Animator feuArtifice;
    public float wait ;
    public int nbreRound;
    public Light2D myLight;

    private int roundCount = 0;
    public Color[] colors;
    public AudioClip boomS;
    public AudioClip cracklingS;

    private AudioSource aS;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        feuArtifice = GetComponent<Animator>();
        wait = Time.time + Random.Range(1f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if(wait != -1 && Time.time > wait && nbreRound > roundCount)
        {
            GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 6)];

            feuArtifice.SetTrigger("boomboom");
            wait = Time.time + Random.Range(2f, 6f);
            roundCount++;
        }
    }

    public void boom()
    {
        aS.volume = 1f;
        aS.PlayOneShot(boomS);
        myLight.color = GetComponent<SpriteRenderer>().color;
        myLight.enabled = true;
    }

    public void crackling()
    {
        aS.volume = 0.3f;
        aS.PlayOneShot(cracklingS);
        myLight.enabled = false;
    }
}
