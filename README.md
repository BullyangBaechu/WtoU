# Project Arrow🏃‍♂️ Project WtoU

Unity 3D Endless Runner Prototype

3D 무한 러너 시스템을 직접 구현하는 프로젝트.
Ground Slice 재사용, 장애물 스폰, 충돌 처리, 아이템 랜덤 박스, SlowZone 감속 시스템 등
엔진 기능과 게임 로직을 직접 구성하는 학습 중심 프로젝트.

📌 프로젝트 개요

Project WtoU는 Unity 기반 3D Endless Runner 스타일의 프로토타입이다.

Lane 기반 이동

무한 GroundSlice 스트리밍

Obstacle / RandomBox 동적 스폰

SlowZone & BlockWall 충돌 처리

난이도에 따라 스폰 거리 자동 조절

Object Pooling 구조 설계

을 목표로 하여, “엔진 기능에 의존하지 않고 직접 Endless Runner 시스템을 구현하는 능력"을 보여준다.

🚀 주요 기능
✔ 1. 무한 Ground Slice 시스템

GroundSlice 프리팹을 Z축 방향으로 계속 배치

플레이어가 지나간 Slice는 뒤로 재배치하여 재사용 (Object Pooling 개념)

GetFrontMostZ()를 통해 가장 앞 Slice 위치 계산

Ground 밖은 Skybox + Fog로 공중처럼 연출

✔ 2. Lane 기반 플레이어 이동

중심 라인 기준으로 -1 / 0 / +1 3 Lane 이동 구현

좌/우 입력 → targetPosition 계산 → 부드러운 이동

Rigidbody 기반 충돌 처리 + GroundLayer 체크로 점프 판단까지 처리 가능 구조

✔ 3. 오브젝트 스폰 시스템
🟦 ObstacleManager

BlockWall, MovingObject 등 장애물 스폰

충돌 범위/위치 검사 → 겹치지 않도록 배치

worldProgress 기반으로 현재 진행 거리 확인

🟨 RandomBoxManager

랜덤 상자 스폰

SlowZone, SpeedUp 등 버프/디버프 요소 확장 가능

🟪 SpawnDirector (난이도 시스템 핵심)

