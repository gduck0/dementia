# 디멘시아

치매 환자를 3인칭이 아닌 1인칭 시점에서 체험하는 VR 시뮬레이터. Unity와 클라우드 AI(Isolation Forest)를 연동해서 사용자 행동 패턴을 분석하고, 혼란도에 따라 시각 왜곡과 환청을 동적으로 만들어낸다.

## 구조

```
Unity VR Client (Meta Quest 3S)  <-->  Python Flask AI Server (Google Cloud)
```

VR 기기에서 이동 속도, 회전 각도, 시선 체류 시간을 0.5초마다 수집해서 서버로 보낸다. 서버는 Isolation Forest 모델로 정상 패턴과 비교해 배회·정지·반복 같은 이상 행동을 판별하고 혼란도 점수를 돌려준다. 클라이언트는 그 점수에 비례해서 화면 왜곡, 환청, 오브젝트 이상 현상의 강도를 조절한다.

## 기술 스택

- Unity 6, C#
- OpenXR, XR Interaction Toolkit
- DOTween
- Python, Flask, Scikit-learn(Isolation Forest)
- Google Cloud Platform
- Meshy.ai, Gemini API (에셋/스크립트 생성)

## 폴더

GitHub: https://github.com/gduck0/dementia

## 내가 한 것

Probuilder를 이용한 맵 및 시작방(세팅룸) 구현. URP Global Volume 기반 왜곡 효과(비네팅, 렌즈 왜곡, 색수차)와 Unity Timeline 기반 엔딩 시네마틱 연출. Scikit-learn Isolation Forest 기반 이상 행동 탐지 모델 학습 및 혼란도 점수 산출 로직 구현.

## 겪었던 문제

**VR 오브젝트가 그랩은 되는데 움직이지 않는 문제**
XR Grab Interactable을 붙였는데 선택만 되고 물리적으로 이동을 안 했다. Rigidbody와 Grab 설정을 다시 맞춰서 해결했다.

**텔레포트 시 위치가 어긋나는 문제**
XR Origin 구조에서 카메라 오프셋을 고려하지 않아서 텔레포트하면 위치가 틀어졌다. 카메라 기준으로 오프셋을 보정하는 로직을 추가했다.

## 한계

AI 서버 연동이 불안정해서 일부 기능은 로컬 로직과 확률 기반으로 대체했다. 개발 기간과 인력 제약으로 단일 환경(집 내부) 중심으로만 구현했다.

## 팀

- PM / 서버: 강승우
- 맵 구현, Tech Art, AI 모델: 길덕영
- VR 개발 및 환경 통합: 박주성
