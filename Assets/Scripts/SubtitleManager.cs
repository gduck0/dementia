using UnityEngine;
using TMPro;
using System.Collections;
public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;
    public TextMeshProUGUI subtitleText;
    void Awake()
    {
        instance = this;
    }

    public void ShowSubtitle(string text, float duration) //자막 보여주기
    {
        StopAllCoroutines();
        StartCoroutine(Show(text, duration));
    }
    // Update is called once per frame
    private IEnumerator Show(string text, float duration)
    {
        subtitleText.text = text;
        yield return new WaitForSeconds(duration);
        subtitleText.text = "";
    }
}
