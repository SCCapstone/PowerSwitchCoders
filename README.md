# PowerSwitchCoders
PowerSwitch Coders - Android Application

Unit Testing - Steps
1. Select Unity Branch and download to appropriate folder
2. Download Unity 2017.3.0f3 and open the PowerSwitch2D project
3. Open PowerSwitch2D/Assets/Levels/TestLevel1.unity in the Unity Editor
4. Select Window tab -> Test Runner to launch Unity's Test Runner
5. For behavioral testing, select the PlayMode tab and hit Run all
  5.a: CanFindNextPathPoint and PlayerClicksButton tests should run, FailNoPathPointsTest should fail- this is intentional testing of
  displaying an error from another script
  5.b: Debug log should read "You have clicked the button"
6. For other testing, select the EditMode Tab and hit Run all - all tests should pass
