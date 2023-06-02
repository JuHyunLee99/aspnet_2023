using Microsoft.AspNetCore.Mvc;
using Portfolio.Data;
using Portfolio.Models;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        public readonly ApplicationDbContext _db;

        // 파일업로드 웹환경 객체
        private readonly IWebHostEnvironment _environment;

        public PortfolioController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _db.Portfolios.ToList(); // SELECT *
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TempPortfolioModel temp)
        {   // PortfolioModel(x) --> TempPortfolioModel
            if (ModelState.IsValid)
            {
                // 파일 업로드되면 새로운 이밎 파일명을 받음
                string upFileName = UploadImageFile(temp);

                // TempPortfolioModel -> PortfolioModel 변경
                var portfoli = new PortfolioModel()
                {
                    Division = temp.Division,
                    Title = temp.Title,
                    Description = temp.Description,
                    Url = temp.Url,
                    FileName = upFileName   // 핵심
                };

                _db.Portfolios.Add(portfoli);
                _db.SaveChanges();

                TempData["succeed"] = "포토폴리오 저장완료";
                return RedirectToAction("Index", "Portfolio");
            }
            return View(temp);
        }

        private string UploadImageFile(TempPortfolioModel temp)
        {
            var resultFileName = "";
            try
            {
                if (temp.PortfolioImage != null)
                {
                    // wwwroot 밑에 uploads라는 폴더 존재
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    // 중복된 이미지파일명 제거
                    resultFileName = Guid.NewGuid() + "_" + temp.PortfolioImage.FileName;
                    string filePath = Path.Combine(uploadFolder, resultFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        temp.PortfolioImage.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }

            return resultFileName;
        }
    }
}
