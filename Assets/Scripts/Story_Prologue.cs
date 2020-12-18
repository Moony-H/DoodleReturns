using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_Prologue : MonoBehaviour
{
    public TextShowUI textUI;
    public List<string> strList;

    private void Awake()
    {
        textUI = GameObject.FindObjectOfType<TextShowUI>();
        StringPrint(strList, Vector3.zero);
    }

    public void StringPrint(List<string> _strList, Vector3 _pos)
    {
        StartCoroutine(StringPrintCoroutine(_strList, _pos));
    }

    IEnumerator StringPrintCoroutine(List<string> _strList, Vector3 _pos)
    {
        yield return null;
        for(int i = 0; i < _strList.Count; i++)
        {
            textUI.TextShow(_strList[i], _pos, 5);
            yield return new WaitForSeconds(5);
        }

        SceneManager.LoadScene("Scene01");
    }
}
