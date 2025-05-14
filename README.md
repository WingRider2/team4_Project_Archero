# 🧩 프로젝트 개요
이 프로젝트는 Unity 기반 2D 으로 제작한 궁수의 전설 클론 코딩으로, 플레이어의 스탯 성장, 다양한 스킬 조합, 몬스터 AI, 디버프 및 전투 시스템, 데이터 저장/로드, 풀링 최적화, UI, 사운드 시스템 등 게임 전반의 핵심 시스템을 직접 구현한 프로젝트 입니다.
SOLID 원칙과 재사용성을 고려한 구조 설계 및 구현을 중점으로 합니다.

---

# 🧱 주요 시스템 구성
### ✅ Stat 및 Buff 시스템

BaseStat, PlayerStat, MonsterStat 구조 분리
EquipmentValue, BuffValue, BaseValue 조합으로 최종 스탯 계산
PlayerStatManager 및 MonsterStatManager로 세분화된 제어
스탯 변화 시 이벤트 트리거 (OnValueChanged)

---

### ✅ 디버프 및 상태 이상 시스템
IDebuffSkill 인터페이스 기반 설계
Burn, Poison, Slow 등 상태이상 코루틴으로 처리
중복 적용 시 지속시간 갱신

---

### ✅ 스킬 시스템
ISkill, IAngleArrowSkill, IDebuffSkill 등 인터페이스 기반 구조
TripleArrowSkill, FireArrowSkill, StatSkill 등 다양한 확장 스킬 구현
각 스킬은 전투 중 실시간 선택 및 적용 가능

---

### ✅ 몬스터 AI (Behavior Tree)
INode 기반 Custom BT 구조 직접 구현
SelectorNode, SequenceNode, CoolDownNode, InverterNode 등 핵심 노드 완비
BossBT, CloseSimBT 등 몬스터 특성에 따른 상속 기반 트리 구성
EaFindNode를 활용한 A* 경로 탐색 및 장애물 회피 구현

---

### ✅ 오브젝트 풀링
IPoolObject 기반 풀 등록 및 생성
ObjectPoolManager는 SceneOnlyManager 상속으로 씬별 독립적 관리
HPBarUI, Projectile, HealthBar 등 다양한 오브젝트 풀링 적용

---

### ✅ 사운드 시스템
SoundManager, UnitSoundBase, SoundSource 설계
AudioClip[] 기반으로 랜덤 재생 / 루프 재생
각 유닛별 사운드 스크립터블 객체 분리로 재사용성 향상

---

### ✅ 데이터 저장/복호화
SaveManager에서 AESUtil을 활용한 저장 파일 암호화
JsonConvert로 객체 직렬화 후 저장
게임 종료 시 자동 저장, 시작 시 자동 로드 처리

---

### ✅ 퀘스트 시스템
QuestManager를 통해 조건 타입별 맵핑 구조 (QuestConditionType)
UpdateCurrentCount()로 스탯/보스 처치/챕터 클리어 등 연동
업적형/일일형/주간형 QuestType 지원(일일, 주간 추후 개발 예정)

---

### ✅ UI 및 전투 인터페이스
UIManager_Main, UIManager_Battle 구조 분리
해상도에 따른 UI 비율 유지
레벨업/게임오버 등 상황별 패널 활성화 및 텍스트 크기 통일

---

# 🧠 사용한 디자인 패턴
패턴	          사용 위치

|패턴|사용 위치|
|------|---|
|Singleton|GameManager, SaveManager, SoundManager, TableManager, 등
|Factory|StatFactory(), CreateSkill() 내부|
|Strategy|ISkill, IDebuffSkill, IAngleArrowSkill|
|Composite|SequenceNode, SelectorNode 등 BT 구조|
|Decorator|CoolDownNode, InverterNode 등 BT 트리|
|ObjectPool|ObjectPoolManager, IPoolObject|

