using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    public class NoteController : Controller
    {
        private readonly NoteContext _context;

        public NoteController(NoteContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách note
        public IActionResult Index()
        {
            var notes = _context.Notes.ToList(); // Lấy danh sách notes từ CSDL
            return View(notes);
        }

        // Thêm note mới
        [HttpPost]
        public IActionResult Create(Note note)
        {
            note.CreatedDate = DateTime.Now;
            _context.Notes.Add(note);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Sửa note (GET)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // Sửa note (POST)
        [HttpPost]
        public IActionResult Edit(Note note)
        {
            var existingNote = _context.Notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote != null)
            {
                existingNote.Title = note.Title;
                existingNote.Content = note.Content;
                _context.SaveChanges(); // Lưu thay đổi vào CSDL
            }
            return RedirectToAction("Index");
        }

        // Xóa note
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges(); // Lưu thay đổi vào CSDL
            }
            return RedirectToAction("Index");
        }
    }
}
