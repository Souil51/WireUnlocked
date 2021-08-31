using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    public string Combo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationStopped()
    {
        gameObject.SetActive(false);
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);

        Animator animat = GetComponent<Animator>();

        animat.Play(Combo);
    }
}
