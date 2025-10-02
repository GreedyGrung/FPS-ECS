# FPS game prototype

## Overview

Stack: **LeoECS Lite**, **VContainer**, **UniTask**.
<br/>**Infrastructure runs on services via DI; gameplay runs on ECS.**
<br/>Entry points: `BootstrapScope` (app) and `GameplayScope` (gameplay).

### Custom authoring logic

Custom lightweight authoring layer automatically **converts scene GameObjects into ECS entities** both right after the scene is loaded and at runtime:

* Finds/creates authored objects (`Actor`).
* Emits entities and attaches the corresponding ECS components by running `Convert` method from `IAuthoring` interface on each authoring component.
* Attaches values to components from inspector or loading them from `IConfigsProvider` if needed.
* Keeps MonoBehaviours thin (view/data carriers only). All gameplay logic is placed in systems.

## Key Features

* **FPS character control & shooting**
* **Simple enemy AI**
* **Progression system**
* **Save/Load**
