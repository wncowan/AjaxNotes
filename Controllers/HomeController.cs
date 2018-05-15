using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AjaxNotes.DbConnection;

namespace AjaxNotes.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string queryString = "SELECT * FROM notes ORDER BY id DESC";
            List<Dictionary<string, object>> results = DbConnector.Query(queryString);
            ViewBag.notes = results;
            return View();
        }

        [HttpPost]
        [Route("/add_title")]
        public IActionResult AddNote(string title)
        {
            Console.WriteLine("entered add note");
            
            string queryString = $"INSERT INTO notes (title, created_at, updated_at) VALUES ('{title}', NOW(), NOW())";
            Console.WriteLine(queryString);
            DbConnector.Execute(queryString);
            return RedirectToAction("Index");
        }
            

        [HttpPost]
        [Route("/update_note")]
        public IActionResult UpdateNote(string content, string id)
        {
            Console.WriteLine("entered updateNote");
            string queryString = $"UPDATE notes SET content = '{content}',  updated_at = NOW() WHERE id={id};";
            DbConnector.Execute(queryString);
            return RedirectToAction("Index");
        }

        // [HttpPost]
        // [Route("/update_note/{id}")]
        // public IActionResult UpdateNote(string content, string id)
        // {
        //     Console.WriteLine("entered updateNote");
        //     string queryString = $"UPDATE notes SET content = '{content}',  updated_at = NOW() WHERE id={id};";
        //     DbConnector.Execute(queryString);
        //     return RedirectToAction("Index");
        // }

        [HttpPost]
        [Route("/delete_note/{id}")]
        public IActionResult DeleteNote(string id)
        {
            Console.WriteLine("entered DeleteNote");
            string queryString = $"DELETE FROM notes WHERE id={id}";
            DbConnector.Execute(queryString);
            return RedirectToAction("Index");
        }

    }
}