﻿using Between.SceneManagement;
using Between.UI.Base;

namespace Between.UI.Level
{
    public class ToMainMenuButton : UiButton
    {
        protected override void PerformOnClick()
        {
            SceneChanger.ChangeScene(LevelManager.Instance.SceneIndex, 1);
        }
    }
}