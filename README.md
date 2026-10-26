# 디멘시아

치매 환자의 인지 혼란을 1인칭 시점에서 체험하는 VR 시뮬레이터입니다. 집 안에서 일상 과제를 수행하는 동안 시각 왜곡·환청·오브젝트 변화 등의 연출로 혼란을 경험하도록 구성했습니다.

## 프로젝트 정보

- 형태: 3인 팀 VR 프로젝트
- 주요 구성: Unity · C# · Meta Quest 3S · Python · Flask · Isolation Forest

이 저장소는 Unity 클라이언트의 주요 동작 스크립트를 정리한 공개본입니다. 아래 구조와 담당 역할은 전체 팀 프로젝트를 기준으로 설명합니다.

## 담당 역할

### 본인 — 맵 구현 / Tech Art / AI 모델

- ProBuilder를 이용한 집 내부 맵과 시작방(세팅룸) 구현
- URP Global Volume 기반 비네팅·렌즈 왜곡·색수차 연출
- Unity Timeline 기반 엔딩 시네마틱 구성
- Scikit-learn Isolation Forest 기반 이상 행동 탐지 모델 학습과 혼란도 점수 산출 로직 구현

## 주요 기능

- VR 오브젝트 잡기와 시선 응시를 통한 상호작용
- 일상 과제 진행에 따른 이벤트·점수·타이머 관리
- 시각 왜곡·환청·오브젝트 변화 연출
- 화면 페이드와 텔레포트를 이용한 공간 전환
- 음성·자막과 Timeline을 연결한 이야기 진행 및 엔딩 연출

## 구조

Unity VR 클라이언트와 Google Cloud의 Python Flask 서버를 연결하는 구조로 구성했습니다. 이동 속도·회전 각도·시선 체류 시간 등의 행동 데이터를 0.5초 간격으로 수집하고, Isolation Forest 모델이 산출한 혼란도 점수를 클라이언트 연출에 반영하는 방식입니다.

최종 프로토타입에서는 일부 기능을 Unity 로컬 로직과 확률 기반 처리로 대체해 체험 흐름을 구성했습니다.

## 기술 스택

- Client: Unity 6, C#, OpenXR, XR Interaction Toolkit
- Environment / Effects: ProBuilder, URP Global Volume, Unity Timeline, DOTween
- AI / Server: Python, Flask, Scikit-learn(Isolation Forest), Google Cloud Platform

## 겪었던 문제

**VR 오브젝트가 선택만 되고 움직이지 않는 문제**  
XR Grab Interactable로 오브젝트를 선택할 수 있었지만 물리적으로 이동하지 않았습니다. Rigidbody와 Grab 설정을 조정해 상호작용을 수정했습니다.

**텔레포트 시 도착 위치가 어긋나는 문제**  
XR Origin과 실제 카메라 위치의 차이로 텔레포트 도착 지점이 어긋났습니다. 목표 방향으로 XR Origin을 회전시키고 카메라가 목표 좌표에 오도록 이동시켜 위치를 보정했습니다.

## 팀 구성

- 강승우: PM / 서버
- 길덕영: 맵 구현 / Tech Art / AI 모델
- 박주성: VR 개발 / 환경 통합
