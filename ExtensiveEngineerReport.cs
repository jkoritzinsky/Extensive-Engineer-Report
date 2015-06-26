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
        private bool reportsRegistered;
        void Awake()
        {
            reportsRegistered = false;
            designConcerns = new List<IDesignConcern>();
            InitializeConcerns();
            Debug.Log("[Extensive Engineer Report] Extensive Engineer Report Initialized");
        }

        private void InitializeConcerns()
        {
            foreach (var assembly in AssemblyLoader.loadedAssemblies)
            {
                foreach (var type in assembly.assembly.GetTypes())
                {
                    if (object.Equals(type.BaseType, typeof(DesignConcernBase)) && !object.Equals(type.Assembly, typeof(DesignConcernBase).Assembly))
                    {
                        Debug.Log("[Extensive Engineer Report] Found concern: " + type.Name);
                        var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
                        if ((object)defaultConstructor != null) designConcerns.Add((IDesignConcern)defaultConstructor.Invoke(null));
                        else Debug.LogWarning("[Extensive Engineer Report] Concern: " + type.Name + " does not have a default constructor");
                    }
                } 
            }
        }
        void Start() { }
        void Update()
        {
            if (EngineersReport.Instance != null && !reportsRegistered)
            {
                Debug.Log("[Extensive Engineer Report] Concerns registering");
                foreach (var concern in designConcerns)
                {
                    EngineersReport.Instance.AddTest(concern);
                }
                reportsRegistered = true;
                Debug.Log("[Extensive Engineer Report] Concerns registered");
            }
        }
        void FixedUpdate() { }
        void OnDestroy()
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
    }
}
