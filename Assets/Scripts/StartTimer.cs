using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("StartTimer");
    }

    public void StartTimerFinished()
    {
        gameObject.SetActive(false);

        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        GameController goCtrl = go.GetComponent<GameController>();

        goCtrl.StartGame();
    }
}
