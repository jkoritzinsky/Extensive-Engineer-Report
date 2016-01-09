using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JKorTech.Extensive_Engineer_Report
{
    [KSPScenario(ScenarioCreationOptions.AddToAllGames, GameScenes.EDITOR)]
    class GeneralSettings : ScenarioModule
    {
        [Persistent]
        public bool critical = true;

        [Persistent]
        public bool warning = true;

        [Persistent]
        public bool notice = true;

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node);
            ConfigNode.CreateConfigFromObject(this, node);
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            ConfigNode.LoadObjectFromConfig(this, node);
        }

        public bool ShouldRun(PreFlightTests.DesignConcernSeverity severity)
        {
            switch (severity)
            {
                case PreFlightTests.DesignConcernSeverity.NOTICE:
                    return notice;
                case PreFlightTests.DesignConcernSeverity.WARNING:
                    return warning;
                case PreFlightTests.DesignConcernSeverity.CRITICAL:
                    return critical;
                default:
                    return false;
            }
        }
    }
}
