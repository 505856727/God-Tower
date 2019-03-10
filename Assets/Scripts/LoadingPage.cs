using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadingPage : MonoBehaviour
{

    public Slider progressBar;

    // 目标进度
    float target = 0;
    // 读取场景的进度，取值范围0~1
    float progress = 0;
    // 异步对象
    AsyncOperation op = null;

    void Start()
    {
        Debug.Log("开始LoadScene");

        op = SceneManager.LoadSceneAsync("SampleScene");
        op.allowSceneActivation = false;
        progressBar.value = 0;

        // 开启协程，开始调用加载方法
        StartCoroutine(processLoading());
    }

    float dtimer = 0;
    void Update()
    {
        progressBar.value = Mathf.Lerp(progressBar.value, target, dtimer * 0.02f);
        dtimer += Time.deltaTime;
        if (progressBar.value > 0.99f)
        {
            progressBar.value = 1;
            op.allowSceneActivation = true;
        }
    }

    // 加载进度
    IEnumerator processLoading()
    {
        while (true)
        {
            target = op.progress; // 进度条取值范围0~1
            if (target >= 0.9f)
            {
                target = 1;
                yield break;
            }
            yield return 0;
        }
    }

}
