using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BooksServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksServices.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext context) => _context = context;


        // GET: api/values
        [HttpGet]
        public IEnumerable<Book> Get() => _context.Books.ToList();

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(int id) => _context.Books.SingleOrDefault(b => b.Id == id);


        // POST api/values
        [HttpPost]
        public void Post([FromBody]Book value)
        {
            _context.Books.Add(value);
            _context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Book value)
        {
            _context.Books.Update(value);
            _context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context?.Dispose();
        }
    }
}
