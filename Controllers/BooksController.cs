using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mm.Models;

namespace mm.Controllers;

public class BooksController : Controller
{
    private readonly BookDAL _dal;

    public BooksController(IConfiguration config)
    {
        _dal = new BookDAL(config);
    }

    public IActionResult Index()
    {
        var books = _dal.GetAll();
        return View(books);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        _dal.Insert(book);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var book = _dal.GetById(id);
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(Book book)
    {
        _dal.Update(book);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var book = _dal.GetById(id);
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _dal.Delete(id);
        return RedirectToAction("Index");
    }
}
