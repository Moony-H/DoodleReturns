using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultUI : MonoBehaviour
{
    public static DefaultUI instance;
    public bool active = false;
    public GameObject child_background;
    public bool canGameOver = true;

    public static int gameoverScene = 0;

    private void Awake()
    {
        instance = this;
        if (child_background != null && child_background.activeSelf != active)
            child_background.SetActive(active);
    }

    private void Update()
    {
        bool esc = Input.GetKeyDown(KeyCode.Escape);

        if (esc)
            StopUIActive(!active);

    }

    public void StopUIActive(bool _true = true)
    {
        active = _true;
        GameStop(active);
        child_background.SetActive(active);
    }

    public void StopUINone()
    {
        StopUIActive(false);
    }

    public void OpenScene(string _sceneName)
    {
        OpenScene(SceneManager.GetSceneByName(_sceneName).buildIndex);
    }

    public void OpenScene(int _sceneIdx)
    {
        GameStop(false);
        SceneManager.LoadScene(_sceneIdx);
        switch (_sceneIdx)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                gameObject.SetActive(false);
                break;
            default:
                gameObject.SetActive(true);
                break;
        }
    }

    public void GameStop(bool _stop = false) 
    {
        if(_stop)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        //Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void GameRestart()
    {
        GameRestart(true);
    }

    public void GameRestart(bool _gameover = true)
    {
        Debug.Log("현재의 씬을 다시 엶");
        if (!_gameover)
        {
            OpenScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (!canGameOver) return;
        // 게임 오버로 이동하는 코드
        gameoverScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOver");
    }

    public void GameOverBack()
    {
        OpenScene(gameoverScene);
        gameoverScene = 0;
    }

    public void GameGoToTitle()
    {
        Debug.Log("0번째 씬을 엶");
        OpenScene(0);
    }

    public void GameEnd()
    {
        Application.Quit();
        Debug.Log("게임 종료 실행됨");
    }

}
