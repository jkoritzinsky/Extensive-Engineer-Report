using PreFlightTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            designConcerns.Add(new ScienceButNoComms());
            Debug.Log("[Extensive Engineer Report] Extensive Engineer Report Initialized");
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
