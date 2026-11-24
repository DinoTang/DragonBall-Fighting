# CLAUDE.md: Bộ nhớ dự án DragonBall-Fighting

> File này lưu kết quả phân tích dự án để các phiên Claude sau đọc lại và làm tiếp.
> Phân tích lần cuối: **2026-11-24**. Code có thể đã thay đổi từ đó, nên **kiểm tra lại code trước khi dựa vào một chi tiết cụ thể**.

## Quy ước làm việc với người dùng
- **Luôn trả lời bằng tiếng Việt**, trình bày có cấu trúc và dễ hiểu cho người mới.
- Người dùng là Unity Gameplay Developer.
- **Phân tích trước, chỉ sửa khi người dùng đồng ý.** Không tự ý tạo, sửa, di chuyển hay xoá file.
- Thông tin nào chưa rõ thì nói rõ là cần điều tra, kèm file cụ thể. Không đoán.
- Khi điều tra xong một mục ở phần "Cần điều tra", cập nhật file này (đánh dấu `[x]` và ghi kết quả).

## Tổng quan
- **Unity 6000.4.5f1**, game đối kháng **2D**, 2 người chơi trên cùng 1 máy (**Red = P1**, **Blue = P2**).
- Khoảng 230 script C# (~10.300 dòng). Hiện chỉ có **một nhân vật là Vegeta** (cả 2 bên đều là Vegeta).
- Commit message và comment trong code viết bằng tiếng Việt.

## Cấu trúc thư mục
| Thư mục | Nội dung |
|---|---|
| `Assets/_Data/Scripts` | Toàn bộ code game |
| `Assets/_Data/Prefabs` | `Characters/PlayerRed`, `Characters/PlayerBlue`, `Skills/Vegeta`, `BeamSkill`, `FX`, `UI`, `Environment` |
| `Assets/_Data/Resources` | ScriptableObject nạp bằng `Resources.Load`: `CharacterData/`, `SkillData/Vegeta/` (8 skill), `Sounds/`, `Options/InputSO` |
| `Assets/_Data/Anim, Audio, Sprites, Materials` | Animation, âm thanh, hình ảnh, material |
| `Assets/Scenes` | Build Settings: Splash → MainMenu → CharacterSelect → **SampleScene (scene đánh nhau)** → PostMatch |
| `Assets/Casual_Hit` | Asset bên thứ 3 (có scene demo và script `CameraShake`) |
| `Assets/_Recovery` | Scene do Unity tự sinh khi bị crash |
| `scratch/` | 5 script Python dùng để sinh scene |

**Assembly Definition:**
- `DragonBall.Game`: thư mục gốc `Scripts/`.
- `DragonBall.UI` (`Scripts/UI`): dùng lại Game và TextMeshPro.
- `DragonBall.Editor` (`Scripts/Editor`): chỉ chạy trong Editor.

## Các hệ thống chính
| Hệ thống | Script chính | Nhiệm vụ |
|---|---|---|
| Nền tảng chung | `BaseBehaviour.cs` | Lớp cha của hầu hết script. `Awake()`/`Reset()` gọi `LoadComponent()` để tự tìm và gán reference |
| Nhân vật (hub) | `Character/CharacterCtrl.cs` | Giữ reference tới mọi component con và tới `Opponent` |
| Component con | `Character/CharacterAbstract.cs` | Tự tìm `CharacterCtrl` bằng `GetComponentInParent` |
| State Machine | `Manager/StateManager.cs`, `State/**` (~70 state) | Idle, Move, Air, Attack, Crouch, Hurt/Knock, Skill... Cây kế thừa: `State` → `CharacterState` → các state cụ thể |
| Input | `Manager/InputManager/*` | Singleton. Mỗi player có một `IPlayerInputProvider` riêng (Keyboard / Gamepad / NewInputSystem). Phím cấu hình trong `InputSO` (`PlayerInputConfig`) |
| Combo / Skill | `CharacterCombo.cs`, `InputBuffer.cs`, `CharacterSkillManager.cs`, `Object/SkillWithCooldown.cs` | Ghép phím thành chuỗi → so khớp → tung skill |
| Ki | `CharacterKiManager.cs`, `Configs/KiBalanceConfig.cs` | Cộng/trừ Ki theo enum `KiChangeReason` |
| Sát thương | `DamageSender`/`DamageReceiver`, `CharacterDamageSender`/`CharacterDamageReceiver`, `Projectile/*DamageSender` | Va chạm trigger → trừ máu → chuyển sang state bị đánh |
| Pooling | `Spawner/Spawner.cs` (`Spawner<T>`), `Spawner/Despawn/Despawn.cs` (`Despawn<T>`), `Object/PoolObj.cs` | Tái sử dụng FX và projectile. Prefab gốc đặt dưới object con tên `"Prefab"` |
| Trận đấu | `Manager/GameManager.cs` | Máy trạng thái trận Intro → Ready → Fight → GameOver. Phát static event |
| Phiên chơi | `Manager/GameSession.cs` | Lazy singleton, `DontDestroyOnLoad`: chuyển scene, lưu nhân vật đã chọn và kết quả trận |
| Super Freeze | `Manager/SuperFreezeManager.cs` | Dừng đối thủ, làm tối màn hình khi tung Ultimate |
| Camera / Audio | `Manager/CameraManager.cs`, `Manager/AudioManager.cs` | Bám theo, zoom, rung màn hình; phát nhạc nền, SFX, giọng announcer (âm thanh nạp từ SO trong `Resources/Sounds`) |
| UI | `UI/UIManager.cs`, `HealthBarUI`, `ManaBarUI`, `UIClock`, `UIHitCount`, `UI/Navigation/*` | HUD trong trận và các màn hình menu |

**ScriptableObject:**
- `CharacterDataSO` (lớp con `VegetaDataSO`): portrait, animator, readyClip, skillList, voice.
- `CharactersDataSO`: bảng tra từ enum sang SO.
- `CharacterSelectSO`, `InputSO`.
- `SkillDataSO`, với các lớp con: `SkillChargeSO` → `BeamSkillDataSO` → `UltimateSkillDataSO`, và `RushAttackSkillSO`, `TeleportPunchSkillSO`.
- Sound SO: `AnnouncerSoundSO`, `CharacterSoundSO`, `UISoundSO`.

## Cách các hệ thống giao tiếp
```
InputManager ──poll──> State.OnUpdate()       ──SetNextState──> StateManager
             └─poll──> CharacterCombo.Update  ──TryUseSkill───> CharacterSkillManager ─> CharacterKiManager
Animation Event ─> CharacterAnimEvent ─> bật hitbox (CharacterDamageSender) / SkillProjSpawner.Spawn
DamageSender ─OnTrigger─> DamageReceiver.DeductHP ─> OnHurt ─> StateManager của đối thủ (HurtState)
GameManager ─static events (OnMatchReadyPhase/FightPhase/FightStarted/GameOver)─> UIManager
UIManager / States ─đọc trực tiếp─> GameManager.Instance (Winner, TimerMatch, PlayerIsHit...)
```
Có 3 kiểu giao tiếp:
1. Gọi trực tiếp qua singleton `.Instance` (phổ biến nhất).
2. Static event, chỉ dùng ở `GameManager`.
3. Animation Event: tên method trong file `.anim` phải khớp với method trong `CharacterAnimEvent`.

## Luồng thực thi

### Luồng scene
1. `SplashScreenUI` → `GameSession.GoToMainMenu()`.
2. `MainMenuUI` → `GoToCharacterSelect()`.
3. `CharacterSelectUI.OnStartBattleClicked()` → `StartBattle(red, blue)`.
4. Vào `SampleScene`.
5. `GameManager.CanShowResult()` → `GoToPostMatch()`.
6. `PostMatchUI` cho chọn Rematch, chọn lại nhân vật, hoặc về Menu.

`GameSession` và `SceneTransitionFader` **không có sẵn trong scene nào**; chúng tự tạo lần đầu có code truy cập `Instance`.

### Khởi tạo trận (SampleScene)
1. `GameManager.Awake` → `LoadComponent`:
   - Nạp `CharactersDataSO` và `CharacterSelectSO`.
   - `LoadOpponentCharacter()`: tìm 2 `CharacterCtrl` qua component con `PlayerRed`/`PlayerBlue`, gán `Opponent` cho nhau, gán `CharacterDataSO` (lấy từ `GameSession` nếu có).
2. `GameManager.Start` → `InitializeMatch()`: reset máu, đồng hồ và các cờ.
3. `StateManager.Start` đưa nhân vật vào `IdleState`.
4. `GameManager.Update` → `HandleMatchState()`:
   - **Intro:** chạy `CharacterModalCtrl.StartIntroCharacter`. Bấm Space hoặc Enter để bỏ qua.
   - **Ready:** coroutine phát "Ready", đợi 1,5s, phát "Fight", đợi 0,5s rồi sang `Fight`.
   - **Fight:** đếm ngược 90s (tạm dừng khi Super Freeze). Hết giờ hoặc có người chết thì sang GameOver.
   - **GameOver:** chọn người thắng, `RecordGameSessionResult`, KO thì chạy slow-motion. Sau 3,5s tự chuyển sang PostMatch.

### Vòng lặp mỗi frame của nhân vật (`StateManager.Update`)
1. Nếu `isFrozen` thì bỏ qua frame.
2. Đặt `mainStateType` theo `CharacterCollision.IsGround`: trên đất là `IdleState`, trên không là `AirState`.
3. Nếu có `nextState` thì gọi `SetState`: `OnExit` state cũ, rồi `OnEnter` state mới.
4. Gọi `CurrentState.OnUpdate()`. State đọc input rồi gọi `SetNextState`. Lần chuyển state áp dụng ở **frame sau**.

State đếm thời gian bằng biến `timed`. Phần lớn kết thúc khi `timed >= duration`, rồi gọi `SetNextStateToMain()`.

### Đòn đánh thường
1. `IdleState` (bấm Hit) → `GroundHit1State`.
2. Animator nhận `IsAttack` và `AttackCounter`.
3. Animation Event `EnableHitbox` bật collider của `CharacterDamageSender`.
4. `OnTriggerEnter2D` chỉ xử lý khi trúng đúng `Opponent` → `DeductHP`.
5. Bên nhận đòn:
   - Nếu đang block: FX parry, cộng Ki, không mất máu.
   - Nếu không: `TakeDamage` → `OnHurt` → `HurtState` (hoặc `AirHurtState`), cộng Ki.
6. Bên đánh: cộng Ki, sinh FX `Hit_Effect`, phát âm thanh, tăng `hitCounts` (cho UI combo), tắt collider.
7. Đánh nối Hit1 → Hit2... dựa vào cờ `shouldCombo`, được set ở `BaseGroundHitState` (chưa đọc chi tiết).

### Tung skill
1. `CharacterCombo.Update` thêm phím vào `InputBuffer`. Phím hết hạn sau 0,5s. Chuỗi combo là tên enum `InputKey` nối bằng `"+"`.
2. Bảng combo, có xét hướng mặt (`localScale.x`):

| Combo | Skill |
|---|---|
| `Skill` | BasicProjectile (Ground khi đứng đất, Air khi trên không) |
| Tiến + `Skill` | Skill_1 |
| Lùi + `Skill` | Skill_2 (beam) |
| `Down+Tiến+Hit` | Skill_4 |
| `U` | Skill_5 (Rush Attack) |
| `O` | Burst |
| `Down+Skill` | Ultimate |

3. `CharacterSkillManager.TryUseSkill`: kiểm tra cooldown và Ki, `Animator.Play(clip)`, trừ Ki.
4. `SkillDataSO.GetSkillState()` trả về state tương ứng (đa hình): `EnergySkillState`, `BeamSkillState`, `UltimateSkillState` hoặc `RushAttackSkillState`.
5. `SkillState.OnEnter` khoá Rigidbody.
6. Animation Event (ví dụ `EnableSkillOne`) gọi `SkillProjSpawner` spawn projectile từ pool. Beam ghép từ head + body×N + tail.
7. `BaseSkillDamageSender` → `DeductHPBySkill`. Luật block:
   - Skill `Normal`: chỉ nhận `damage × blockDamageMultiplier`.
   - Skill `Medium`/`Hard`: block tốn `guardKiCost`; không đủ Ki thì guard break.
8. `SkillState.OnExit`: bắt đầu cooldown, xoá `currentSkill`, mở khoá Rigidbody.

### Ultimate và Super Freeze
- `UltimateSkillState` chạy 6 pha: StartUp → CutSceneReady → Ready → CutScenePerform → Perform → Finish.
- `SuperFreezeManager.TriggerSuperFreeze`: đặt `Animator.speed = 0` cho đối thủ, khoá Rigidbody, set `StateManager.IsFrozen`, hiện lớp phủ tối.
- Kết thúc ở pha Perform hoặc qua animation event `EnableSkillUltimate`.
- Trong lúc freeze: dừng đồng hồ, tạm dừng `Despawn` và projectile.

## Vấn đề đã xác minh trong code (chưa sửa)

### Mức cao
1. **Có thể lỗi khi Build.** `Scripts/Character/CharacterModalCtrl.cs:3` có `using UnityEditorInternal;` trong assembly runtime.
2. **Chọn nhân vật không có tác dụng thật.** `GameSession.StartBattle()` chỉ lưu enum, còn `GameManager.LoadOpponentCharacter()` dùng `GameSession.RedCharacterDataSO/BlueCharacterDataSO`, luôn là Vegeta mặc định. Ngoài ra còn nguồn thứ ba là `CharacterSelectSO`.
3. **Phụ thuộc thứ tự `Awake`.** `CharacterSkillManager.LoadSkillListSO()` và `CharacterModalCtrl.LoadData()` đọc `CharacterDataSO`, trong khi `GameManager.Awake` mới gán giá trị này. Ngoài ra `skillList` đã serialize (`Count > 0`) sẽ bỏ qua bước nạp.
4. **SO giữ trạng thái lúc chạy.** `RushAttackSkillSO.hasPlayedSound` dùng chung cho 2 player và còn giữ lại giữa các lần Play trong Editor.
5. **Hạng sau trận sai.** `GameManager.RecordGameSessionResult` dùng ngưỡng HP 80/40, trong khi `hpMax` mặc định 10000, nên gần như luôn "S RANK". `MaxCombo = 10` và `"(SUPER SAIYAN)"` đang hardcode.
6. **Block skill không kiểm tra chết.** `CharacterDamageReceiver.ApplyDamageBySkillWhenBlock` trừ máu nhưng không gọi `IsDead()`.

### Mức trung bình
7. **Một phím xử lý ở 2 nơi.** Phím Hit vừa làm `IdleState` chuyển state, vừa vào combo buffer. Bên nào thắng phụ thuộc thứ tự Update.
8. **`CharacterCombo.Update` chỉ kiểm tra `IsGameOver`**, không kiểm tra `MatchState == Fight`, nên có thể tung skill lúc Ready/Intro (chưa xác nhận).
9. **`SkillTwoCombo()` set `IsBeaming = true` mỗi frame** khi chưa có skill nào đang chạy.
10. **Tạo state bằng `new` mỗi lần chuyển**, sinh rác GC. Idle và Air được cache nhưng `timed` không bao giờ reset.
11. **Chuỗi kiểm tra `is X || is Y` rải rác** ở `CharacterCollision.CanEnterIdleState`, `CharacterFlip.Flipping`, `PreventStandingOnEnemy`. Thêm state mới phải sửa nhiều chỗ.
12. **Gắn chặt với Vegeta:**
    - `UltimateSkillState` ép kiểu sang `VegetaDataSO`.
    - `SkillProjSpawner` nạp skill theo `CharacterSelector` gắn trên spawner (chỉ một nhân vật).
    - `TeleFXNameEnum.Vegeta` được hardcode.
13. **Âm thanh nhân vật dùng chung.** `AudioManager.PlayCharacterSound(type)` không phân biệt nhân vật hay player.
14. **Hằng số hardcode trong state:** `MoveState.speed = 15`, `JumpState.jumpForce = 40`, `duration = 0.3f`.
15. **Singleton không thống nhất:**
    - `GameManager` và Spawner chỉ cảnh báo khi trùng, không Destroy bản thừa.
    - `GameSession`, `SuperFreezeManager`, `SceneTransitionFader` tự tạo nếu chưa có.
    - `AudioManager` tự Destroy bản trùng.
16. **Dữ liệu trùng lặp:**
    - `BlastSkillProjectile.owner` và `SkillProjCtrl.Owner`.
    - `CharacterDataSO.skillList` và `SkillProjSpawner.skillList`.
17. **Nhiều `Debug.Log`** trong `Load*()`, va chạm, đòn đánh.
18. **Phím debug:** `R` restart trận bất cứ lúc nào. Space/Enter sang PostMatch trùng chức năng với việc tự chuyển sau 3,5s.
19. **Nhiều code comment-out**, và có method input cũ hardcode phím trong `InputManager` (ví dụ `GetLeftInput()` không tham số).

## Cần điều tra thêm
- [ ] Script Execution Order trong `ProjectSettings/` (liên quan vấn đề 3 và 7).
- [ ] Hierarchy thật của `Prefabs/Characters/PlayerRed.prefab`. Lưu ý `CharacterCollision` có `[RequireComponent(typeof(StateManager))]`; nếu nó nằm ở object con thì có thể bị thêm một `StateManager` thứ hai.
- [ ] Có 2 file `PlayerRed.prefab` (`Prefabs/` và `Prefabs/Characters/`): file nào đang được dùng?
- [ ] Có 2 asset `Vegeta.asset` và `VegetaDataSO.asset` trong `Resources/CharacterData`: `CharactersDataSO` trỏ tới asset nào?
- [ ] Giá trị serialize thực tế (`hpMax`, `KiBalanceConfig`, phím trong `InputSO`) trong prefab và scene.
- [ ] Active Input Handling trong Player Settings (code dùng cả legacy `Input` và `NewInputSystemProvider`).
- [ ] Đọc kỹ: `BaseGroundHitState` (`shouldCombo`), `RushAttackSkillState`, `SuperDashState`, các state Hurt/Knock, `CameraManager.Following/Zooming`, `UIManager` (từ dòng 110), `FrameRateManager`.
- [ ] Tool Editor: `Editor/DragonBallSceneGenerator.cs` (941 dòng, sinh scene và **ghi đè Build Settings**), `Editor/PrefabHierarchyConverter.cs`, `Editor/SuperFreezeTestRunner.cs`.
- [ ] Kiểm tra tên Animation Event trong các file `.anim` có khớp method của `CharacterAnimEvent` không (ví dụ `DisablenockUp` được giữ lại cho event cũ).
- [ ] `scratch/*.py` và `Assets/_Recovery/` còn cần không?

## Bước tiếp theo đề xuất
- (a) Điều tra các mục ở phần trên.
- (b) Lập kế hoạch sửa các vấn đề mức cao.

Mọi thay đổi code đều cần người dùng đồng ý trước.
