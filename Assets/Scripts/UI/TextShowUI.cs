using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShowUI : MonoBehaviour
{
    public static TextShowUI instance;
    public GameObject origObj;
    public List<GameObject> tutorialObjList;

    private void Awake()
    {
        instance = this;
        //테스트 스트립트
        //TextShow_Player("안녕! 나는 공책 낙서야");
        //TutorialShortShow();
    }

    public void TextShow_Player(string _text)
    {
        Vector3 vec = FindObjectOfType<Player>().gameObject.transform.position;
        vec.x += 1;
        vec.y += 1.5f;
        TextShow(_text, vec);
    }

    public void TextShow(string _text, Vector3 _pos, int _time = 3)
    {
        _pos = Camera.main.WorldToScreenPoint(_pos);
        GameObject obj = Instantiate(origObj, _pos, Quaternion.identity);
        obj.transform.SetParent(transform);
        obj.transform.Find("Text").GetComponent<Text>().text = _text;
        StartCoroutine(ShowCoroutine(obj, _time));
    }

    IEnumerator ShowCoroutine(GameObject _target, int _time = 3)
    {
        yield return new WaitForSeconds(_time);
        Destroy(_target);
    }

    public void TutorialShortShow(int _idx = 0, int _time = 10)
    {
        StartCoroutine(ShowHideCoroutine(tutorialObjList[_idx], _time));
    }

    IEnumerator ShowHideCoroutine(GameObject _target, int _time = 10)
    {
        _target.SetActive(true);
        yield return new WaitForSeconds(_time);
        _target.SetActive(false);
    }
}