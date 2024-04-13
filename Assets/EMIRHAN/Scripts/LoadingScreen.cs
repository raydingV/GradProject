using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider LoadingBarFILL;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string[] InfoText;
    private float progressValue;

    public void LoadScene(int sceneID)
    {
        text.text = InfoText[Random.Range(0, InfoText.Length)];
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        loadingScreen.SetActive(true);

        while (progressValue <= 1f && !operation.isDone)
        {
            progressValue = Mathf.Clamp01(operation.progress / 0.1f);

            LoadingBarFILL.value = progressValue;

            yield return null;
        }
    }
}
