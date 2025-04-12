# UnityDemo
(｡･∀･)ﾉﾞ嗨，你好，这是我的2D类银河恶魔城测试项目。

创建这个仓库的初衷是为了借此熟悉游戏开发流程，找到游戏开发工作，以便加入未来十年内可能发生的AI赋能游戏的大浪潮。

顺便推销一下自己，本人研究方向为计算机视觉领域下的**人体姿态估计**，可以借助pytorch框架设计新模型；

1. 目前熟练掌握mmpose框架，可以独立完成从训练模型到开发部署一整套流程；
2. 懂得用tensorRT、ONNXRuntime进行模型推理；
3. 会配置和使用包括jetson orin nx在内的nvidia边缘推理设备；
4. 熟练mmdeploy部署框架，目标检测也会，mmdetection和mmcv也会，其他关于计算机视觉（图片生成等）的东西也懂一点，可以快速上手！

**切回正题！**

还不太会markdown，所以界面有点粗糙......

github仓库中放了release包，里面有我打包好的运行程序，**下载打开就能进行游玩**。不过目前还没有达到游玩标准，先标记为v0.1.0，后续我会继续更新的。

目前DEMO的重点如下：**

- ⭐走通unity开发到发布整个流程。
- ⭐⭐掌握2D游戏的基本开发，包括小型敌人和玩家的设计。
- ⭐⭐⭐实现BOSS战和一些特殊机制。

目前已完成的功能如下：dui

- [x] **对象池：**使用继承和多态的模式创建Entity类，并由此迭代开发Enemy和Player。使用对象池对敌人进行管理（增删），使用单例模式管理player。
- [x] **有效状态机：**使用排他有限状态机（即一次只能进入一个状态）设计玩家和敌人。
- [x] 玩家具备基本移动、跳远、蹬墙、三段战斗和反击等基本功能，支持血条以及简单UI显示。
- [x] 敌人具备待机、移动、攻击、可被反击等基本功能，支持血条显示实时血量。
- [x] 基本的地形、背景、菜单UI设置。
- [x] **行为树：**使用行为树+状态机混合，设计BOSS的初步AI逻辑。（BOSS采用空洞骑士的假骑士BOSS战，使用的精灵表是网上开源的）

所涉及的美术资源和代码参考如链接所示：https://www.udemy.com/course/2d-rpg-alexdev/，

在此郑重对发布这个视频的原作者和UP主进行感谢。这是一个相当优秀的unity入门教程，帮助我在短时间内了解unity。

**但我保证，项目中的代码都是我自己在理解后的基础上自己编写的！！！**

部分如敌人视野检测、蹬墙跳逻辑并未参考作者的写法，因为感觉作者的实现逻辑和我的想法有冲突，所以自己重新实现了。

另外原教程中涉及关于玩家的技能树和物品栏等内容我没有去实现，因为**本项目的重点在于设计一场初步具备AI逻辑行为的BOSS战**。据我了解，一些决策树等基本的机器学习算法已经成功运用在了BOSS战上，对这方面进行深入了解比较符合我的兴趣。

后续要继续实现的功能如下：

- [x] 使用行为树进行一场BOSS战的头目AI逻辑。🚀**（已完成）**
- [ ] 使用有限状态机/行为树设计一个队友NPC的行为逻辑。👌
- [ ] 加入BOSS战死亡后进行时间回溯，快速回顾战斗行为并回到初遇BOSS。（因为感觉这个很酷，代码也有点含金量，而且这样可以死了直接重开BOSS战）😋

## 一、完成功能介绍

<p>   <img src="images/player_animation_state.png" width="32%" style="margin-right:2%;"><img src="images/game_start.gif" width="32%" style="margin-right:2%;"><img src="images/player_statemachine.png" width="32%"></p>

通过unity自带的打包工具构建exe游玩程序，并通过动画状态机实现简单的界面淡入淡出效果。展示玩家动画状态机和写的一些脚本，使用State控制状态的进入、更新和退出，stateMachine控制玩家当前状态和下一个状态，player脚本负责调用stateMachine进入状态。

<p>   <img src="images/base_move.gif" width="32%" style="margin-right:2%;">   <img src="images/base_jump.gif" width="32%"> <img src="images/fall_die.gif" width="32%"></p>

玩家可通过wasd进行基本移动操作，后面的背景会随着玩家移动进行移动切换，以实现人物真的在移动的效果；玩家可通过space键进行基本跳跃功能；没踩在ground标记的可碰撞物体时，会下坠，下面绑定了一个标记了trigger的盒子，玩家越过就死了。弹出死亡菜单，点击重新游戏。

<p>   <img src="images/enemy_alert.gif" width="32%" style="margin-right:2%;">   <img src="images/jump_avoid_attack.gif" width="32%"> <img src="images/avoid_attack.gif" width="32%"></p>

当进入敌人视野时，敌人进行警戒状态，会超玩家所在位置进行移动。当判定攻击距离大于检测射线时，发动攻击。（这里是用双射线检测，避免跟在敌人身后又不察觉的问题）。同时使用携程设置了一个被攻击时变白色的效果。

玩家可以通过跳上高台躲避敌人的视野，当检测射线距离过长或敌人警戒时间结束，敌人重新恢复巡逻状态，解除敌意。

玩家可以通过躲避闪开攻击，敌人的攻击范围和动画显示绑定契合，当攻击未触碰到玩家碰撞器时，不会触发伤害。

<p>   <img src="images/combo_attack.gif" width="32%" style="margin-right:2%;">   <img src="images/counter_attack.gif" width="32%"> <img src="images/enemy_die.gif" width="32%"></p>

玩家拥有三段连续攻击，第三段连续攻击会发生轻微位移，以增强打击效果。

玩家可以通过把握敌人攻击帧进行反击。我为敌人设置了一个可被反击的窗口，当窗口出现，敌人会出现红色圆块代表可被反击，此时按下鼠标右键进行反击状态，检测到敌人攻击自动反击，并触发敌人僵直效果。

敌人血条见0，敌人触发死亡状态，同时把敌人丢进对象池，不销毁，以便后续继续使用。

<p>   <img src="images/wall_slide.gif" width="32%" style="margin-right:2%;">   <img src="images/multi_jump.gif" width="32%"> <img src="images/exit_game.gif" width="32%"></p>

当玩家跳跃靠近墙体，进行爬墙动画，保留y轴速度，以实现下滑效果。按住s键，可以加快下滑速度。

实现了基本的蹬墙跳，并优化了手感。

点击esc弹出玩家菜单，点击返回按钮返回主界面，点击退出退出游戏，并保存用户存档。

## 二、使用行为树进行BOSS的设计

<p>   <img src="images/boss_fight.png" width="95%" style="margin-right:2%;"></p>
待施工🚧

目前已完成：BOSS动画状态机，BOSS行为树设计与代码构建。

**（release里的v0.1.1里的BOSS是用状态机写的，新的BOSS战代码还有些bug，改完了再打包上来）**

## 三、未来功能设计

待施工🚧

