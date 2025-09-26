# FPS game prototype

## Overview

Stack: **LeoECS Lite**, **VContainer**, **UniTask**.
<br/>**Infrastructure runs on services via DI; gameplay runs on ECS.**
<br/>Entry points: `BootstrapScope` (app) and `GameplayScope` (gameplay).

### Custom authoring logic

Custom lightweight authoring layer automatically **converts scene GameObjects into ECS entities** at runtime:

* Finds authored objects (`Actor`, markers/components).
* Emits entities and attaches the corresponding ECS components.
* Keeps MonoBehaviours thin (view/data carriers only).

## Key Features

* **FPS character control & shooting**
* **Simple enemy AI**
* **Progression system**
* **Save/Load**
