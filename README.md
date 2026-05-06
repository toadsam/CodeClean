# CodeClean

프로젝트를 진행하며 작성한 C# 코드와 리팩터링 학습 내용을 모아둔 저장소입니다. 퍼즐 기믹, 이동 오브젝트, 회전 오브젝트, 인터페이스 분리 등 게임 로직을 정리하는 데 초점을 둡니다.

## 프로젝트 개요

`CodeClean`은 완성 게임 저장소라기보다, 구현했던 기능을 다시 읽기 좋게 정리하고 구조를 개선하는 코드 아카이브입니다. Unity/C# 프로젝트에서 자주 등장하는 상호작용 오브젝트와 퍼즐 로직을 작은 단위의 클래스로 나누어 관리합니다.

## 주요 코드 영역

- 이동 가능한 오브젝트와 회전 가능한 오브젝트 인터페이스
- 레버와 이동 플랫폼 로직
- 퍼즐 버튼, 패턴 사인, 제한 시간 처리
- 낙하 오브젝트와 장애물 처리
- 투명화 관련 스크립트
- 스테이지 관리 코드

## 기술 스택

- C#
- .NET 6 형태의 코드 정리 구조
- Unity 게임 로직 기반 스크립트

## 폴더 구조

```text
.
├── README.md
└── TSEROFCodeClean/
    └── TSEROFCodeClean/
        ├── Interface/
        ├── Manager/
        ├── PuzzleGimmick/
        ├── Program.cs
        └── *.cs
```

## 코드 읽는 순서

1. `Interface/IMovable.cs`, `Interface/IRotatable.cs`에서 공통 동작 계약을 확인합니다.
2. `MovingPlatform.cs`, `RotationCube.cs`, `RotationObstacle.cs`에서 오브젝트 동작 구현을 봅니다.
3. `PuzzleGimmick/` 아래에서 퍼즐 버튼과 패턴 처리 흐름을 확인합니다.
4. `Manager/Stage2Manager.cs`에서 스테이지 단위 제어 코드를 확인합니다.

## 실행 및 활용

이 저장소는 코드 정리와 복습 목적이 강합니다. 실제 Unity 프로젝트에 적용하려면 필요한 스크립트를 Unity 프로젝트의 `Assets/Scripts` 아래로 옮기고, 씬의 GameObject에 컴포넌트로 연결해야 합니다.

## 개선 아이디어

- 각 스크립트별 역할을 XML 주석으로 보강
- Unity 씬 예시 추가
- 퍼즐 기믹별 사용 예시 GIF 또는 이미지 추가
- 인터페이스 기반 구조를 적용한 Before/After 설명 추가
