using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum CouleurMain { RED, GREEN, BLUE}

public class GameController : MonoBehaviour
{
    private string m_szEndText = "You score is ";

    List<GameObject> lstBar = new List<GameObject>();
    private int nZPosition = 0;

    string[] szCouleurs = {"blue", "green", "red"};

    private CouleurMain m_currentColor = CouleurMain.RED;

    int m_nComboCouleur = 0;//Le joueur clique toujours sur un centre de la bonne couleur
    int m_nComboPrecision = 0;//Le joueur clique toujours sur un centre
    int m_nComboDouble = 0;//Deux combo en même temps

    private bool bGameStarted = false;

    ChronoController ctrlTimer;
    CanvasController ctrlCanvas;

    private int m_nScore = 0;
    private int m_nScoreMultiplier = 100;

    private bool bComboColorAdded = false;
    private bool bComboPrecisionAdded = false;

    private bool bFirstStart = false;

    private int m_nBarKilledTimer = 0;

    private KeyCode code_A;
    private KeyCode code_Z;
    private KeyCode code_E;

    // Start is called before the first frame update
    void Start()
    {
        ctrlCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();

        for (int i = 0; i < 5; i++)
        {
            CreateNewBar();
        }

        ChangeColor(CouleurMain.RED);

        ctrlTimer = GameObject.FindGameObjectWithTag("Chrono").GetComponent<ChronoController>();

        GameObject goInput = GameObject.FindGameObjectWithTag("InputController");
        InputController inputCtrl = goInput.GetComponent<InputController>();

        if(inputCtrl.GetLayout() == KeyBoardLayout.AZERTY)
        {
            code_A = KeyCode.A;
            code_Z = KeyCode.Z;
            code_E = KeyCode.E;
        }
        else if (inputCtrl.GetLayout() == KeyBoardLayout.QWERTY)
        {
            code_A = KeyCode.Q;
            code_Z = KeyCode.W;
            code_E = KeyCode.E;
        }

        ctrlCanvas.SetKeyString(code_A, code_Z, code_E);
    }

    // Update is called once per frame
    void Update()
    {
        bComboColorAdded = bComboPrecisionAdded = false;

        if (bGameStarted)
        {
            if (!bFirstStart)
                InvokeRepeating("ResetTimerBar", 0f, 8f);

            bFirstStart = true;

            ctrlTimer.StartCounting();

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null && hit.collider.tag == "PartCenter")
                {
                    bComboPrecisionAdded = true;

                    if (hit.collider.transform.parent.tag == "RedBar" && m_currentColor == CouleurMain.RED
                        || hit.collider.transform.parent.tag == "GreenBar" && m_currentColor == CouleurMain.GREEN
                        || hit.collider.transform.parent.tag == "BlueBar" && m_currentColor == CouleurMain.BLUE)
                    {
                        bComboColorAdded = true;

                        BarController bContr = hit.transform.parent.gameObject.GetComponent<BarController>();
                        bContr.CenterClicked();
                    }
                }
            }

            if (bComboColorAdded)
                m_nComboCouleur++;

            if (bComboPrecisionAdded)
                m_nComboPrecision++;

            if (bComboColorAdded && bComboPrecisionAdded)
                m_nComboDouble++;

            if (Input.GetKeyDown(code_A))
            {
                ChangeColor(CouleurMain.RED);
            }

            if (Input.GetKeyDown(code_Z))
            {
                ChangeColor(CouleurMain.GREEN);
            }

            if (Input.GetKeyDown(code_E))
            {
                ChangeColor(CouleurMain.BLUE);
            }

            int nScoreAdded = 0;

            //Si on a les 2 combo en même temps
            if(m_nComboDouble == 10)
            {
                nScoreAdded += 2000;

                m_nComboDouble = 0;

                ctrlCanvas.StartColorCombo();
                StartPrecisionCombo();
            }
            else
            {
                //Sinon, si on a un des 2 combo

                //Combo Couleur
                if (m_nComboCouleur == 10)
                {
                    nScoreAdded += 1000;

                    m_nComboCouleur = 0;

                    ctrlCanvas.StartColorCombo();
                }

                //Combo Précision
                if (m_nComboPrecision == 10)
                {
                    nScoreAdded += 1000;

                    m_nComboPrecision = 0;

                    StartPrecisionCombo();
                }
            }

            if (m_nBarKilledTimer >= 5)
            {
                nScoreAdded += 1000;

                m_nBarKilledTimer = 0;

                ctrlCanvas.StartTimerCombo();
            }

            if (nScoreAdded > 0)
            {
                m_nScore += nScoreAdded;
                ctrlCanvas.UpdateScore(m_nScore);
            }
        }
    }

    public void DeleteBar(GameObject goBar)
    {
        lstBar.Remove(goBar);

        BarController barCtrl = goBar.GetComponent<BarController>();
        barCtrl.StartShake();

        CreateNewBar();
    }

    public void CreateNewBar()
    {
        int nCouleur = Random.Range(0, szCouleurs.Length);

        GameObject bar = (GameObject)GameObject.Instantiate(Resources.Load("Bar_" + szCouleurs[nCouleur]));

        lstBar.Add(bar);
        nZPosition++;
        bar.transform.position = new Vector3(0, 0, nZPosition);
    }

    public void ChangeColor(CouleurMain couleur)
    {
        ctrlCanvas.ChangeColor(couleur);

        this.m_currentColor = couleur;
    }

    public void StartGame()
    {
        this.bGameStarted = true;
    }

    public void AddScore()
    {
        m_nScore += m_nScoreMultiplier;

        m_nBarKilledTimer++;

        ctrlCanvas.UpdateScore(m_nScore);
    }

    public void TimerEnded()
    {
        bGameStarted = false;

        ctrlCanvas.GameEnd(m_szEndText + " " + m_nScore);

        ScoreController scoreCtrl = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        scoreCtrl.AddScore(m_nScore);
    }

    public void StartPrecisionCombo()
    {
        GameObject goCanvas = GameObject.FindGameObjectWithTag("Canvas");
        Transform txtCombo = goCanvas.transform.Find("Combo_precision_text");

        ComboController comboCtrl = txtCombo.gameObject.GetComponent<ComboController>();
        comboCtrl.StartAnimation();
    }

    public void ResetTimerBar()
    {
        m_nBarKilledTimer = 0;
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);

        m_nBarKilledTimer--;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
