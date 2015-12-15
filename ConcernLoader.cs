using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static JKorTech.Extensive_Engineer_Report.ConcernUtils;

namespace JKorTech.Extensive_Engineer_Report
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class ConcernLoader : MonoBehaviour
    {
        private static bool loaded;

        internal static List<DesignConcernBase> ShipDesignConcerns { get; } = new List<DesignConcernBase>();

        internal static List<SectionDesignConcernBase> SectionDesignConcerns { get; } = new List<SectionDesignConcernBase>();

        internal static List<PreFlightTests.IPreFlightTest> PreFlightTests { get; } = new List<PreFlightTests.IPreFlightTest>();

        internal static List<string> DisabledCategories = new List<string>();

        internal void Awake()
        {
            if (loaded)
                Destroy(this);
            else
            {
                LoadTypes();
                loaded = true;
            }
        }

        private static void LoadTypes()
        {
            var designConcerns = DiscoverTypes<DesignConcernBase>();
            designConcerns.RemoveAll(concern => !InCorrectFacility(concern));
            SectionDesignConcerns.AddRange(designConcerns.OfType<SectionDesignConcernBase>());
            ShipDesignConcerns.AddRange(designConcerns.Where(concern => !(concern is SectionDesignConcernBase)));
            PreFlightTests.AddRange(DiscoverTypes<PreFlightTests.IPreFlightTest>());
            DisabledCategories.AddRange(LoadDisabledCategories());
            SectionDesignConcerns.RemoveAll(concern => DisabledCategories.Contains(concern.Category));
            ShipDesignConcerns.RemoveAll(concern => DisabledCategories.Contains(concern.Category));
        }

        private static IEnumerable<string> LoadDisabledCategories()
        {
            var configNode = GameDatabase.Instance.GetConfigNodes("ExtensiveEngineerReport")[0];
            foreach (var category in configNode.values.DistinctNames())
            {
                if (!bool.Parse(configNode.GetValue(category)))
                    yield return category;
            }
        }

        private static List<TBase> DiscoverTypes<TBase>()
            where TBase : class
        {
            var instances = new List<TBase>();
            foreach (var assembly in AssemblyLoader.loadedAssemblies)
            {
                if (Equals(assembly, typeof(PreFlightCheck).Assembly)) continue; //Skip types in the stock assembly
                foreach (var type in assembly.assembly.GetTypes())
                {
                    if (!type.IsAbstract && typeof(TBase).IsAssignableFrom(type))
                    {
                        Debug.Log("[Extensive Engineer Report] Found item: " + type.Name);
                        var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
                        if (defaultConstructor != null) instances.Add((TBase)type.GetConstructor(Type.EmptyTypes).Invoke(null));
                        else Debug.Log("[Extensive Engineer Report] Item " + type.Name + " does not have a default constructor");
                    }
                }
            }
            return instances;
        }
    }
}
