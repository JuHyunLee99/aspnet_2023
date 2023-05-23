using aspnet02_boardapp.Data;
using aspnet02_boardapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace aspnet02_boardapp.Controllers
{
    // https://localhost:7800/Board/Index
    public class BoardController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BoardController(ApplicationDbContext db)
        {
            _db = db;   // 알아서 DB가 연결
        }

        public IActionResult Index()    // 게시판 최초 화면 리스트
        {
            IEnumerable<Board> objBoardList = _db.Boards.ToList();  //SELECT쿼리
            return View(objBoardList);
        }


        // https://localhost:7139/Board/Create
        //GetMethod로 화면을 URL로 부를때 액션
        [HttpGet]
        public IActionResult Create()  // 게시판 글쓰기
        { 
            return View();
        }

        // Submit이 발생해서 내부로 데이터를 전달하는 액션
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Board board)
        {
            board.PostDate = DateTime.Now;  // 현재 저장하는 일시를 할당
            _db.Boards.Add(board);    // INSERT
            _db.SaveChanges();    //COMMIT

            return RedirectToAction("Index", "Board");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id ==0) 
            {
                return NotFound();  // Error.cshtml이 표시
            }

            var board = _db.Boards.Find(Id);    // SELECT * FROM board WHERE Id = @id

            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Board board)
        {
            board.PostDate = DateTime.Now;  // 현재 저장하는 일시를 할당
            _db.Boards.Update(board);   // UPDATE query실행
            _db.SaveChanges();          // COMMIT

            return RedirectToAction("Index", "Board");
        }

        public IActionResult Delete(int? Id) 
        {
            if (Id == null || Id == 0)
            {
                return NotFound();  // Error.cshtml이 표시
            }

            var board = _db.Boards.Find(Id);    // SELECT * FROM board WHERE Id = @id

            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        [HttpPost]
        public IActionResult DeletePost(int? Id)
        {
            var board = _db.Boards.Find(Id);

            if (board == null)
            {
                return NotFound();
            }

            _db.Boards.Remove(board);   // Delete query실행
            _db.SaveChanges();          // COMMIT

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();  // Error.cshtml이 표시
            }

            var board = _db.Boards.Find(Id);    // SELECT * FROM board WHERE Id = @id

            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }
    }
}
