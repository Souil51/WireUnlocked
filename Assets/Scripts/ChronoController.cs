using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChronoController : MonoBehaviour
{
    public float fTimeLeft = 30;

    public Text txtChrono;

    private bool bIsCounting = false;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        txtChrono.text = fTimeLeft.ToString("0.00").Replace(",", ".");

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsCounting)
        {
            fTimeLeft -= Time.deltaTime;

            txtChrono.text = fTimeLeft.ToString("0.00").Replace(",", ".");

            if (fTimeLeft <= 0)
            {
                bIsCounting = false;
                txtChrono.text = "0.00";

                gameController.TimerEnded();
            }
        }
    }

    public void StartCounting()
    {
        bIsCounting = true;
    }
}
