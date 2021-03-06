using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JKorTech.Extensive_Engineer_Report.KSPExtensions;

namespace JKorTech.Extensive_Engineer_Report
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class ConcernRunner : KSPPluginFramework.MonoBehaviourExtended
    {
        public static ConcernRunner Instance { get; private set; }
        internal override void Start()
        {
            GameEvents.onEditorShipModified.Add(ShipModified);
            GameEvents.onEditorLoad.Add(OnLoad);
            GameEvents.onEditorShowPartList.Add(RunTests);
            CrewPanelMonitor.CrewChanged += RunTests;
            ShipSections.API.PartSectionInitialized.Add(SectionInitialized);
            ShipSections.API.SectionRenamed.Add(SectionRenamed);
            ShipSections.API.SectionsMerged.Add(SectionsMerged);
            ShipSections.API.NewSectionCreated.Add(NewSection);
            Instance = this;
        }

        private void NewSection(string data)
        {
            RunTests();
        }

        private void SectionsMerged(string data0, string data1)
        {
            //It's just easier to rerun all of the tests.
            RunTests();
        }

        private void SectionRenamed(string data0, string data1)
        {
            //Since just renaming sections, can easily just change the section name.
            SectionConcerns[data1] = SectionConcerns[data0];
            SectionConcerns.Remove(data0);
        }

        private void SectionInitialized(Part data)
        {
            RunTests();
        }

        private void ShipModified(ShipConstruct data)
        {
            RunTests();
        }

        private void OnLoad(ShipConstruct data0, CraftBrowser.LoadType data1)
        {
            if(data1 == CraftBrowser.LoadType.Normal)
            {
                ShipConcerns.Clear();
                SectionConcerns.Clear();
            }
        }

        internal Dictionary<DesignConcernBase, bool> ShipConcerns = new Dictionary<DesignConcernBase, bool>();
        internal Dictionary<string, Dictionary<SectionDesignConcernBase, bool>> SectionConcerns = new Dictionary<string, Dictionary<SectionDesignConcernBase, bool>>();
        public bool TestsPass { get; private set; }

        internal void RunTests()
        {
            LogFormatted("Run Tests");
            TestsPass = true;
            if (!ShipSections.API.AnyCurrentVesel)
                return;
            foreach (var concern in ConcernLoader.ShipDesignConcerns)
            {
                try
                {
                    ShipConcerns[concern] = concern.Test();
                    if (GetScenarioModules<GeneralSettings>().First().ShouldRun(concern.GetSeverity()))
                    {
                        TestsPass &= ShipConcerns[concern];
                    }
                    LogFormatted_DebugOnly($"Concern {concern.GetConcernTitle()} returned {ShipConcerns[concern]}");
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"[Extensive Engineer Report] Test {concern.GetConcernTitle()} threw an exception.");
                    UnityEngine.Debug.LogException(ex);
                    TestsPass &= (ShipConcerns[concern] = true);
                }
            }
            foreach (var section in ShipSections.API.SectionNames)
            {
                if (SectionConcerns.ContainsKey(section))
                {
                    SectionConcerns[section].Clear();
                }
                else
                {
                    SectionConcerns.Add(section, new Dictionary<SectionDesignConcernBase, bool>());
                }
                var data = ShipSections.API.GetSectionDataForMod<ConcernData>(section);
                foreach (var concern in ConcernLoader.SectionDesignConcerns.Where(concern => !data.disabledConcerns.Contains(concern.GetType().Name)))
                {
                    try
                    {
                        var sectionParts = ShipSections.API.PartsBySection.First(group => group.Key == section);
                        if (concern.IsApplicable(sectionParts))
                        {
                            SectionConcerns[section][concern] = concern.TestCondition();
                            if (GetScenarioModules<GeneralSettings>().First().ShouldRun(concern.GetSeverity()))
                            {
                                TestsPass &= SectionConcerns[section][concern]; 
                            }
                            LogFormatted_DebugOnly($"Concern {concern.GetConcernTitle()} for section {section} returned {SectionConcerns[section][concern]}");
                        }
                    }
                    catch (Exception ex)
                    {
                        UnityEngine.Debug.LogError($"[Extensive Engineer Report] Test {concern.GetConcernTitle()} threw an exception.");
                        UnityEngine.Debug.LogException(ex);
                        TestsPass &= (SectionConcerns[section][concern] = true);
                    }
                }
            }
        }

        internal override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.onEditorShipModified.Remove(ShipModified);
            GameEvents.onEditorLoad.Remove(OnLoad);
            CrewPanelMonitor.CrewChanged -= RunTests;
            ShipSections.API.PartSectionInitialized.Remove(SectionInitialized);
            ShipSections.API.SectionRenamed.Remove(SectionRenamed);
            ShipSections.API.SectionsMerged.Remove(SectionsMerged);
            ShipSections.API.NewSectionCreated.Remove(NewSection);
            GameEvents.onEditorShowPartList.Remove(RunTests);
            Instance = null;
        }

        internal void EnableTest(string section, SectionDesignConcernBase test)
        {
            ShipSections.API.GetSectionDataForMod<ConcernData>(section).disabledConcerns.Remove(test.GetType().Name);
            RunTests();
        }

        internal void DisableTest(string section, SectionDesignConcernBase test)
        {
            ShipSections.API.GetSectionDataForMod<ConcernData>(section).disabledConcerns.AddUnique(test.GetType().Name);
            SectionConcerns[section].Remove(test);
            RunTests();
        }
    }
}
