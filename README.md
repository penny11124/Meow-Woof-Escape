# 🐱🐶 喵汪夜密逃 Meow & Woof: Midnight Escape

> 一款雙人同機合作的2D解謎冒險遊戲。玩家分別操控一隻貓與一隻狗，從可怕的寵物店一路逃出，途中必須分工合作、同步行動，才能突破重重關卡，並走向不同的結局。

> 2024 Game Programming Final Project

![Unity](https://img.shields.io/badge/Engine-Unity%202021.3-000000?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)
![URP](https://img.shields.io/badge/Render-URP%202D-blue)
![Players](https://img.shields.io/badge/Players-2P%20Local%20Co--op-orange)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows&logoColor=white)

---

## Demo 影片

[![喵汪夜密逃 Demo](https://img.youtube.com/vi/icht55iK8UQ/hqdefault.jpg)](https://youtu.be/icht55iK8UQ)

點擊圖片或前往 YouTube 觀看：<https://youtu.be/icht55iK8UQ>

## 故事背景

一間看似歡樂的寵物店，每天都有許多小孩來買新寵物，但這些寵物最後總是被養死。店裡的動物們都想逃離這個可怕的地方。某天，趁店員打開籠子的瞬間，狗衝了出去，載著貓一起展開逃亡……

## 遊戲特色

- **雙人同機合作**：兩位玩家共用一組鍵盤，分別操控貓與狗。許多機關必須兩人同時按下互動鍵才能觸發，考驗默契與溝通。
- **角色分工**：貓與狗有各自的任務與道具，例如由狗踩住按鈕撐起牆壁讓貓通過、由貓推倒櫃子壓制店員。
- **多種玩法**：每一關的核心玩法都不同，包含限時逃脫、潛行躲避監視器，以及引導 NPC 的生存挑戰。
- **劇情與多重結局**：關卡之間穿插打字機效果的劇情過場，第二關的選擇會決定玩家走向一般結局或真結局。

## 操作方式

| 動作 | 1P 貓 🐱 | 2P 狗 🐶 |
| --- | --- | --- |
| 左右移動 | `A` / `D` | `←` / `→` |
| 跳躍 | `W` | `↑` |
| 攀爬（梯子上） | `W` / `S` | `↑` / `↓` |
| 互動 / 拿取道具 | `Space` | `Enter` |
| 暫停選單 | `Esc` | `Esc` |

## 關卡介紹

**教學關**
熟悉兩隻角色的操作，學習拿取鑰匙、兩人合力推倒高櫃等基本互動。

**第一關：逃出寵物店**
在 200 秒內讓貓與狗都抵達出口。架上的物品會在角色靠近時掉落，被砸中會扣除時間；被店員抓到則直接失敗。玩家需要利用場景機關，例如讓狗踩住按鈕升起牆壁、推倒櫃子砸向店員，才能順利脫逃。

**第二關：店長家**
誤闖店長家的貓狗必須躲過來回巡邏的店長、店長的兒子，以及會以扇形視野掃描的監視器，一旦被發現就會被送回原點。畫面採左右分割，兩位玩家各自探索、收集道具；玩家可以選擇直接開門逃離，或是挑戰更困難的路線，觸發不同結局。

**第三關：救援行動**
麻煩的小鬼逃進了危機四伏的禁地！他每隔一段時間會隨機轉向面對貓或狗，玩家必須在對的時機舉起他喜歡的「魚」吸引他靠近，或舉起他討厭的「骨頭」把他趕開，讓他閃避從天而降的障礙物。小孩只有 3 顆愛心，撐過 60 秒即可過關，而且障礙物會隨時間越來越密集。

## 執行方式

1. 安裝 [Unity Hub](https://unity.com/download) 與 **Unity 2021.3.5f1**（建議使用相同版本以避免相容性問題）。
2. 下載專案：
   ```bash
   git clone https://github.com/penny11124/game-programming-final-project.git
   ```
3. 在 Unity Hub 選擇「Add project from disk」，開啟專案資料夾。
4. 開啟 `Assets/Scenes/HomePage.unity`，按下 Play 即可從主選單開始遊戲。

## 專案結構

```
Assets/
├── Scenes/                 # 遊戲場景（依遊玩順序）
│   ├── HomePage            # 主選單
│   ├── train               # 教學關
│   ├── FirstScene          # 第一關：逃出寵物店
│   ├── second_stage        # 第二關：店長家
│   ├── ThirdScene          # 第三關：救援行動
│   ├── TrueEnd             # 真結局
│   └── *_to_*              # 關卡間的劇情過場
├── Scripts/
│   ├── PlayerControl/      # 角色移動、跳躍、動畫狀態機
│   ├── General/            # 場景轉場、暫停選單、解析度設定
│   ├── train/              # 教學關機關
│   ├── FirstScene/         # 第一關：計時器、掉落物、店員、按鈕牆
│   ├── SecondStageScripts/ # 第二關：巡邏 NPC、監視器偵測、攀爬、分割畫面
│   ├── FallScene/          # 第三關：障礙物生成、道具持有、生命值
│   └── TrueEnd/            # 結局畫面
├── Settings/               # 貓與狗的 Input System 按鍵設定
├── Cat & Dog/ Sprites/ Animation/  # 角色與場景美術、動畫
└── ...                     # 第三方素材包
```

## 技術重點

- **雙人輸入**：使用 Unity 新版 Input System，為貓與狗各自建立一組 Input Actions，讓兩位玩家能在同一鍵盤上獨立操作。
- **同步互動判定**：多數合作機關會同時檢查「兩位玩家是否都在範圍內」與「是否都按下互動鍵」，才觸發事件。
- **扇形視野偵測**：監視器以 `Physics2D.OverlapCircleAll` 搭配角度計算判斷角色是否進入視野，並即時產生半透明扇形 Mesh 顯示偵測範圍。
- **動態難度**：第三關依剩餘時間切換難度階段，逐步縮短障礙物生成間隔並增加生成位置。
- **場景轉場**：以單例的 `SceneTransitionManager` 統一管理淡入淡出與場景切換，劇情過場使用打字機效果並可跳過。

## 素材來源

本專案使用了以下 Unity Asset Store 免費素材：Free Wood Door Pack、LowPolyMetalRack、TDG Storage Solutions、karsiori Key Pack
