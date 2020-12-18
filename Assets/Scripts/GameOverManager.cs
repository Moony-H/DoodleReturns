using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : SingletonGameObject<GameOverManager>
{
    public int beforeScene;

    public void OpenScene()
    {
        SceneManager.LoadScene(beforeScene);
    }
}
