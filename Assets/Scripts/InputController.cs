using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyBoardLayout {AZERTY = 0, QWERTY = 1}

public class InputController : MonoBehaviour
{
    private KeyBoardLayout layout = KeyBoardLayout.AZERTY;

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

    public void SetQWERTY()
    {
        layout = KeyBoardLayout.QWERTY;
    }

    public void SetAZERTY()
    {
        layout = KeyBoardLayout.AZERTY;
    }

    public KeyBoardLayout GetLayout()
    {
        return layout;
    }
}
