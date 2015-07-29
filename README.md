# Extensive Engineer Report
Extensive Engineer Report is a Kerbal Space Program mod that extends the Engineer's Report in the Vehicle Assembly Building and the Spaceplane Hanger with additional checks to ensure that your vessel has everything you need.
This mod is available on CKAN or on the releases page on Github (https://github.com/jkoritzinsky/Extensive-Engineer-Report/releases/latest).
## Special Features (v0.4)
### Pre Flight Warning
Warns before launching vessels if any warning or critical level checks in this mod fail.

## Stock bugs that affect this mod
A bug in the "Docking Port Facing" test in stock breaks the Engineer's Report testing functionality.  It is being tracked on the KSP bug tracker here: http://bugs.kerbalspaceprogram.com/issues/5193.

## Stock Tests In This Version (v0.4)
* Unmanned With Science And No Transmitters
  * Warns when a vessel has no crew, no stock science transmitters, but it does have science experiments.
  * Severity: Warning
* High Heat Gen; No Radiators
  * Warns when a vessel has high heat generating parts like nuclear engines, but no radiators to radiate the heat.
  * Severity: Warning
* Landing Legs but no Lights
  * Warns if you have landing legs or landing gear but no lights to help prevent accidental dark night landings.
  * Severity: Notice
  * Suggested by: cephalo
* Has Control Surfaces
  * Warns when a plane in the SPH does not have control surfaces.
  * Severity: Warning
* Suggest fixed power generation if only deployable
  * Warns when the only power generation is deployable to prevent DOA probes because panels weren't deployed.
  * Severity: Notice
  * Suggested by: cephalo on the forum
* Has SAS
  * Warns if your vessel has no SAS modules or pilots in command modules
  * Severity: Warning
* Labs Have Scientists Aboard
  * Warns if your vessel has a science lab but no scientists
  * Severity: Warning
* Non resettable experiments have scientists or lab
  * Warns if non reusable experiments (i.e. Science Jr.) are on a vessel but there is nothing on the vessel to reset them.
  * Severity: Warning
  * Suggested by: rmpalomino
* Probe core has backup battery
  * Warns if a vessel is unmanned and does not have a battery that has charge flow stopped.
  * Severity: Warning
  * Note: Because of when the Engineer's Report refreshes, this will not go away until a part is added or removed after it is fixed.
* Radial stages strutted
  * Warns if radial stages are not attached to upper stages via struts
  * Severity: Notice
* Struts don't go to lower stages
  * Warns if struts start on higher stages.  This test is because drag on struts is extremely high in stock.
  * Severity: Notice
* TWR above 1
  * Warns if TWR of first stage at max thrust is < 1 in VAB
  * Severity: Notice
  * Suggested by: MooseCannon

## Tests In The RemoteTech Concerns Plugin (v0.3)
* Antenna Checks
  * Warns if an unmanned probe either has no antenna or only has the integrated antenna (probe cores).
  * Severity: Critical
  * Warns before flight if a ship only has an integrated antenna.
* Has Flight Computer
  * Warns if an unmanned probe does not have a flight computer.
  * Severity: Critical

## Tests In The BDArmory Concerns Plugin (v0.1)
* Weapons and no weapon manager
  * Severity: Notice
  * Suggested by: MooseCannon
* Autopilot but no weapon manager
  * Severity: Notice
  * Suggested by: MooseCannon
* Weapon manager but no flares
  * Severity: Notice
  * Suggested by: MooseCannon

## Unassociated Mod-Related Concerns
* Unmanned probe has kOS
  * Warns if an unmanned probe lacks a kOS processor (Only if kOS is installed)
  * Severity: Notice
  * Suggested by: LinuxGurugamer

# Changelog

## Bug Fixes in v0.4
TWR no longer warns when TWR is actually > 1
Pre flight warning only warns about tests that apply to the current building
Pre flight warning now plays nice with Kerbal Construction Time


## How To Report Bugs and Request Features
If you find a bug or have a feature request, add an issue at https://github.com/jkoritzinsky/Extensive-Engineer-Report/issues/new on GitHub. This will have the fastest turnaround.  I'll also respond on the KSP forum or on Reddit, but it might take a little longer.

### Custom Test Configuration Through UI
Once KSP 1.1 on Unity 5 is released, I will create a configuration UI for this mod.  I don't want to have to build the UI twice.
