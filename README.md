# Unity Forging Game

This project is a Unity-based forging mini-game built to demonstrate a data-driven architecture, modular UI handling, and quest-based progression. It was developed as part of a test assignment to showcase clean and extensible code.

## Overview

- **UI-Driven Layout:**  
  - **Machines:** Displayed as a vertical list on the right side.  
  - **Inventory Panel:** Shown on the left side.  
  - **Character View:** Located at the top, showing active quests and bonuses.

- **Simultaneous Crafting:**  
  - Each machine operates independently on its own timer, allowing multiple crafting processes concurrently if resources are available.

- **Quest Tracking:**  
  - Crafting an item updates quest progress. Upon meeting a quest’s requirements, the quest is marked complete and the corresponding machine is unlocked immediately.

- **Data-Driven Design:**  
  - Items, recipes, machines, quests, and bonuses are defined exactly as specified using ScriptableObjects, ensuring the game’s data is easily configurable.


## How to Run the Project

1. **Open the Project:**  
   - Open Unity (version 6000.0.32f1 or later recommended) and select **Open Project** to load the repository.

2. **Scene Setup:**  
   - The Main scene contains the UI elements (inventory panel, machine list, quest/bonus display) and managers (`GameManager`, `InventoryManager`, `QuestManager`).

3. **Play the Scene:**  
   - Press the **Play** button in the Unity Editor to start the game. You can interact with the inventory, drag and drop items, and start crafting processes via the machine UI.

## Project Structure

- **Scripts:**  
  - **Core Systems:**  
    - `InventoryManager.cs` – Central data storage for inventory items, UI updating, and bonus item handling.  
    - `GameManager.cs` – Initializes machines and injects dependencies (such as the `InventoryManager`) into them.  
    - `QuestManager.cs` – Manages quest progress and unlocks machines when requirements are met.  
    - `CraftingEvents.cs` – Static event system used for notifying when items are crafted.
  - **Machine & Crafting:**  
    - `MachineController.cs` – Combined script for handling machine-specific crafting logic (including timers, success checks, and UI updates).  
    - `CraftingSlot.cs` & `CraftedSlot.cs` – UI components for managing crafting input and output.
  - **UI Elements:**  
    - `InventorySlot.cs` – Controls the visual representation of inventory items with drag & drop support.  
    - `DragIcon.cs` – A singleton used for providing visual feedback when dragging an item.  
    - `BonusUIElement.cs`, `ForgeUIElement.cs`, `QuestUIElement.cs` – UI scripts for displaying bonuses, machine locks, and quest information.
  - **ScriptableObjects:**  
    - `Item.cs` – Base data type for items.  
    - `BonusItem.cs` – Extends `Item` and implements bonus effects.  
    - `Recipe.cs` – Defines input/output items, crafting time, and success rate.  
    - `Quest.cs` – Contains quest information including requirements and progress.

- **Prefabs & UI:**  
  - Contains prefabs for notifications, bonus UI, and any other reusable UI components.

- **Data Folders:**  
  - ScriptableObject instances for items, recipes, quests, and bonus items.

## Unique Code Decisions

- **Dependency Injection:**  
  - The `GameManager` initializes each machine, as well as the `QuestManager` and `StartingInventoryGenerator`, by injecting the `InventoryManager` reference into them. This approach decouples the systems, makes testing easier, and ensures consistency across the project.
  
- **Singletons vs. DI:**  
  - While `DragIcon` and `ToastNotificationManager` are implemented as singletons (for ease of access in UI interactions), core game systems like the `InventoryManager` are injected via initialization, promoting loose coupling.

- **Data-Driven Architecture:**  
  - The use of ScriptableObjects for items, recipes, quests, and bonuses allows game designers to easily modify and balance game data without modifying code.

- **UI and Data Synchronization:**  
  - The inventory UI (managed by `InventorySlot` scripts) is tightly synchronized with the central `InventoryManager` to ensure the UI accurately reflects the underlying data at all times.

- **Event-Driven Quest Updates:**  
  - Crafting events trigger quest progress updates, ensuring that game logic (like unlocking machines) reacts immediately to player actions.

- **Tooltip System:**  
  - The `Tooltip` script displays contextual help when hovering over UI elements, providing clear and immediate information about items, machines, and other UI components.
  
- **Toast Notification System:**  
  - The `ToastNotificationManager` script provides brief, on-screen notifications for events such as crafting success/failure and quest completions, enhancing player feedback without disrupting gameplay.


## Running & Testing

- **Editor Testing:**  
  - Use Unity’s Play mode to test the drag-and-drop functionality, crafting processes, and quest progression.

- **Modularity:**  
  - Machines can be added or modified without altering the core inventory or quest systems.

- **Customization:**  
  - Designers can modify ScriptableObject instances in the Unity Inspector to change game data, including items, recipes, and quests.

## Conclusion

This project demonstrates a scalable, data-driven approach to building a forging mini-game in Unity. It emphasizes separation of concerns, modular design, and the use of game development best practices.