namespace SystemsGrimoire {
    /// <summary>
    /// Abstract base for the Command pattern.
    /// Decouples the invoker of an operation from the object that performs it.
    /// Enables queuing, logging, undo/redo, and sequencing of operations.
    ///
    /// Usage:
    ///     public class DamageCommand : Command {
    ///         private readonly IDamageable m_Target;
    ///         private readonly int m_Amount;
    ///
    ///         public DamageCommand(IDamageable target, int amount) {
    ///             m_Target = target;
    ///             m_Amount = amount;
    ///         }
    ///
    ///         public override void Execute() => m_Target.TakeDamage(m_Amount);
    ///     }
    /// </summary>
    public abstract class Command {
        public abstract void Execute();
    }
}
