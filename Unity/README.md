# Systems Grimoire - Unity C#

A curated spellbook of Unity C# systems, architectural patterns, and reusable game mechanics.

## Systems

### Observable (`Observable/`)
Reactive property and collection wrappers that fire events on value changes. Foundation for data binding in MVC.

- **`Observable<T>`** - Single value with `OnValueChanged`, `OnValueChangedTo`, `OnValueChangedFromTo` events
- **`ObservableList<T>`** - IList implementation with per-item and collection-level change events

### Command (`Command/`)
Decouples operation invokers from executors. Enables queuing, logging, and sequencing.

- **`Command`** - Abstract base with `Execute()`
- **`CommandQueue`** - FIFO queue for batched command processing

### Event System (`EventSystem/`)
ScriptableObject-based pub/sub for fully decoupled communication between systems. Wire events in the Inspector with zero code coupling.

- **`EventChannel<T..>`** - SO-based broadcaster (1-4 parameter variants)
- **`EventListener<T..>`** - Serializable subscriber with lifecycle management
- **`EventManager<T..>`** - MonoBehaviour that manages listener collections

### Singleton (`Singleton/`)
Thread-safe generic MonoBehaviour singleton with async initialization, DontDestroyOnLoad, and colored editor logging.

### MVC (`MVC/`)
Model-View-Controller framework powered by the Observable pattern.

- **`Model<T>`** - Generic data holder using `ObservableList<T>`
- **`Controller<TModel, TView>`** - Abstract mediator with `ConnectModel()` / `ConnectView()` / `Destroy()`
- **`Example/`** - Complete Shop implementation showing the full pattern in action

### UI Framework (`UIFramework/`)
Hierarchical UI management system.

- **`UserInterface`** - Abstract base with Show/Hide lifecycle and parent state management
- **`MainUserInterface`** - Full-screen UI panels (inventory, settings)
- **`SubUserInterface`** - Overlay/modal panels with priority tags
- **`UICarousel`** - Paginated horizontal scroll with lerp animation

## Architecture Overview

```
┌─────────────┐     events      ┌──────────────┐
│    Model     │ ──────────────> │  Controller  │
│ (Observable) │                 │  (Mediator)  │
└─────────────┘ <────────────── └──────────────┘
                   updates            │  ▲
                                      │  │
                              updates │  │ user input
                                      ▼  │
                                ┌──────────────┐
                                │     View     │
                                │ (MonoBehaviour)│
                                └──────────────┘
```

## Namespace

All systems use the `SystemsGrimoire` namespace. Adjust to fit your project.
