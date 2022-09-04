using BookStore.Models;

namespace BookStore.Commands
{
    public class CartCommand : ICommand
    {
        private readonly Book _book;
        private readonly Cart _cart1;

        public CartCommand(Book book, Cart cart1)
        {
            _book = book;
            this._cart1 = cart1;
        }

        bool ICommand.CanExecute()
        {
            return _cart1.GetCartItem(_book) != null;
        }

        void ICommand.Execute()
        {
            _cart1.IncreaseQuantity(_book);
        }

        void ICommand.Undo()
        {
           _cart1.ReduceQuantity(_book);
        }
    }
}
