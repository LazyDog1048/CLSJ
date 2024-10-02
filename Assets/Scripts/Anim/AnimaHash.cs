using UnityEngine;

namespace game
{
    public static class AnimaHash
    {
        public static int ToAnimHash(string str)
        {
            return Animator.StringToHash(str);
        }
        
        public static readonly int state_Play = Animator.StringToHash("Play");
        
        //Cursor
        public static readonly int state_Normal_Idle = Animator.StringToHash("Normal_Idle");
        public static readonly int state_Normal_Click = Animator.StringToHash("Normal_Click");
        public static readonly int state_Gather_Idle = Animator.StringToHash("Gather_Idle");
        public static readonly int state_Gather_X_Idle = Animator.StringToHash("Gather_X_Idle");
        public static readonly int state_Gather_X_Click = Animator.StringToHash("Gather_X_Click");
        public static readonly int state_Teleport_Idle = Animator.StringToHash("Teleport_Idle");
        
        //Button
        public static readonly int state_Normal = Animator.StringToHash("Normal");
        public static readonly int state_Highlighted = Animator.StringToHash("Highlighted");
        public static readonly int state_Selected = Animator.StringToHash("Selected");
        public static readonly int state_Pressed = Animator.StringToHash("Pressed");
        public static readonly int state_Disabled = Animator.StringToHash("Disabled");
        public static readonly int state_PointUp = Animator.StringToHash("PointUp");
        public static readonly int state_PointDown = Animator.StringToHash("PointDown");
        
        //BtnTutorial
        public static readonly int state_EIdle = Animator.StringToHash("EIdle");
        public static readonly int state_SpaceIdle = Animator.StringToHash("SpaceIdle");
        public static readonly int state_PressE = Animator.StringToHash("PressE");
        public static readonly int state_PressQ = Animator.StringToHash("PressQ");
        public static readonly int state_PressSpace = Animator.StringToHash("PressSpace");
        public static readonly int state_PressWASD = Animator.StringToHash("PressWASD");
        public static readonly int state_ClickLeft = Animator.StringToHash("ClickLeft");
        public static readonly int state_ClickRight = Animator.StringToHash("ClickRight");
        
        //laser
        public static readonly int laser_level = Animator.StringToHash("Level");

        public static readonly int state_Click = Animator.StringToHash("Click");
        public static readonly int state_Idle = Animator.StringToHash("Idle");
        public static readonly int state_Walk = Animator.StringToHash("Walk");
        public static readonly int state_Dead = Animator.StringToHash("Dead");
        public static readonly int state_Attack = Animator.StringToHash("Attack");
        public static readonly int state_MeleeAtk = Animator.StringToHash("Melee_Atk");
        public static readonly int state_RangedAtk = Animator.StringToHash("Ranged_Atk");
        public static readonly int state_Skill = Animator.StringToHash("Skill");
        public static readonly int state_Born = Animator.StringToHash("Born");
        public static readonly int state_Destroy = Animator.StringToHash("Destroy");
        public static readonly int state_Start = Animator.StringToHash("Start");
        public static readonly int state_Sleep = Animator.StringToHash("Sleep");
        public static readonly int state_Shock = Animator.StringToHash("Shock");
        public static readonly int state_Buff_Walk = Animator.StringToHash("Buff_Walk");
        
        //BoneEnemy
        public static readonly int state_Bone_Idle = Animator.StringToHash("Bone_Idle");
        public static readonly int state_Bone_Walk = Animator.StringToHash("Bone_Walk");
        public static readonly int state_Bone_Melee_Atk = Animator.StringToHash("Bone_Melee_Atk");
        public static readonly int state_Bone_Break = Animator.StringToHash("Bone_Break");
        
        //Player_State
        public static readonly int state_Run = Animator.StringToHash("Run");
        public static readonly int state_PickIdle = Animator.StringToHash("PickIdle");
        public static readonly int state_PickRun = Animator.StringToHash("PickRun");
        public static readonly int state_PickUp = Animator.StringToHash("PickUp");
        public static readonly int state_PutDown = Animator.StringToHash("PutDown");
        public static readonly int state_UpgradeIn = Animator.StringToHash("UpgradeIn");
        public static readonly int state_UpgradeOut = Animator.StringToHash("UpgradeOut");
        public static readonly int state_Throw = Animator.StringToHash("Throw");
        public static readonly int state_Disappear = Animator.StringToHash("Disappear");
        public static readonly int state_Appear = Animator.StringToHash("Appear");
        public static readonly int state_Charging = Animator.StringToHash("Charging");
        public static readonly int state_ChargingRecover = Animator.StringToHash("ChargingRecover");
        public static readonly int state_Dizzy = Animator.StringToHash("Dizzy");
        public static readonly int state_Dig = Animator.StringToHash("Dig");
        public static readonly int state_Pull = Animator.StringToHash("Pull");
        public static readonly int state_Throw_Stone = Animator.StringToHash("Throw_Stone");
        public static readonly int state_Roll = Animator.StringToHash("Roll");
        public static readonly int state_Fall = Animator.StringToHash("Fall");
        public static readonly int state_Landing = Animator.StringToHash("Landing");
        public static readonly int state_Dance_1 = Animator.StringToHash("Dance_1");
        public static readonly int state_Dance_2 = Animator.StringToHash("Dance_2");
        public static readonly int state_Dance_3 = Animator.StringToHash("Dance_3");
        public static readonly int state_Dance_4 = Animator.StringToHash("Dance_4");
        public static readonly int state_Dance_5 = Animator.StringToHash("Dance_5");
        public static readonly int state_CoinJump = Animator.StringToHash("CoinJump");
        public static readonly int state_Surprise = Animator.StringToHash("Surprise");
        public static readonly int state_Excavate = Animator.StringToHash("Excavate");

        public static readonly int state_State_1 = Animator.StringToHash("State_1");
        public static readonly int state_State_2 = Animator.StringToHash("State_2");
        public static readonly int state_State_3 = Animator.StringToHash("State_3");
        public static readonly int state_Bala_1 = Animator.StringToHash("Bala_1");
        public static readonly int state_Bala_2 = Animator.StringToHash("Bala_2");
        public static readonly int state_Bala_3 = Animator.StringToHash("Bala_3");
        
        
        //Hero_State
        public static readonly int state_TeleOut = Animator.StringToHash("TeleOut");
        public static readonly int state_TeleIn = Animator.StringToHash("TeleIn");
        public static readonly int state_Skill_1 = Animator.StringToHash("Skill_1");
        public static readonly int state_Skill_2 = Animator.StringToHash("Skill_2");
        public static readonly int state_Skill_3 = Animator.StringToHash("Skill_3");
        public static readonly int state_Skill_3_Attack = Animator.StringToHash("Skill_3_Attack");
        public static readonly int state_Skill_3_Idle = Animator.StringToHash("Skill_3_Idle");
        public static readonly int state_Skill_3_Trigger = Animator.StringToHash("Skill_3_Trigger");
        public static readonly int state_Transform = Animator.StringToHash("Transform");
        
        
        //MG_Tower_State
        public static readonly int state_Idle_Font = Animator.StringToHash("Idle_Font");
        public static readonly int state_Idle_Left = Animator.StringToHash("Idle_Left");
        public static readonly int state_Idle_Right = Animator.StringToHash("Idle_Right");
        public static readonly int state_Idle_Back = Animator.StringToHash("Idle_Back");

        public static readonly int state_CoolDown = Animator.StringToHash("CoolDown");
        
        public static readonly int state_Heat_Attack_Font = Animator.StringToHash("Heat_Attack_Font");
        public static readonly int state_Heat_Attack_Left = Animator.StringToHash("Heat_Attack_Left");
        public static readonly int state_Heat_Attack_Right = Animator.StringToHash("Heat_Attack_Right");
        public static readonly int state_Heat_Attack_Back = Animator.StringToHash("Heat_Attack_Back");
        
        public static readonly int state_Attack_Font = Animator.StringToHash("Attack_Font");
        public static readonly int state_Attack_Left = Animator.StringToHash("Attack_Left");
        public static readonly int state_Attack_Right = Animator.StringToHash("Attack_Right");
        public static readonly int state_Attack_Back = Animator.StringToHash("Attack_Back");
        
        public static readonly int state_Skill_Left = Animator.StringToHash("Skill_Left");
        public static readonly int state_Skill_Right = Animator.StringToHash("Skill_Right");
        
        //Medusa_Tower_State
        public static readonly int state_Attack_1 = Animator.StringToHash("Attack_1");
        public static readonly int state_Attack_2 = Animator.StringToHash("Attack_2");
        public static readonly int state_Attack_3 = Animator.StringToHash("Attack_3");
        
        //Spike_Tower_State
        public static readonly int state_Attack_Idle = Animator.StringToHash("Attack_Idle");
        public static readonly int state_Recover = Animator.StringToHash("Recover");
        
        public static readonly int state_SkillEnter = Animator.StringToHash("SkillEnter");
        public static readonly int state_SkillExit = Animator.StringToHash("SkillExit");
        
        //UpgradeTemple
        public static int state_Enter = Animator.StringToHash("Disappear");
        public static int state_Upgrading = Animator.StringToHash("Upgrading");
        
        //Altar
        public static readonly int state_Active = Animator.StringToHash("Active");
        public static readonly int state_Detection = Animator.StringToHash("Detection");
        public static readonly int state_Inactive = Animator.StringToHash("Inactive");
        public static readonly int state_Trigger = Animator.StringToHash("Trigger");
        public static readonly int state_Enhance = Animator.StringToHash("Enhance");


        //TowerPlace
        public static readonly int state_Blank = Animator.StringToHash("Blank");
        public static readonly int state_Blank_Idle = Animator.StringToHash("Blank_Idle");
        
        //LockItem
        public static readonly int state_Locked = Animator.StringToHash("Locked");
        public static readonly int state_Lock_Click = Animator.StringToHash("Lock_Click");
        public static readonly int state_CanUnLock = Animator.StringToHash("CanUnLock");
        public static readonly int state_Unlock_Click = Animator.StringToHash("Unlock_Click");
        public static readonly int state_Unlocked = Animator.StringToHash("Unlocked");

        //DefensePoint
        public static readonly int state_Skill_Alarm = Animator.StringToHash("Alarm");
        public static readonly int state_Skill_Ready = Animator.StringToHash("Skill_Ready");
        
        //DuckCow 
        // {SmallDuckCowState.Idle, AnimaHash.state_Idle},
        // {SmallDuckCowState.Eat, AnimaHash.state_Eat},
        // {SmallDuckCowState.Wink, AnimaHash.state_Wink},
        // {SmallDuckCowState.Finger, AnimaHash.state_Finger},
        // {SmallDuckCowState.Leave, AnimaHash.state_Leave},
        // {SmallDuckCowState.Fart, AnimaHash.state_Fart},
        // {SmallDuckCowState.Click, AnimaHash.state_Click},
        public static readonly int state_Wink = Animator.StringToHash("Wink");
        public static readonly int state_Finger = Animator.StringToHash("Finger");
        public static readonly int state_Leave = Animator.StringToHash("Leave");
        public static readonly int state_Fart = Animator.StringToHash("Fart");
        public static readonly int state_Eat = Animator.StringToHash("Eat");

        //Turtle
        public static readonly int state_InShell = Animator.StringToHash("InShell");
        public static readonly int state_BackToShell = Animator.StringToHash("BackToShell");

        //UpgradeBar
        public static readonly int state_Max = Animator.StringToHash("Max");
        public static readonly int state_Max_Idle = Animator.StringToHash("Max_Idle");
        
        //Tribesman
        public static readonly int state_Kowtow = Animator.StringToHash("Kowtow");
        public static readonly int state_Dance = Animator.StringToHash("Dance");
        
        //BreakingTile
        public static readonly int state_Before_Idle = Animator.StringToHash("Before_Idle");
        public static readonly int state_After_Idle = Animator.StringToHash("After_Idle");
        
        //GameOverCharacter
        public static readonly int state_PerfectVictoryDance = Animator.StringToHash("PerfectVictoryDance");
        public static readonly int state_PerfectVictory = Animator.StringToHash("PerfectVictory");
        public static readonly int state_Victory = Animator.StringToHash("Victory");
        public static readonly int state_Lose = Animator.StringToHash("Lose");
        
        //GameOverTitle
        public static readonly int state_LoseEnter = Animator.StringToHash("LoseEnter");
        public static readonly int state_LoseIdle = Animator.StringToHash("LoseIdle");
        public static readonly int state_VictoryEnter = Animator.StringToHash("VictoryEnter");
        public static readonly int state_VictoryIdle = Animator.StringToHash("VictoryIdle");

        //Level_1_5_Boss
        public static readonly int state_Stage1_Idle = Animator.StringToHash("Stage1_Idle");
        public static readonly int state_Stage2_Idle = Animator.StringToHash("Stage2_Idle");
        public static readonly int state_Stage1_Enter = Animator.StringToHash("Stage1_Enter");
        public static readonly int state_Stage2_Enter = Animator.StringToHash("Stage2_Enter");
        public static readonly int state_MaskShow = Animator.StringToHash("MaskShow");
        public static readonly int state_MaskHide = Animator.StringToHash("MaskHide");
        public static readonly int state_BossDead = Animator.StringToHash("BossDead");
        public static readonly int state_GameOver = Animator.StringToHash("GameOver");
        
        //Level_1_5_Boss_Tentacle
        public static readonly int state_Skill_Idle = Animator.StringToHash("Skill_Idle");
        public static readonly int state_Skill_Attack = Animator.StringToHash("Skill_Attack");
        public static readonly int state_Skill_Dead = Animator.StringToHash("Skill_Dead");
        public static readonly int state_SpawnTentacle_Attack = Animator.StringToHash("SpawnTentacle_Attack");
        public static readonly int state_SpawnTentacle_Idle = Animator.StringToHash("SpawnTentacle_Idle");
        //ChickDig
        public static readonly int state_Flash = Animator.StringToHash("Flash");
        
        //Alice
        public static readonly int state_Alert_Idle = Animator.StringToHash("Alert_Idle");
        public static readonly int state_Annihilate_Idle = Animator.StringToHash("Annihilate_Idle");
        public static readonly int state_Guard_Idle = Animator.StringToHash("Guard_Idle");
        public static readonly int state_Alert_Attack = Animator.StringToHash("Alert_Attack");
        public static readonly int state_Guard_Attack = Animator.StringToHash("Guard_Attack");
        public static readonly int state_Annihilate_Attack = Animator.StringToHash("Annihilate_Attack");
        public static readonly int state_AlertToGuard = Animator.StringToHash("AlertToGuard");
        public static readonly int state_GuardToAlert = Animator.StringToHash("GuardToAlert");
        public static readonly int state_ToAnnihilate = Animator.StringToHash("ToAnnihilate");
        public static readonly int state_AnnihilateToAlert = Animator.StringToHash("AnnihilateToAlert");
        public static readonly int state_AnnihilateToGuard  = Animator.StringToHash("AnnihilateToGuard");
        
        public static readonly int state_Idle_0 = Animator.StringToHash("Idle_0");
        public static readonly int state_Idle_1 = Animator.StringToHash("Idle_1");
        public static readonly int state_Idle_2 = Animator.StringToHash("Idle_2");
        public static readonly int state_Idle_3 = Animator.StringToHash("Idle_3");
        public static readonly int state_ZeroToOne = Animator.StringToHash("0To1");
        public static readonly int state_OneToTow = Animator.StringToHash("1To2");
        public static readonly int state_TwoToTree = Animator.StringToHash("2To3");
        
        
        public static readonly int state_IsChallenge = Animator.StringToHash("IsChallenge");
        public static readonly int state_IsDown = Animator.StringToHash("IsDown");
        public static readonly int state_IsUp = Animator.StringToHash("IsUp");
        public static readonly int state_IsRight = Animator.StringToHash("IsRight");
        public static readonly int state_IsLeft = Animator.StringToHash("IsLeft");
        
        public static readonly int state_IsRed = Animator.StringToHash("IsRed");
        
        // 
        public static readonly int state_Close = Animator.StringToHash("Close");
        public static readonly int state_Open = Animator.StringToHash("Open");
        // public static readonly int state_Attack_0 = Animator.StringToHash("Attack_0");
        // public static readonly int state_Attack_1 = Animator.StringToHash("Attack_1");
        // public static readonly int state_Attack_2 = Animator.StringToHash("Attack_2");
        
        public static readonly int state_Small = Animator.StringToHash("Small");
        public static readonly int state_Mid = Animator.StringToHash("Mid");
        public static readonly int state_Big = Animator.StringToHash("Big");
        
        //Clown
        public static readonly int state_Ship_Idle = Animator.StringToHash("Ship_Idle");
        public static readonly int state_Ship_Click = Animator.StringToHash("Ship_Click");
        public static readonly int state_Balloon_Enter = Animator.StringToHash("Balloon_Enter");
        public static readonly int state_Balloon_Idle = Animator.StringToHash("Balloon_Idle");
        public static readonly int state_Balloon_Click = Animator.StringToHash("Balloon_Click");
        public static readonly int state_Clown = Animator.StringToHash("Clown");

    }


}