using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class MainMenuController : MonoBehaviour
{
    Text txtTop1;
    Text txtTop2;
    Text txtTop3;
    Text txtTop4;
    Text txtTop5;

    Transform pnlScores;
    Transform pnlMain;
    Transform pnlCredits;

    // Start is called before the first frame update
    void Start()
    {
        txtTop1 = transform.Find("pnlScore").Find("top1").gameObject.GetComponent<Text>();
        txtTop2 = transform.Find("pnlScore").Find("top2").gameObject.GetComponent<Text>();
        txtTop3 = transform.Find("pnlScore").Find("top3").gameObject.GetComponent<Text>();
        txtTop4 = transform.Find("pnlScore").Find("top4").gameObject.GetComponent<Text>();
        txtTop5 = transform.Find("pnlScore").Find("top5").gameObject.GetComponent<Text>();

        pnlScores = transform.Find("pnlScore");
        pnlMain = transform.Find("Panel");
        pnlCredits = transform.Find("pnlCredits");
    }

    public void ChangeKeyboardLayout()
    {
        GameObject goInput = GameObject.FindGameObjectWithTag("InputController");
        InputController inputCtrl = goInput.GetComponent<InputController>();

        if (inputCtrl.GetLayout() == KeyBoardLayout.AZERTY)
        {
            inputCtrl.SetQWERTY();
            UpdateTextLayout(KeyBoardLayout.QWERTY);
        }
        else
        {
            inputCtrl.SetAZERTY();
            UpdateTextLayout(KeyBoardLayout.AZERTY);
        }
    }

    public void UpdateTextLayout(KeyBoardLayout layout)
    {
        //Maj du panel LAYOUT
        Transform tMainPanel = transform.Find("Panel");
        Transform tLayout = tMainPanel.Find("pnlLayout");

        Transform tText = tLayout.Find("Text");
        Transform tButton = tLayout.Find("Button");
        Transform tButtonText = tButton.Find("Text");

        if(layout == KeyBoardLayout.QWERTY)
        {
            tText.GetComponent<Text>().text = "Current keyboard layout : QWERTY";
            tButtonText.GetComponent<Text>().text = "Change to AZERTY";
        }
        else if(layout == KeyBoardLayout.AZERTY)
        {
            tText.GetComponent<Text>().text = "Current keyboard layout : AZERTY";
            tButtonText.GetComponent<Text>().text = "Change to QWERTY";
        }

        //Maj des règles
        Transform pnlRules = tMainPanel.Find("pnlRules");
        Transform txtRules = pnlRules.Find("txtRules_1");

        if (layout == KeyBoardLayout.QWERTY)
        {
            txtRules.GetComponent<Text>().text = txtRules.GetComponent<Text>().text.Replace("A Z E", "Q W E");
        }
        else if (layout == KeyBoardLayout.AZERTY)
        {
            txtRules.GetComponent<Text>().text = txtRules.GetComponent<Text>().text.Replace("Q W E", "A Z E");
        }
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void UpdateScores()
    {
        ScoreController scoreCtrl = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();

        List<int> lstScores = scoreCtrl.GetScores();

        if (lstScores.Count > 0)
            txtTop1.text = "Top 1 - " + lstScores.OrderByDescending(item => item).ToList()[0] + " points";
        else
            txtTop1.text = "No top 1";

        if (lstScores.Count > 1)
            txtTop2.text = "Top 2 - " + lstScores.OrderByDescending(item => item).ToList()[1] + " points";
        else
            txtTop2.text = "No top 2";

        if (lstScores.Count > 2)
            txtTop3.text = "Top 3 - " + lstScores.OrderByDescending(item => item).ToList()[2] + " points";
        else
            txtTop3.text = "No top 3";

        if (lstScores.Count > 3)
            txtTop4.text = "Top 4 - " + lstScores.OrderByDescending(item => item).ToList()[3] + " points";
        else
            txtTop4.text = "No top 4";

        if (lstScores.Count > 4)
            txtTop5.text = "Top 5 - " + lstScores.OrderByDescending(item => item).ToList()[4] + " points";
        else
            txtTop5.text = "No top 5";
    }

    public void ShowMainMenu()
    {
        pnlScores.gameObject.SetActive(false);
        pnlMain.gameObject.SetActive(true);
        pnlCredits.gameObject.SetActive(false);
    }

    public void ShowScores()
    {
        UpdateScores();

        pnlScores.gameObject.SetActive(true);
        pnlMain.gameObject.SetActive(false);
        pnlCredits.gameObject.SetActive(false);
    }

    public void ShowCredits()
    {
        pnlScores.gameObject.SetActive(false);
        pnlMain.gameObject.SetActive(false);
        pnlCredits.gameObject.SetActive(true);
    }
}
