using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public void ChangeColor(CouleurMain couleur)
    {
        Transform tPanel = transform.Find("Panel");

        Transform pnlRed = tPanel.Find("pnlRed");
        Transform pnlBlue = tPanel.Find("pnlBlue");
        Transform pnlGreen = tPanel.Find("pnlGreen");

        pnlRed.gameObject.SetActive(couleur == CouleurMain.RED);
        pnlGreen.gameObject.SetActive(couleur == CouleurMain.GREEN);
        pnlBlue.gameObject.SetActive(couleur == CouleurMain.BLUE);

        Image imgBorderRedBottom = transform.Find("pnlBorder").Find("bottom").GetComponent<Image>();
        Image imgBorderRedLeft = transform.Find("pnlBorder").Find("left").GetComponent<Image>();
        Image imgBorderRedRight = transform.Find("pnlBorder").Find("right").GetComponent<Image>();

        switch (couleur)
        {
            case CouleurMain.RED:
                imgBorderRedBottom.color = imgBorderRedLeft.color = imgBorderRedRight.color = new Color(1f, 0.2039f, 0.2039f);
                break;
            case CouleurMain.GREEN:
                imgBorderRedBottom.color = imgBorderRedLeft.color = imgBorderRedRight.color = new Color(0.3608f, 1f, 0.3176f);
                break;
            case CouleurMain.BLUE:
                imgBorderRedBottom.color = imgBorderRedLeft.color = imgBorderRedRight.color = new Color(0.1922f, 0,2353f, 1f);
                break;
        }
    }

    public void GameEnd(string szText)
    {
        Transform pnlEndGame = transform.Find("pnlEndGame");
        Transform txtEnded = pnlEndGame.Find("txtEnded");

        txtEnded.GetComponent<Text>().text = szText;

        pnlEndGame.gameObject.SetActive(true);
    }

    public void UpdateScore(int nScore)
    {
        Transform pnlMain = transform.Find("Panel");
        Transform pnlScore = pnlMain.Find("Score");
        Text txtScore = pnlScore.Find("Text").gameObject.GetComponent<Text>();

        txtScore.text = nScore.ToString();
    }

    public void StartColorCombo()
    {
        Transform txtCombo = transform.Find("Combo_color_text");

        ComboController comboCtrl = txtCombo.gameObject.GetComponent<ComboController>();
        comboCtrl.StartAnimation();
    }

    public void StartTimerCombo()
    {
        Transform txtCombo = transform.Find("Combo_kill_text");

        ComboController comboCtrl = txtCombo.gameObject.GetComponent<ComboController>();
        comboCtrl.StartAnimation();
    }

    public void SetKeyString(KeyCode kc_A, KeyCode kc_Z, KeyCode kc_E)
    {
        Transform tPanel = transform.Find("Panel");

        Transform txtA = tPanel.Find("txt_A");
        Transform txtZ = tPanel.Find("txt_Z");
        Transform txtE = tPanel.Find("txt_E");

        txtA.GetComponent<Text>().text = kc_A == KeyCode.A ? "A" : "Q";
        txtZ.GetComponent<Text>().text = kc_Z == KeyCode.Z ? "Z" : "W";
        txtE.GetComponent<Text>().text = kc_E == KeyCode.E ? "E" : "E";
    }
}
