# Guess Who?
## Unity 3D 네트워크 팀 프로젝트

<img width="479" height="265" alt="image" src="https://github.com/user-attachments/assets/159fe990-f610-4fec-a1a0-0008c83300cd" />
<img width="479" height="265" alt="image" src="https://github.com/user-attachments/assets/de5f03d0-3c77-4844-8576-1e6955bcde41" />

------

### 프로젝트 소개📌

장르: 3D 멀티플레이 액션 캐주얼

개발 기간: 25.03.05. ~ 25.04.02 (29일)

개발 인원: 4명

담당 파트: 아이템 스폰 시스템, 플레이어 아이템 사용 로직, 아이템 관련 UI

------

### 기술 상세💡

+ ### 아이템 스폰 시스템

  <img width="839" height="251" alt="image" src="https://github.com/user-attachments/assets/fe6bfcd3-d562-421a-8a18-1c52049f6bbd" />

    ### 핵심 기능

   + ### ItemSpawnManager.cs
 
   + 마스터 클라이언트에서만 실행되며, 아이템 스폰 포인트들을 RoomObject로 생성함
 
     <img width="900" height="283" alt="image" src="https://github.com/user-attachments/assets/69570705-c5c3-4bef-8c47-c2052d5267f0" />

     게임 시작 시 룸에 들어왔는가 & 네트워크에 연결되었는가 여부를 살핀 뒤 아이템 포인트 스폰

   + ### ItemSpawnPoint.cs
 
   + 전체 스폰 포인트들을 관리하며 실제 아이템을 활성화/비활성화함
 
     + #### void RPC_SpawnItems
    
       네트워크에 연결된 모든 클라이언트에서 각 스폰 포인트들에 대해 아이템을 초기화하고, itemNum 기준으로 아이템을 개별 스폰하도록 동기화하는 RPC 함수
       
       <img width="912" height="239" alt="image" src="https://github.com/user-attachments/assets/a61fd8ed-7b74-4a56-987e-d09c3dcded9a" />
       

   + ### ItemSpawnCtrl.cs
 
   + 개별 스폰 지점에서 아이템을 직접 활성화/비활성화하고 리스트 상태를 관리함
    
     플레이어가 아이템을 획득하면 해당 아이템을 비활성화하며, 리스폰 타이머 만료 시 아이템을 세팅함
     
   + ### RPC_ItemSpawn.cs
 
   + 아이템 제어 담당 스크립트로부터 온 RPC를 수신하여, 모든 클라이언트에 아이템 상태를 반영함
 
     + #### void ItemSpawn()
    
       랜덤 숫자와 스폰할 포인트의 정보를 전달받아 해당 포인트에 특정 숫자와 맞는 아이템을 스폰함

------------

+ ### 플레이어 아이템 사용 로직

  플레이어가 획득한 아이템을 바탕으로 무기를 장착하고, 특정 방향으로 투척하는 기능 구현

    ### 핵심 기능

     + #### ScriptableObject 기반 무기 데이터 관리
 
       <img width="659" height="193" alt="image" src="https://github.com/user-attachments/assets/ee6de3b5-0681-400e-8cce-163a4ea8fbb0" />

       플레이어가 아이템 획득 시 해당 무기가 활성화되며, 배열로 구성된 인벤토리에 장착 정보를 저장함.
       
       아이템 정보는 ScriptableObject를 통해 유형/데이터를 관리함
       
       <img width="448" height="145" alt="image" src="https://github.com/user-attachments/assets/72dc1e48-4a6f-4d60-879f-fe5dc76a9d07" />


     + #### 무기 변경
 
       <img width="317" height="168" alt="image" src="https://github.com/user-attachments/assets/54ac49d1-6a6b-4602-a785-f54697d14ba8" />
       <img width="317" height="168" alt="image" src="https://github.com/user-attachments/assets/9cbe45b0-211c-4602-ab3e-b70bd3283b96" />
       
       좌(stone), 우(gun) 등 2번 슬롯에 무기 장착 가능
       
       e키로 무기 슬롯 전환 시 현재 무기에 대응하는 데이터와 상태를 갱신함

       UI에 해당 무기 아이콘이 반영됨

     + ####  Stone(투척 무기)
 
       + #### void StoneThrow
      
         플레이어가 바라보는 방향 기준으로 Stone을 발사함.(TransformDirection 사용)
         
         <img width="851" height="62" alt="image" src="https://github.com/user-attachments/assets/afb767ad-acb7-4924-8c38-98770c353976" />
         
         RPC를 통해 instantiateStone 호출 -> 모든 클라이언트에 투척 애니메이션 및 결과 동기화

     + ####  Gun(원거리 히트스캔 무기)
 
       + #### void ShootPistol
      
         플레이어가 바라보는 방향 기준으로 Ray 발사함. 충돌 대상이 Player라면 RPC를 통해 피해 전달

       + #### void ApplyDamage

         피해 처리 RPC함수. PhotonView ID를 통해 대상을 식별하며, 각 플레이어의 데미지 적용 메서드를 실행시켜 탈락 처리함.


------------------

### 기술 스택 🛠
 
+ #### Engine: Unity (C#)

+ #### 네트워크: Photon Pun2

+ #### 데이터 관리 : ScriptableObject

+ #### UI/UX : Unity UI (Canvas, Sprite)

+ #### Physics : Raycast / Rigidbody

+ #### VFX: ParticleSystem


       
    
