# Extensive Engineer Report
Extensive Engineer Report is a Kerbal Space Program mod that extends the Engineer's Report in the Vehicle Assembly Building and the Spaceplane Hanger with additional checks to ensure that your vessel has everything you need.
This mod is available on CKAN or on the releases page on Github (https://github.com/jkoritzinsky/Extensive-Engineer-Report/releases/latest).
## Dependencies
* ShipSections v1.1
* ModuleManager
## Special Features (v0.5)
### Launch Button Color
If any checked tests (applicable and severity enabled) fail, the launch button will turn red.  It will still work, but it should catch your eye and get you to check your tests.

### Applicable Tests
Tests only show up if they are considered applicable (required mods installed, has first part type of a combo)

## Tests In This Version (v0.5)
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
* Struts don't go to lower stages (auto-disables with FAR installed)
  * Warns if struts start on higher stages.  This test is because drag on struts is extremely high in stock.
  * Severity: Notice
* TWR above 1
  * Warns if TWR of first stage at max thrust is < 1 in VAB
  * Severity: Notice
  * Suggested by: MooseCannon
* Unmanned probe has an advanced flight computer
  * Warns if an unmanned probe lacks an advanced flight computer (ie kOS or MechJeb) if any applicable mods are installed.
  * Severity: Notice
  * Suggested by: LinuxGurugamer

## RemoteTech Tests
* Antenna Checks
  * Warns if an unmanned probe either has no antenna or only has the integrated antenna (probe cores).
  * Severity: Critical
  * Warns before flight if a ship only has an integrated antenna.
* Has Flight Computer
  * Warns if an unmanned probe does not have a flight computer.
  * Severity: Critical

## BDArmory Tests
* Weapons and no weapon manager
  * Severity: Notice
  * Suggested by: MooseCannon
* Autopilot but no weapon manager
  * Severity: Notice
  * Suggested by: MooseCannon
* Weapon manager but no flares
  * Severity: Notice
  * Suggested by: MooseCannon


# Changelog

## Changed in v0.5
* Added a UI.
* Depends on ShipSections and MM
* Section tests can be toggled on and off.
* Visible (but not intrusive) warning before launch.
## Bug Fixes in v0.4
TWR no longer warns when TWR is actually > 1
Pre flight warning only warns about tests that apply to the current building
Pre flight warning now plays nice with Kerbal Construction Time


## How To Report Bugs and Request Features
If you find a bug or have a feature request, add an issue at https://github.com/jkoritzinsky/Extensive-Engineer-Report/issues/new on GitHub. This will have the fastest turnaround.  I'll also respond on the KSP forum or on Reddit, but it might take a little longer.
