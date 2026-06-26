using System;
using System.Collections;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public enum QuestStep { DishWash, GiftFound, PickPhone, FindClue, LookMirror, LookFamilyPhoto, End} //임무 종류
    public QuestStep currentQuest = QuestStep.DishWash; //임무 순서 제어
    private int foundClues = 0; //찾은 단서 개수
    private bool[] clueFoundFlags = new bool[4];
    public PhoneRingPlayer phoneRingPlayer;

    [Header("Objects")]
    public GameObject familyphotoObject;
    private bool familyphotoActivated = false;


    [Header("Transitions")]
    public CanvasGroup fadeCanvas;
    public Transform secondFloorSpawnPoint;
    public Transform FixedPoint;
    private XROrigin xrOrigin;
    private InputActionManager inputActionManager;

    [Header("Triggers")]
    public Collider livingRoomTrigger;
    public Collider chairTrigger;

    [Header("Timeline")]
    public PlayableDirector timelineDirector;

    public NPCLookPlayer sonNPC;

    public GameObject ObjectChild;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        xrOrigin = FindFirstObjectByType<XROrigin>();
        inputActionManager = FindFirstObjectByType<InputActionManager>();
    }
    void Start() //첫 시작시 설거지부터
    {
        if (familyphotoObject != null)
            familyphotoObject.SetActive(false);   // 처음엔 숨김

        fadeCanvas.alpha = 0;
        livingRoomTrigger.enabled = false;
        chairTrigger.enabled = false;

        if (TimerManager.instance != null)
        {
            TimerManager.instance.StartTimer();
            VoiceManager.instance.PlayVoice(0); //설거지 해야겠다 대사
        }
        else
            Debug.LogWarning("TimerManager not found!");
    }

    private void Update()
    {
        CheckfamilyphotoActivation();
    }

    private void CheckfamilyphotoActivation()
    {
        if (familyphotoActivated) return;

        if (ScoreManager.instance.playerScore >= 65 && currentQuest == QuestStep.LookFamilyPhoto)
        {
            familyphotoObject.SetActive(true);
            familyphotoActivated = true;
            Debug.Log("familyphoto 활성화됨");
        }
    }

    private void OnEnable() //임무 이벤트 받기
    {
        EventManager.OnEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventManager.OnEvent -= HandleEvent;
    }


    private void HandleEvent(EventManager.EventType e) //오브젝트 종류 구분
    {
        switch (e) 
        {
            case EventManager.EventType.Plate: OnDishWash(); break;
            case EventManager.EventType.Gift: OnGiftFound(); break;
            case EventManager.EventType.Phone: OnPhone(); break;
            case EventManager.EventType.Clue1: OnClueFound(0); break;
            case EventManager.EventType.Clue2: OnClueFound(1); break;
            case EventManager.EventType.Clue3: OnClueFound(2); break;
            case EventManager.EventType.Clue4: OnClueFound(3); break;
            case EventManager.EventType.Mirror: OnLookMirror(); break;
            case EventManager.EventType.FamilyPhoto: OnLookFamilyPhoto(); break;

        }
    }


    // 각 임무 단계
    public void OnDishWash() //설거지
    {
        if(currentQuest != QuestStep.DishWash) return;
        currentQuest = QuestStep.GiftFound;
        Debug.Log("온디쉬 실행");
        StartCoroutine(DishSequence());
    }

    IEnumerator DishSequence()
    {
        yield return new WaitForSeconds(2f);
        sonNPC.PlayFisrtDialogueClip(); // 학교 다녀 오겠습니다(npc 위치에서)
        //VoiceManager.instance.PlayVoice(sonNPC.questDialogueClips[0]); // 학교 다녀 오겠습니다

        while (sonNPC.IsPlaying)
            yield return null;

        ObjectChild.SetActive(false);

        yield return new WaitForSeconds(2f);
        VoiceManager.instance.PlayVoice(1); //아들 생일 선물 어디뒀더라
    }
    public void OnGiftFound() //생일 선물 찾기
    {
        if(currentQuest != QuestStep.GiftFound) return;
        currentQuest = QuestStep.PickPhone;
        VoiceManager.instance.PlayVoice(2);
        ScoreManager.instance.AddScore(10);
        StartCoroutine(PhoneRing());
    }

    private IEnumerator PhoneRing()
    {
        while (VoiceManager.instance.IsPlaying)
            yield return null;

        yield return new WaitForSeconds(3f);   // 3초 대기
        phoneRingPlayer.PlayRing();
        yield return new WaitForSeconds(1f);
        VoiceManager.instance.PlayVoice(3);
    }

    public void OnPhone() //전화 받기
    {
        if(currentQuest != QuestStep.PickPhone) return;
        currentQuest = QuestStep.FindClue;
        phoneRingPlayer.StopRing();
        ScoreManager.instance.AddScore(10);
        phoneRingPlayer.PlayVoice(sonNPC.questDialogueClips[1]); //아들 대사 (전화기 위치에서)
        //VoiceManager.instance.PlayVoice(4); //아들 대사
        StartCoroutine(PlayDelayVoice());
    }

    private IEnumerator PlayDelayVoice()
    {
        while(phoneRingPlayer.IsPlaying)
            yield return null;

        yield return new WaitForSeconds(3f);
        Debug.Log("3초 후 실행");
        VoiceManager.instance.PlayVoice(4); //아들 방에 가봐야겠어
        Debug.Log("퀘스트 설정 완료");
    }

    public void OnClueFound(int clueIndex)
    {
        if (currentQuest != QuestStep.FindClue) return;
        if (clueFoundFlags[clueIndex]) return;

        clueFoundFlags[clueIndex] = true;
        foundClues++;
        ScoreManager.instance.AddScore(10);

        if ( foundClues >= 4 )
        {
            currentQuest = QuestStep.LookMirror;
        }
        
    }

    public void OnLookMirror() //
    {
        if (currentQuest != QuestStep.LookMirror) return;

        currentQuest = QuestStep.LookFamilyPhoto;
        ScoreManager.instance.AddScore(10);
        VoiceManager.instance.PlayVoice(5); //거울 보고 놀라는 대사
    }

    public void OnLookFamilyPhoto()
    {
        if (currentQuest != QuestStep.LookFamilyPhoto) return;

        currentQuest = QuestStep.End;
        VoiceManager.instance.PlayVoice(6); //치매를 깨닫는 대사
        StartCoroutine(PlayHappyEnding());
    }

    private IEnumerator PlayHappyEnding()
    {
        while (VoiceManager.instance.IsPlaying)
            yield return null;

        fadeCanvas.alpha = 1;
        DisableAllInput();
        yield return new WaitForEndOfFrame();
        TeleportHelper.TeleportPlayer(secondFloorSpawnPoint.position, secondFloorSpawnPoint.rotation);

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(FadeScreen(0f, 2f));
        EnableAllInput();
        VoiceManager.instance.PlayVoice(sonNPC.questDialogueClips[2]); // 아들이 거실로 내려오라는 대사(전체 음향)
        livingRoomTrigger.enabled = true;
    }

    public void OnEnterLivingRoom()
    {
        VoiceManager.instance.PlayVoice(sonNPC.questDialogueClips[3]); // 아들의 의자에 앉아보라는 대사(전체 음향)
        chairTrigger.enabled = true;
    }
    public void OnEnterChair()
    {
        StartCoroutine(StartTimelineSequence());
    }

    private IEnumerator StartTimelineSequence()
    {
        // 1. 이동 멈추기
        DisableAllInput();
        yield return new WaitForEndOfFrame();

        TeleportHelper.TeleportPlayer(FixedPoint.position, FixedPoint.rotation);

        // 5. 타임라인 시작
        timelineDirector.Play();
    }

    private void DisableAllInput()
    {
        if (inputActionManager == null) return;

        // InputActionManager가 관리하는 모든 Action Asset 비활성화
        foreach (var asset in inputActionManager.actionAssets)
        {
            asset.Disable();     // ⭐ 이동/회전/시선 입력 완전히 차단됨
        }
    }
    private void EnableAllInput()
    {
        if (inputActionManager == null) return;

        foreach (var asset in inputActionManager.actionAssets)
            asset.Enable();
    }

    private IEnumerator FadeScreen(float target, float time)
    {
        float start = fadeCanvas.alpha;
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(start, target, t / time);
            yield return null;
        }
        fadeCanvas.alpha = target;
    }
}
