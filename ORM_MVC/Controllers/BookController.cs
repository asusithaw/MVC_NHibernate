using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using ORM_MVC.Models;
using ISession = NHibernate.ISession;

namespace ORM_MVC.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {           
            IList<Book> books;

            using (ISession session = NHibernateSession.OpenSession())  
            {
                books = session.Query<Book>().ToList(); 
            }

            return View(books);
        }

        // GET: Book/Details/1
        public ActionResult Details(int id)
        {
            Book book = new Book();
            using (ISession session = NHibernateSession.OpenSession())
            {
                book = session.Query<Book>().Where(b => b.Id == id).FirstOrDefault();
            }

            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Book book = new Book();    
                book.Title = collection["Title"].ToString();
                book.Genre = collection["Genre"].ToString();
                book.Author = collection["Author"].ToString();

                
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())   
                    {
                        session.Save(book); 
                        transaction.Commit();   
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = new Book();
            using (ISession session = NHibernateSession.OpenSession())
            {
                book = session.Query<Book>().Where(b => b.Id == id).FirstOrDefault();
            }
                       
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Book book = new Book();
                book.Id = id;
                book.Title = collection["Title"].ToString();
                book.Genre = collection["Genre"].ToString();
                book.Author = collection["Author"].ToString();
               
                using (ISession session = NHibernateSession.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(book);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            // Delete the book
            Book book = new Book();
            using (ISession session = NHibernateSession.OpenSession())
            {
                book = session.Query<Book>().Where(b => b.Id == id).FirstOrDefault();
            }
            
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {                
                using (ISession session = NHibernateSession.OpenSession())
                {
                    Book book = session.Get<Book>(id);

                    using (ITransaction trans = session.BeginTransaction())
                    {
                        session.Delete(book);
                        trans.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}
