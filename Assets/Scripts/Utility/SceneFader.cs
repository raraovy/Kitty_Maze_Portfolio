using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    //페이더 이미지
    public Image img;

    //값(Value)을 커브 값으로 환산 대응
    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }


    //씬 시작 시 1초 동안 페이드 인 효과 (알파 값 이용) - 코루틴
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a); //R, G, B, ALPHA

            yield return null;
        }
    }

    public void FadeTo(string sceanName)
    {
        StartCoroutine(FadeOut(sceanName));
    }


    IEnumerator FadeOut(string sceanName)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);

            yield return null;
        }
        SceneManager.LoadScene(sceanName);
    }


}
