# Extensive Engineer Report
Extensive Engineer Report is a [Kerbal Space Program](http://kerbalspaceprogram.com) mod that extends the Engineers' Report in the Vehicle Assembly Building and the Spaceplane Hanger with additional checks to ensure that your vessel has everything you need.

## Tests In This Version (v0.2)
* Unmanned With Science And No Transmitters
  * Warns when a vessel has no crew, no stock science transmitters, but it does have science experiments.
  * Severity: Warning
  * Suggested by: Someone on [/r/KerbalSpaceProgram](http://reddit.com/r/KerbalSpaceProgram) (if you suggested this, let me know and you'll get credit here)
* High Heat Gen; No Radiators
  * Warns when a vessel has high heat generating parts like nuclear engines, but no radiators to radiate the heat.
  * Severity: Warning
* Landing Legs but no Lights
  * Warns if you have landing legs or landing gear but no lights to help prevent accidental dark night landings.
  * Severity: Notice
  * Suggested by: [cephalo](http://forum.kerbalspaceprogram.com/threads/126662-1-04-Extensive-Engineer-Report?p=2040769&viewfull=1#post2040769) on the forum
* Has Control Surfaces
  * Warns when a plane in the SPH does not have control surfaces.
  * Severity: Warning
* Suggest fixed power generation if only deployable
  * Warns when the only power generation is deployable to prevent DOA probes because panels weren't deployed.
  * Severity: Notice
  * Suggested by: [cephalo](http://forum.kerbalspaceprogram.com/threads/126662-1-04-Extensive-Engineer-Report?p=2040769&viewfull=1#post2040769) on the forum

## Tests In The RemoteTech Concerns Plugin (v0.1)
* Antenna Checks
  * Warns if an unmanned probe either has no antenna or only has the integrated antenna (probe cores).
  * Severity: Critical
* Has Flight Computer
  * Warns if an unmanned probe does not have a flight computer.
  * Severity: Critical
## How To Report Bugs and Request Features
If you find a bug or have a feature request, [add an issue here](https://github.com/jkoritzinsky/Extensive-Engineer-Report/issues/new) on GitHub. This will have the fastest turnaround.

### Custom Test Configuration Through UI
Once KSP 1.1 on Unity 5 is released, and no earlier, I will create a configuration UI for this mod.
