using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    private SpriteRenderer sprt_part_1;
    private SpriteRenderer sprt_part_2;
    private SpriteRenderer sprt_part_center;
    private SpriteRenderer sprt_number;

    private GameObject part_1;
    private GameObject part_2;
    private GameObject part_center;
    private GameObject number;

    private int nMaxNbClicked = 4;
    private int nbClicked = 4;

    private float fShake = 0;
    private float fShakeAmount = 0.05f;
    private float fDecreaseFactor  = 5.0f;

    private Vector3 vPositionOnShake = Vector3.zero;

    private Vector2 velocitySmooth;
    private float smoothTimeX = 0.5f;
    private float smoothTimeY = 0.5f;

    private float verticalSpeed = 0.7f;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        part_1 = transform.Find("part_1").gameObject;
        part_2 = transform.Find("part_2").gameObject;
        part_center = transform.Find("part_center").gameObject;
        number = transform.Find("number").gameObject;

        sprt_part_1 = part_1.transform.GetComponent<SpriteRenderer>();
        sprt_part_2 = part_2.transform.GetComponent<SpriteRenderer>();
        sprt_part_center = part_center.transform.GetComponent<SpriteRenderer>();
        sprt_number = number.transform.GetComponent<SpriteRenderer>();

        int nForme = Random.Range(0, 6);

        int nPart1Rotate = 0;
        int nPart2Rotate = 0;

        float fPositionXPart1 = 0;
        float fPositionYPart1 = 0;

        float fPositionXPart2 = 0;
        float fPositionYPart2 = 0;

        float fSizeCenter = sprt_part_center.bounds.size.x;

        RectTransform rtPart1 = part_1.GetComponent<RectTransform>();

        float fLongueur = sprt_part_center.bounds.size.x;
        float fLargeur = sprt_part_center.bounds.size.y;

        float fSizeCenterOffset = (fSizeCenter * 3) + (fSizeCenter / 2);

        float fValeur = 16.06f;

        switch (nForme)
        {
            case 0:
                {
                    nPart2Rotate = 180;

                    fPositionXPart1 = 0;
                    fPositionYPart1 = -fValeur;

                    fPositionXPart2 = 0;
                    fPositionYPart2 = fValeur;

                    break;
                }
            case 1:
                {
                    nPart1Rotate = -90;
                    nPart2Rotate = 90;

                    fPositionXPart1 = -fValeur;
                    fPositionYPart1 = 0;

                    fPositionXPart2 = fValeur;
                    fPositionYPart2 = 0;

                    break;
                }
            case 2:
                {
                    nPart1Rotate = -90;

                    fPositionXPart1 = -fValeur;
                    fPositionYPart1 = 0;

                    fPositionXPart2 = 0;
                    fPositionYPart2 = -fValeur;

                    break;
                }
            case 3:
                {
                    nPart2Rotate = 90;

                    fPositionXPart1 = 0;
                    fPositionYPart1 = -fValeur;

                    fPositionXPart2 = fValeur;
                    fPositionYPart2 = 0;

                    break;
                }
            case 4:
                {
                    nPart1Rotate = 90;
                    nPart2Rotate = 180;

                    fPositionXPart1 = fValeur;
                    fPositionYPart1 = 0;

                    fPositionXPart2 = 0;
                    fPositionYPart2 = fValeur;

                    break;
                }
            case 5:
                {
                    nPart1Rotate = 180;
                    nPart2Rotate = -90;

                    fPositionXPart1 = 0;
                    fPositionYPart1 = fValeur;

                    fPositionXPart2 = -fValeur;
                    fPositionYPart2 = 0;

                    break;
                }
        }

        part_1.transform.rotation = Quaternion.Euler(0, 0, nPart1Rotate);// new Quaternion(0, 0, nPart1Rotate, 0);
        part_2.transform.rotation = Quaternion.Euler(0, 0, nPart2Rotate);// new Quaternion(0, 0, nPart2Rotate, 0);

        part_1.transform.localPosition = new Vector3(fPositionXPart1, fPositionYPart1, 0);
        part_2.transform.localPosition = new Vector3(fPositionXPart2, fPositionYPart2, 0);

        //Position sur la camera
        float fPosXTest = Camera.main.pixelWidth;
        float fPosYTest = Camera.main.pixelHeight;
        Vector3 vLast = Camera.main.ScreenToWorldPoint(new Vector3(fPosXTest, fPosYTest, 0));

        Vector3 vLastCanvas = Camera.main.ScreenToWorldPoint(new Vector3(0, 75, 0));

        float fMaxX = vLast.x;
        float fMaxY = vLast.y;

        float fCurrentX = Random.Range(-fMaxX + 2, fMaxX - 2);
        float fCurrentY = Random.Range(-fMaxY + 2, fMaxY - 2 + (-fMaxY - vLastCanvas.y));

        transform.position = new Vector3(fCurrentX, fCurrentY, transform.position.z);

        int nNbClick = Random.Range(1, nMaxNbClicked + 1);
        this.nbClicked = nNbClick;

        UpdateNumber();
    }

    // Update is called once per frame
    void Update()
    {
        if (fShake > 0)
        {
            //On secoue les bars
            transform.localPosition = vPositionOnShake + (Random.insideUnitSphere * fShakeAmount);
            fShake -= Time.deltaTime * fDecreaseFactor;
        }
        else
        {
            fShake = 0.0f;

            if (vPositionOnShake != Vector3.zero)
            {
                float verifBas = Quaternion.Euler(new Vector3(0, 0, 0)).z;
                float verifHaut = Quaternion.Euler(new Vector3(0, 0, 180)).z;
                float verifRight = Quaternion.Euler(new Vector3(0, 0, 90)).z;
                float verifLeft = Quaternion.Euler(new Vector3(0, 0, -90)).z;

                Vector3 vOrigine = new Vector3(0, 0, 0);

                //Les bar sortent de l'écran
                if (part_1.transform.rotation.z == verifBas)//Vers le bas
                {
                    part_1.transform.localPosition = part_1.transform.localPosition + (Vector3.down * verticalSpeed);
                }
                else if(Mathf.Approximately(part_1.transform.rotation.z, verifLeft))//Vers la gauche
                {
                    part_1.transform.localPosition = part_1.transform.localPosition + (Vector3.left * verticalSpeed);
                }
                else if (Mathf.Approximately(part_1.transform.rotation.z, verifRight))//Vers la droite
                {
                    part_1.transform.localPosition = part_1.transform.localPosition + (Vector3.right * verticalSpeed);
                }
                else if (part_1.transform.rotation.z == verifHaut)//Vers le haut
                {
                    part_1.transform.localPosition = part_1.transform.localPosition + (Vector3.up * verticalSpeed);
                }

                if (part_2.transform.rotation.z == verifBas)//Vers le bas
                {
                    part_2.transform.localPosition = part_2.transform.localPosition + (Vector3.down * verticalSpeed);
                }
                else if (Mathf.Approximately(part_2.transform.rotation.z, verifLeft))//Vers la gauche
                {
                    part_2.transform.localPosition = part_2.transform.localPosition + (Vector3.left * verticalSpeed);
                }
                else if (Mathf.Approximately(part_2.transform.rotation.z, verifRight))//Vers la droite
                {
                    part_2.transform.localPosition = part_2.transform.localPosition + (Vector3.right * verticalSpeed);
                }
                else if (part_2.transform.rotation.z == verifHaut)//Vers le haut
                {
                    part_2.transform.localPosition = part_2.transform.localPosition + (Vector3.up * verticalSpeed);
                }

                //On vérifie la distance depuis l'origine sans prendre en le Z car le Z augmente au fur et à mesure de l'instanciation des bars
                Vector3 vPosNeutre = new Vector3(part_1.transform.position.x, part_1.transform.position.y, 0);
                if(Vector3.Distance(vOrigine, vPosNeutre) > 50)
                    Destroy(this.gameObject);
            }
        }
    }

    public void CenterClicked()
    {
        //part_center.transform.rotation *= Quaternion.Euler(0, 0, 90);
        nbClicked--;

        UpdateNumber();

        if(nbClicked == 0)
        {
            GameObject goGameController = GameObject.FindGameObjectWithTag("GameController");
            GameController gameCtrl = goGameController.GetComponent<GameController>();

            gameCtrl.DeleteBar(this.gameObject);
        }
    }

    private void UpdateNumber()
    {
        string szNumber = "number_" + nbClicked;

        var test = Resources.Load("number_sprite/" + szNumber);
        Sprite sprtToSet = Resources.Load<Sprite>("number_sprite/" + szNumber);
        this.sprt_number.sprite = sprtToSet;
    }

    public void StartShake()
    {
        fShake = 2.0f;
        vPositionOnShake = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        part_1.GetComponent<BoxCollider2D>().enabled = false;
        part_2.GetComponent<BoxCollider2D>().enabled = false;
        part_center.GetComponent<BoxCollider2D>().enabled = false;

        gameController.AddScore();
    }
}
