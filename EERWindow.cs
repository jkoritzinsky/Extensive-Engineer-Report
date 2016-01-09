using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static JKorTech.Extensive_Engineer_Report.ConcernUtils;
using static JKorTech.Extensive_Engineer_Report.KSPExtensions;

namespace JKorTech.Extensive_Engineer_Report
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class EERWindow : KSPPluginFramework.MonoBehaviourWindow
    {

        private const int WindowWidth = 400, WindowHeight = 600;
        private ApplicationLauncherButton button;
        private Vector2 scrollPos;
        private static readonly string TestsPassingIconLocation = "ExtensiveEngineerReport/TestPass";
        private Texture TestsPassingIcon;
        private Texture TestsFailIcon;
        private static readonly string TestsFailIconLocation = "ExtensiveEngineerReport/TestFail";
        
        internal override void Start()
        {
            DragEnabled = true;
            WindowRect.Set((Screen.width - WindowWidth) / 4, (Screen.height - WindowHeight) / 2, WindowWidth, WindowHeight);
            TestsPassingIcon = GameDatabase.Instance.GetTexture(TestsPassingIconLocation, false);
            TestsFailIcon = GameDatabase.Instance.GetTexture(TestsFailIconLocation, false);
            WindowCaption = "Extensive Engineer Report";
            if (ApplicationLauncher.Instance != null && ApplicationLauncher.Ready)
                OnAppLauncherReady();
            else
                GameEvents.onGUIApplicationLauncherReady.Add(OnAppLauncherReady);
        }

        internal override void Update()
        {
            base.Update();
            if (ConcernRunner.Instance.TestsPass)
            {
                button.SetTexture(TestsPassingIcon);
                EditorLogic.fetch.launchBtn.SetColor(Color.green);
            }
            else
            {
                button.SetTexture(TestsFailIcon);
                EditorLogic.fetch.launchBtn.SetColor(Color.red);
            }
        }

        private void OnAppLauncherReady()
        {
            if (button != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(button);
                button = null;
            }
            button = ApplicationLauncher.Instance.AddModApplication(
                () => Visible = true,
                () => Visible = false,
                null,
                null,
                null,
                null,
                ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.SPH,
                TestsPassingIcon);
            
        }

        internal override void OnDestroy()
        {
            ApplicationLauncher.Instance.RemoveModApplication(button);
        }

        internal override void DrawWindow(int id)
        {
            var passStyle = new GUIStyle(KSPPluginFramework.SkinsLibrary.CurrentSkin.toggle);
            passStyle.onNormal.textColor = XKCDColors.AlgaeGreen;
            passStyle.hover = passStyle.onHover;
            var failStyle = new GUIStyle(KSPPluginFramework.SkinsLibrary.CurrentSkin.toggle);
            failStyle.onNormal = failStyle.hover;
            failStyle.onNormal.textColor = Color.red;
            var descriptionStyle = new GUIStyle(KSPPluginFramework.SkinsLibrary.CurrentSkin.label);
            descriptionStyle.wordWrap = true;
            descriptionStyle.normal.textColor = Color.red;
            using (new GuiLayout(GuiLayout.Method.Horizontal))
            {
                var settings = GetScenarioModules<GeneralSettings>().First();
                GUILayout.Label("Enabled Test Severity");
                var old = settings.critical;
                settings.critical = GUILayout.Toggle(settings.critical, "Critical", KSPPluginFramework.SkinsLibrary.CurrentSkin.button);
                if (old != settings.critical) ConcernRunner.Instance.RunTests();
                old = settings.warning;
                settings.warning = GUILayout.Toggle(settings.warning, "Warning", KSPPluginFramework.SkinsLibrary.CurrentSkin.button);
                if (old != settings.warning) ConcernRunner.Instance.RunTests();
                old = settings.notice;
                settings.notice = GUILayout.Toggle(settings.notice, "Notice", KSPPluginFramework.SkinsLibrary.CurrentSkin.button);
                if (old != settings.notice) ConcernRunner.Instance.RunTests();
            }
            using (new GuiLayout(GuiLayout.Method.ScrollView, ref scrollPos))
            {
                GUILayout.Label("Ship-Wide Tests");
                foreach (var test in ConcernLoader.ShipDesignConcerns.Where(test => InCorrectFacility(test) && test.IsApplicable()))
                {
                    var passed = ConcernRunner.Instance.ShipConcerns[test];
                    GUILayout.Toggle(true, test.GetConcernTitle(), passed ? passStyle : failStyle);
                    if (!passed)
                    {
                        GUILayout.Label(test.GetConcernDescription(), descriptionStyle);
                    }
                }
                GUILayout.Label("Section-Specific Tests");
                foreach (var section in ShipSections.API.PartsBySection)
                {
                    var sectionData = ConcernRunner.Instance.SectionConcerns[section.Key];
                    GUILayout.Label(section.Key);
                    foreach (var test in ConcernLoader.SectionDesignConcerns.Where(test => InCorrectFacility(test) && test.IsApplicable(section)))
                    {
                        var run = sectionData.ContainsKey(test);
                        bool runNext;
                        if (run)
                        {
                            runNext = GUILayout.Toggle(run, test.GetConcernTitle(), sectionData[test] ? passStyle : failStyle);
                            if (!sectionData[test])
                            {
                                GUILayout.Label(test.GetConcernDescription(), descriptionStyle);
                            }
                        }
                        else
                            runNext = GUILayout.Toggle(run, test.GetConcernTitle(), passStyle);
                        if (!runNext)
                            ConcernRunner.Instance.DisableTest(section.Key, test);
                        if (!run && runNext)
                            ConcernRunner.Instance.EnableTest(section.Key, test);
                    }
                }
            }
        }
    }
}
