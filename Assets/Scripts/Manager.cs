using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Manager : MonoSingleton<Manager>
{
    bool gameOver = false;
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }
    [SerializeField] GameObject gameOverObj;

    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => gameOver)
            .Subscribe(_ => StartCoroutine(Quit()));
    }

    IEnumerator Quit()
    {
        gameOverObj.SetActive(true);
        yield return Coroutine.Wait1;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
