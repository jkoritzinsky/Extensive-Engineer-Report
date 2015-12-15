using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JKorTech.Extensive_Engineer_Report
{
    /// <summary>
    /// These classes exist only to get toggling flow control to trigger the onEditorShipModified event.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class FlowControlAttacher : MonoBehaviour
    {
        public void Awake()
        {
            TryAttach();
        }

        public void Update()
        {
            TryAttach();
        }

        private void TryAttach()
        {
            var controller = UIPartActionController.Instance;
            if (!controller)
                return;

            var prefab = controller.resourceItemEditorPrefab;
            if (!prefab)
                return;

            // Pre-attach the stabilizer to the menu window template
            if (!prefab.GetComponent<EventHandler>())
                prefab.gameObject.AddComponent<EventHandler>();

            Destroy(this);
        }

        private class EventHandler : MonoBehaviour
        {
            private void FireEvent(object val)
            {
                StartCoroutine(DelayFire());
            }

            // Need to delay the event firing because otherwise it will fire before the value is changed...
            private System.Collections.IEnumerator DelayFire()
            {
                yield return new object();
                GameEvents.onEditorShipModified.Fire(EditorLogic.fetch.ship);
            }

            public void Awake()
            {
                var editor = gameObject.GetComponent<UIPartActionResourceEditor>();
                editor.flowBtn.AddValueChangedDelegate(FireEvent);
            }
            public void OnDestroy()
            {
                var editor = gameObject.GetComponent<UIPartActionResourceEditor>();
                editor?.flowBtn?.RemoveValueChangedDelegate(FireEvent);
            }
        }
    }
}
