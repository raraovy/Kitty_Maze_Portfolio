using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    //���̴� �̹���
    public Image img;

    //��(Value)�� Ŀ�� ������ ȯ�� ����
    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }


    //�� ���� �� 1�� ���� ���̵� �� ȿ�� (���� �� �̿�) - �ڷ�ƾ
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
