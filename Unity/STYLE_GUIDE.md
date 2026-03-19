<mark style="background-color: #c6edfb">These are guidelines to keep a Unity project tidy and easy to work on.</mark><!-- adf:backgroundColor attrs='{"color":"#c6edfb"}' -->

## Version Control

Commit one change at a time. I.e. don’t make changes to several different “things” (make some graphical edits to the scene as well as chaning scripts) and create a single commit out of it.<!-- adf:mark type="annotation" attrs='{"annotationType":"inlineComment","id":"0535e045-97c4-4379-b626-b970330785bb"}' -->

When trying out things, do the changes in a temporary scene. When you are sure what you want to commit, make the correct changes in the main scene. This is to avoid commiting changes that are not related to what you actually wanted to change. Alternatively, make experimental changes in the scene, then revert all of them before doing the final changes.

Remove empty directories to make sure Unity delets its corrending meta file. Materials directories tend to be empty. Remove those.

**<mark style="background-color: #fdd0ec">Don’t commit files that you didn’t change, this can happen whe unity enforces changes. If this keeps happening, make a separate commit with only these “Unity enforced chages” to avoid getting them in the future.</mark><!-- adf:backgroundColor attrs='{"color":"#fdd0ec"}' -->**

## Assets

Put all assets under a folder called Project under Assets. Use subfolders as necessary. <!-- adf:mark type="annotation" attrs='{"annotationType":"inlineComment","id":"f05248a9-ed7f-4e86-8236-a95adb019532"}' -->

“Assets/Standard Assets” is only for third party assets. Do not put your own assets there. Do not edit assets here.

Put “art”, i.e. textures, materials, meshes, sprites, etc in a folder called Art bellow Assets/Project. Use subfolders to split assets into logical groups.

Put script assets into other folders depending on what area of the game they are about, e.g. “Enemies”, “Environment”, etc.

Remove assets that are no longer in use.

When updating an asset, just replace the old asset. Do not add the new asset with a new name and update all references to the old one with the new one.

Use PascalCase for naming assets (every word starts with a capital letter). Do not add undersocres or spaces between words.

## Whitespace

Put spaces around operators like =, ==, <, >, +, -, etc. Put spaces after commas. Do not put space before or after parenthesis in function calls or if/while/for statements. Put whitespaces around {}.

Indent with spaces.

Use 4 spaces for indentation.

Do not keep spaces at the end of lines.

End text files with a single line break. Do not have blank lines at the end of the file.

Use Unix line endings (LF only), not Windows (CR+LF)

## Braces

Put the opening brace on the same line as its opening statement (e.g. if or while)

This rule also applies to **for, while** statements. Put **else** on a separate line.

## Explicit Access Modifier

Class fields are private by default, but add the private keyword anyway.

## Naming Conventions

Use American English for all names. Use names that clearly explain whatever it names, without it getting overly long. For variables of small scope, single letter variables are allowed if it makes the code clearer. Anything that makes the code clearer is allowed 😉

private member/readonly member: `m_VariableName` 

private static member: `s_VariableName` 

private constant: `k_ConstantName` 

private unity serialized field: `m_VariableName` 

public unity serialized variable: 

```
[field: SerializedField] public VarType VariableName { get; private set; }
```

### Expression-Bodied Members

Use expression-bodied members for simple one-line methods and properties:

```csharp
// Good - concise and readable
public float GetSpeed() => velocity.magnitude;
public bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, 0.1f);

// Properties
public Vector3 Position => transform.position;
public bool IsAlive => health > 0;
```

### Auto-Property Initializers

Initialize properties directly in their declaration:

```csharp
// Good
public int MaxHealth { get; private set; } = 100;
public float Speed { get; set; } = 5f;
private List<Enemy> m_Enemies { get; set; } = new List<Enemy>();

// Avoid old pattern
private int m_MaxHealth = 100;
public int MaxHealth { get { return maxHealth; } }
```

### Null-Conditional Operators

Use `?.` and `??` for safer null handling:

```csharp
// Good - safe navigation
int? count = player?.Inventory?.Items?.Count;
string name = enemy?.name ?? "Unknown";

// Good - safe event invocation
OnDeath?.Invoke();

// Instead of
if (OnDeath != null) {
    OnDeath.Invoke();
}
```

### String Interpolation

Use string interpolation instead of string.Format or concatenation:

```csharp
// Good
Debug.Log($"Player {playerName} has {health} health remaining");
string message = $"Score: {score:F2}";

// Avoid
Debug.Log("Player " + playerName + " has " + health + " health remaining");
Debug.Log(string.Format("Score: {0:F2}", score));
```

### nameof Operator

Use `nameof()` for property names and avoiding magic strings:

```csharp
// Good - refactor-safe
Debug.LogError($"{nameof(Player)} component not found");
animator.SetBool(nameof(isGrounded), true);

// Avoid
Debug.LogError("Player component not found");
animator.SetBool("isGrounded", true);
```

### Read-Only Auto-Properties

Use read-only auto-properties for immutable data:

```csharp
// Good - can only be set in constructor or initializer
public Rigidbody Rigidbody { get; }
public string PlayerId { get; }

// In constructor
public Player(string id) {
    PlayerId = id;
    Rigidbody = GetComponent<Rigidbody>();
}
```

### Pattern Matching

Use pattern matching for type checks and casting:

```csharp
// Good
if (other is Enemy enemy && enemy.IsAlive) {
    TakeDamage(enemy.AttackPower);
}

// Avoid
Enemy enemy = other as Enemy;
if (enemy != null && enemy.IsAlive) {
    TakeDamage(enemy.AttackPower);
}
```

### Discards

Use `_` to discard unused values:

```csharp
// Good
if (Physics.Raycast(origin, direction, out _, maxDistance)) {
    // Don't need the hit info
}

// Tuple deconstruction
var (x, _, z) = transform.position; // Only need x and z
```

## Best Practices

### Don't Get Same Value More Than Once

Instead of getting the same value more than once, store it in a local variable:

```csharp
// Good
var playerTransform = player.transform;
playerTransform.position = newPosition;
playerTransform.rotation = newRotation;

// Avoid
player.transform.position = newPosition;
player.transform.rotation = newRotation;
```

### Prefer Readonly Fields

Use `readonly` for fields that shouldn't change after initialization:

```csharp
private readonly Rigidbody rb;
private readonly AudioSource audioSource;

private void Awake() {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
}
```

## Comments

First make sure the naming and code structure is as good as it can be. Comment whatever is needed to make the code clear, after you have made sure that the code is as readable as you can without them.

Normally it's more valuable for comments to explain the reasoning behind the code, not how it works.

Assume the person reading the code is at least an average programmer.

Do not keep unnecessary comments.

Do not keep commented out code. Remove it instead. A single line or couple of lines can be OK if it's useful for understanding the possibilities of the code. Log statements are sometimes useful to comment out instead of deleting.

## Errors and Warnings

Treat warnings as errors. Do not keep code that generates compilation warnings. Fix it instead.

For example, if you get a warning about an unused variable, remove that variable! Note that unfortunately third-party code often gives warnings, which clutters the console. Do not fix these warnings in the third-party code. It's better to keep the original code.

## Third Party Code

When adding a Unity plugin, do not edit its code. Otherwise it's difficult to upgrade the plugin later. If you need to change the code, create a copy, add a comment from where it's copied and edit it.

When adding a plugin, make a note of its version number in the commit message.

When copying code from the web, add a comment about where you took it from. This makes it possible to find reasoning behind the code.

## Fail Fast

If the game's state is not what the code expects, an error message should be shown as close as possible to the source of the problem. Fail fast, don't let invalid state linger.

## Asserts

Use asserts to check that what the code expects is true. See Using Assertions in Unity. For example, if you know that a value has to be greater than zero, and it would be a programming error if it wasn't, you can write:

```csharp
Assert.IsTrue(value > 0, "Value should be above zero");
```

Use AreEqual to check for equality. Note that the first argument is the expected value, the second is the actual value:

```csharp
Assert.AreEqual(5, jumpCount, "Wrong amount of jumps");
```

Note that asserts are removed from release builds, so you cannot depend on their side effects. Only use asserts to check for things that are programming errors. If an error can occur because of invalid user input, you should use normal error checks.

## Casts

Differentiate between the "as" operator and casting.

```csharp
x as Foo // sets x to null if cast fails
```

vs.

```csharp
(Foo) x // throws InvalidCastException if cast fails
```

**RULE:** Don't use the "as" operator to cast an object that should be of a specific type. Example:

```csharp
// DON'T - sets obj to null if Instantiate doesn't return what you expect
GameObject obj = Object.Instantiate(...) as GameObject;

// DO - fails fast if wrong type
GameObject obj = (GameObject)Object.Instantiate(...);

// BETTER - use pattern matching
if (Object.Instantiate(...) is GameObject obj) {
    // Use obj safely
}
```

Of course you should still use the "as" operator if it's expected that the object sometimes has another type.

## Iterating

Do not use "foreach" constructs when iterating over an array or List in performance-critical code.

**NOTE:** Modern Unity (2021+) has significantly improved foreach performance. In most gameplay code, foreach is now acceptable. Reserve manual for-loops for performance-critical sections only (Update/FixedUpdate inner loops).

```csharp
// Acceptable in modern Unity for most code
foreach (var enemy in enemies) {
    enemy.Update();
}

// Still preferred in hot paths
for (int i = 0; i < enemies.Count; i++) {
    enemies[i].Update();
}
```

Unfortunately, there seems to be no way to iterate over a Dictionary without allocating an iterator. If you iterate over a Dictionary often, you probably should consider another data structure.

### Span<T> or ReadOnlySpan<T> for Performance-Critical Iteration (C# 7.2+)

For extremely performance-critical code, consider using Span<T>:

```csharp
// Ultra-performance when working with native arrays
NativeArray<float> values = new NativeArray<float>(100, Allocator.Temp);
Span<float> span = values;
for (int i = 0; i < span.Length; i++) {
    span[i] = CalculateValue(i);
}
```
