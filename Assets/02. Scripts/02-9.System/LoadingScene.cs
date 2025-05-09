using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private int _nextSceneIndex = 2;

    [SerializeField]
    private Slider _loadingSlider;

    [SerializeField]
    private TextMeshProUGUI _progressTMP;

    private void Start()
    {
        StartCoroutine(LoadMainSceneAsync(_nextSceneIndex));
    }

    private IEnumerator LoadMainSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false; // 비동기로 로드되는 Scene의 모습이 화면에 보이지 않게 한다.

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingSlider.value = asyncOperation.progress;
            _progressTMP.text = $"{(progress * 100f):0}%";

            // 이 때, 서버와 통신해서 유저 및 기획 데이터를 받아오면 된다.

            if (0.9f <= asyncOperation.progress)
            {
                _loadingSlider.value = 1;
                _progressTMP.text = $"100%";
                asyncOperation.allowSceneActivation = true; // 로딩이 완료되면 Scene을 활성화한다.
            }
            yield return null;
        }
    }
}


