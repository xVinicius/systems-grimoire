using System.Collections.Generic;

namespace SystemsGrimoire {
    /// <summary>
    /// A simple FIFO queue for Command execution.
    /// Commands are enqueued and processed one at a time.
    ///
    /// Usage:
    ///     var queue = new CommandQueue();
    ///     queue.Enqueue(new DamageCommand(enemy, 10));
    ///     queue.Enqueue(new HealCommand(player, 5));
    ///     queue.ProcessNext(); // executes DamageCommand
    ///     queue.ProcessAll();  // executes remaining
    /// </summary>
    public class CommandQueue {
        private readonly Queue<Command> m_Commands = new();

        public int Count => m_Commands.Count;
        public bool HasCommands => m_Commands.Count > 0;

        public void Enqueue(Command command) {
            m_Commands.Enqueue(command);
        }

        public void ProcessNext() {
            if (m_Commands.Count > 0) {
                m_Commands.Dequeue().Execute();
            }
        }

        public void ProcessAll() {
            while (m_Commands.Count > 0) {
                m_Commands.Dequeue().Execute();
            }
        }

        public void Clear() {
            m_Commands.Clear();
        }
    }
}
