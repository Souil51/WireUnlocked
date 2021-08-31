using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private List<int> lstScores = new List<int>();

    private static bool bInit = false;

    public void Awake()
    {
        if (!bInit)
        {
            DontDestroyOnLoad(transform.gameObject);

            bInit = true;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    public void AddScore(int nScore)
    {
        lstScores.Add(nScore);
    }

    public List<int> GetScores()
    {
        return lstScores;
    }
}
