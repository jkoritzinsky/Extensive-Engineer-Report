using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JKorTech.Extensive_Engineer_Report
{

    /// <summary>
    /// Provides events related to the crew panel in the editor. Modified from BetterCrewAssignment
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    class CrewPanelMonitor : MonoBehaviour
    {
        // This is a hack, but I haven't been able to figure out any way around it.
        // I'd love to have event notifications when the player edits crew assignments
        // in the crew management dialog, but I haven't been able to find any. So I
        // poll with this frequency, and fire an event when there's been a change.
        // The update frequency has been chosen to be frequent enough that the player
        // can't "outrace" it (i.e. if the player makes a change, it will get detected
        // before the player can launch the ship or save it to disk), but infrequent
        // enough that it doesn't spam the CPU.
        private static readonly TimeSpan UPDATE_INTERVAL = new TimeSpan(0, 0, 0, 0, 200);

        private EditorScreen currentScreen;
        private DateTime lastUpdate;
        private List<ProtoCrewMember> lastAssignedCrew;
        private Coroutine updateCoroutine; 

        /// <summary>
        /// Start the monitor.
        /// </summary>
        public void Start()
        {
            currentScreen = EditorScreen.Parts;
            lastUpdate = DateTime.Now;
            lastAssignedCrew = GetCurrentlyAssignedCrew();
            GameEvents.onEditorScreenChange.Add(OnEditorScreenChange);
            GameEvents.onGameSceneSwitchRequested.Add(OnGameSceneSwitch);
        }

        /// <summary>
        /// Called on every frame.
        /// </summary>
        public void Update()
        {
            var editor = EditorLogic.fetch;
            if (editor != null)
            {
                if (editor.editorScreen != currentScreen)
                {
                    // This can happen when the player loads a ship while in the crew screen. The
                    // game loads the ship and switches to the Parts screen, but doesn't send
                    // a "screen changed" event.
                    OnEditorScreenChange(editor.editorScreen);
                }
            }
            if (currentScreen != EditorScreen.Crew) return; // only update while in the crew screen
            var now = DateTime.Now;
            if (now - lastUpdate < UPDATE_INTERVAL) return;
            lastUpdate = now;
            OnUpdateTick();
        }

        /// <summary>
        /// Handler for CrewChanged events.
        /// </summary>
        public delegate void CrewPanelEventHandler();
        
        /// <summary>
        /// This event fires when the player has made a crew change inside
        /// the crew panel.
        /// </summary>
        public static event CrewPanelEventHandler CrewChanged;

        /// <summary>
        /// Here when the editor screen changes.
        /// </summary>
        /// <param name="screen"></param>
        private void OnEditorScreenChange(EditorScreen screen)
        {
            bool isStarting = (screen == EditorScreen.Crew);
            bool isStopping = (currentScreen == EditorScreen.Crew);
            currentScreen = screen;
            if (isStopping) OnStop();
            if (isStarting) OnStart();
        }

        /// <summary>
        /// Here when the game scene switches.
        /// </summary>
        /// <param name="action"></param>
        private void OnGameSceneSwitch(GameEvents.FromToAction<GameScenes, GameScenes> action)
        {
            if (action.from != GameScenes.EDITOR) return;
            bool isStopping = (currentScreen == EditorScreen.Crew);
            currentScreen = EditorScreen.Parts;
            if (isStopping) OnStop();
        }

        /// <summary>
        /// Here when entering the crew panel.
        /// </summary>
        private void OnStart()
        {
            lastAssignedCrew = GetCurrentlyAssignedCrew();
            updateCoroutine = StartCoroutine(UpdateTick());
        }

        private IEnumerator UpdateTick()
        {
            while (true)
            {
                yield return new WaitForSeconds((float)UPDATE_INTERVAL.TotalSeconds);
                OnUpdateTick();
            }
        }

        /// <summary>
        /// Here when leaving the crew panel.
        /// </summary>
        private void OnStop()
        {
            lastAssignedCrew = null;
            StopCoroutine(updateCoroutine);
        }

        /// <summary>
        /// Called once every update interval while the crew panel is displayed.
        /// </summary>
        private void OnUpdateTick()
        {
            if (CrewChanged == null) return; // no point in doing anything if the event isn't subscribed
            var currentlyAssignedCrew = GetCurrentlyAssignedCrew();
            if (AreSame(lastAssignedCrew, currentlyAssignedCrew)) return; // no change, nothing to do
            lastAssignedCrew = currentlyAssignedCrew;
            CopyCrew();
            CrewChanged();
        }


        /// <summary>
        /// Copies crew assignments from the dialog to the ship construct.
        /// </summary>
        private void CopyCrew()
        {
            VesselCrewManifest dialogVessel = CurrentDialogContents;
            if (dialogVessel == null)
            {
                return;
            }
            VesselCrewManifest currentVessel = ShipConstruction.ShipManifest;
            List<PartCrewManifest> dialogParts = dialogVessel.GetCrewableParts();
            List<PartCrewManifest> currentParts = currentVessel.GetCrewableParts();
            if (dialogParts.Count != currentParts.Count)
            {
                return;
            }
            for (int partIndex = 0; partIndex < dialogParts.Count; ++partIndex)
            {
                PartCrewManifest dialogPart = dialogParts[partIndex];
                PartCrewManifest currentPart = currentParts[partIndex];
                ProtoCrewMember[] dialogCrew = dialogPart.GetPartCrew();
                ProtoCrewMember[] currentCrew = currentPart.GetPartCrew();
                if ((dialogPart.PartID != currentPart.PartID) || (dialogCrew.Length != currentCrew.Length))
                {
                    return;
                }
                for (int slotIndex = 0; slotIndex < dialogCrew.Length; ++slotIndex)
                {
                    ProtoCrewMember dialogMember = dialogCrew[slotIndex];
                    ProtoCrewMember currentMember = currentCrew[slotIndex];
                    if (!AreSame(dialogMember, currentMember))
                    {
                        currentPart.RemoveCrewFromSeat(slotIndex);
                        if (dialogMember != null)
                        {
                            if (currentVessel.Contains(dialogMember))
                            {
                                PartCrewManifest previousPart = currentVessel.GetPartForCrew(dialogMember);
                                previousPart.RemoveCrewFromSeat(previousPart.GetCrewSeat(dialogMember));
                            }
                            currentPart.AddCrewToSeat(dialogMember, slotIndex);
                        }
                    } // if there's an assignment difference
                } // for each slot in the part
            } // for each crewable part in the vessel
        }


        private static List<ProtoCrewMember> GetCurrentlyAssignedCrew()
        {
            var dialogManifest = CurrentDialogContents;
            return (dialogManifest == null) ? null : dialogManifest.GetAllCrew(true);
        }

        private static bool AreSame(List<ProtoCrewMember> list1, List<ProtoCrewMember> list2)
        {
            if ((list1 == null) && (list2 == null)) return true;
            if ((list1 == null) || (list2 == null)) return false;
            if (list1.Count != list2.Count) return false;
            for (int index = 0; index < list1.Count; ++index)
            {
                if (!AreSame(list1[index], list2[index])) return false;
            }
            return true;
        }

        private static bool AreSame(ProtoCrewMember member1, ProtoCrewMember member2)
        {
            if ((member1 == null) && (member2 == null)) return true;
            if ((member1 == null) || (member2 == null)) return false;
            return member1.name.Equals(member2.name);
        }

        /// <summary>
        /// Gets the current crew assignments within the crew-management dialog. This
        /// is important because the game doesn't actually take these and assign them
        /// to the current ship construct's manifest until the player exits the crew
        /// management dialog.
        /// </summary>
        private static VesselCrewManifest CurrentDialogContents
        {
            get
            {
                // Many, many thanks to KSP user sarbian (of ModuleManager fame) for pointing out
                // the CMAssignmentDialog class to me when I asked about it in the mod development
                // forum. I never in a thousand years would have found this cryptically-named
                // class on my own.
                return (CMAssignmentDialog.Instance == null) ? null : CMAssignmentDialog.Instance.GetManifest();
            }
        }
    }

}
