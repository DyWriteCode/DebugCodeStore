using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameApp : Singleton<GameApp>
{
    public static BGMManage bgmManage;
    public static ControllerManager ControllerManager;
    public static ViewManager viewManager;
    public static ConfigManager ConfigManager;
    public static CameraManager CameraManager;
    public static MessageCenter MessageCenter;
    public static TimerManager TimerManager;
    public static FightWorldManager FightManager;
    public static MapManager MapManager;
    public static GameDataManager GameDataManager; 
    public static UserInuptManager UserInuptManager;
    public static CommandManager CommandManager;
    public static SkillManager SkillManager;

    public override void init()
    {
        bgmManage = new BGMManage();
        ControllerManager = new ControllerManager();
        viewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
        MessageCenter = new MessageCenter();
        TimerManager = new TimerManager();
        FightManager = new FightWorldManager();
        MapManager = new MapManager();
        GameDataManager=new GameDataManager();
        UserInuptManager=new UserInuptManager();
        CommandManager = new CommandManager();
        SkillManager = new SkillManager();
    }

    public override void Update(float dt)
    {
        UserInuptManager.Update();
        TimerManager.OnUpdate(dt);
        FightManager.Update(dt);
        CommandManager.Update(dt);
        SkillManager.Update(dt);
    }
     
}
