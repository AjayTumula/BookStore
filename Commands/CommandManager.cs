using System.Collections.Generic;

namespace BookStore.Commands
{
    public interface ICommandManager
    {
        void Invoke(ICommand command);
        void Undo();
    }
    public class CommandManager : ICommandManager
    {
        private Stack<ICommand> commands = new Stack<ICommand>(); 

        public void Invoke(ICommand command)
        {
            if (command.CanExecute())
            {
                commands.Push(command);
                command.Execute();
            }
        }

        public void Undo()
        {
            var command = commands.Pop();
            command.Undo();
        }
    }
}
