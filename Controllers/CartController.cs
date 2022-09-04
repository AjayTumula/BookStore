using BookStore.Commands;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        ICommandManager _commandManager;
        private readonly BookStoreContext _context;
        private readonly Cart _cart;

        public CartController(BookStoreContext context, Cart cart,ICommandManager commandManager)
        {
            _context = context;
            _cart = cart;
            _commandManager = commandManager;
        }

        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;

            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.AddToCart(selectedBook, 1);
            }

            return RedirectToAction("Index", "Store");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var selectedBook = GetBookById(id);

            if (selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedBook = GetBookById(id);

             if (selectedBook != null)
             {
                 _cart.ReduceQuantity(selectedBook);
             }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseQuantity(int id)
        {
            var selectedBook = GetBookById(id);

            _commandManager.Invoke(new CartCommand(selectedBook, _cart));

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }
    }
}
