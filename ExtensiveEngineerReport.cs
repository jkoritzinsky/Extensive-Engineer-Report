using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JKorTech.Extensive_Engineer_Report
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class ExtensiveEngineerReport : MonoBehaviour
    {
        public List<IDesignConcern> designConcerns;
        public List<IPreFlightTest> preFlightTests;
        private bool reportsRegistered;

        public class NoConcerningAspects : IPreFlightTest
        {
            private ExtensiveEngineerReport report;
            public NoConcerningAspects(ExtensiveEngineerReport report)
            {
                this.report = report;
            }

            public string GetAbortOption()
            {
                return "Cancel";
            }

            public string GetProceedOption()
            {
                return "I've checked. Launch anyway.";
            }

            public string GetWarningDescription()
            {
                var concernNames = report.designConcerns.Where(concern => !IsPassingConcern(concern)).Select(concern => concern.GetConcernTitle())
                    .Aggregate(new StringBuilder().AppendLine(), (builder, str) => builder.AppendLine(str)).ToString();
                return @"There are some concerning aspects from the supplementary Engineers' Report about your vessel.Specifically:" + concernNames
                    + "\nDo you want to check if they are important?";
            }

            public string GetWarningTitle()
            {
                return "Concerning Engineers' Report";
            }

            private bool IsPassingConcern(IDesignConcern concern)
            {
                return concern.GetSeverity() == DesignConcernSeverity.NOTICE || concern.Test() == true || concern is IPreFlightTest;
            }

            public bool Test()
            {
                return report.designConcerns.TrueForAll(IsPassingConcern);
            }
        }

        void Awake()
        {
            reportsRegistered = false;
            designConcerns = InitializeLists<IDesignConcern>();
            preFlightTests = InitializeLists<IPreFlightTest>();
            preFlightTests.Add(new NoConcerningAspects(this));
            Debug.Log("[Extensive Engineer Report] Extensive Engineer Report Initialized");
        }

        private List<TInterface> InitializeLists<TInterface>()
            where TInterface : class
        {
            List<TInterface> instances = new List<TInterface>();
            foreach (var assembly in AssemblyLoader.loadedAssemblies)
            {
                if (object.Equals(assembly, typeof(TInterface).Assembly)) continue;
                foreach (var type in assembly.assembly.GetTypes())
                {
                    if (type.GetInterfaces().Any(t => object.Equals(t, typeof(TInterface))) && !object.Equals(type.Assembly, typeof(TInterface).Assembly))
                    {
                        Debug.Log("[Extensive Engineer Report] Found item: " + type.Name);
                        var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
                        if ((object)defaultConstructor != null) instances.Add((TInterface)defaultConstructor.Invoke(null));
                        else Debug.LogWarning("[Extensive Engineer Report] Item " + type.Name + " does not have a default constructor");
                    }
                }
            }
            return instances;
        }

        void Start() 
        {
            EditorLogic.fetch.launchBtn.methodToInvoke = null;
            EditorLogic.fetch.launchBtn.scriptWithMethodToInvoke = null;
            EditorLogic.fetch.launchBtn.SetInputDelegate((ref POINTER_INFO ptr) =>
            {
                if (ptr.evt == POINTER_INFO.INPUT_EVENT.TAP)
                {
                    PreFlightCheck check = new PreFlightCheck(EditorLogic.fetch.launchVessel, () => { });
                    preFlightTests.ForEach(test => check.AddTest(test));
                    check.RunTests();
                }
            });
            GameEvents.onGUIEngineersReportReady.Add(AddTests);
            GameEvents.onGUIEngineersReportDestroy.Add(RemoveTests);
        }

        private void AddTests()
        {
            Debug.Log("[Extensive Engineer Report] Concerns registering");
            foreach (var concern in designConcerns)
            {
                EngineersReport.Instance.AddTest(concern);
            }
            reportsRegistered = true;
            Debug.Log("[Extensive Engineer Report] Concerns registered");
        }

        private void RemoveTests()
        {
            if (!reportsRegistered) Debug.LogError("[Extensive Engineer Report] Never registered");
            if (reportsRegistered)
            {
                Debug.Log("[Extensive Engineer Report] Concerns un-registering");
                foreach (var concern in designConcerns)
                {
                    EngineersReport.Instance.RemoveTest(concern);
                }
                reportsRegistered = false;
                Debug.Log("[Extensive Engineer Report] Concerns unregistered");
            }
        }

        void Update() { }
        void FixedUpdate() { }
        void OnDestroy()
        {
            GameEvents.onGUIEngineersReportReady.Remove(AddTests);
            GameEvents.onGUIEngineersReportDestroy.Remove(RemoveTests);
        }
    }
}
