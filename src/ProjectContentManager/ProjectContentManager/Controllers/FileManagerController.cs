using ProjectContentManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectContentManager.Controllers
{
    public class FileManagerController : Controller
    {
        private ContentManagerModelContext db = new ContentManagerModelContext();
        public ActionResult Files(string id)
        {
            var categoryId = int.Parse(id);
            var category = db.Categories.Find(categoryId);
            var model = new FilesViewModel();
            model = new FilesViewModel
            {
                CategoryId = categoryId,
                CategoryName = category.Name,
                Files = (category.Root == null) ? db.Contents.Where(m => m.Category.Root == categoryId).ToList() :
                                                  db.Contents.Where(m => m.CategoryId == categoryId).ToList()
            };

            return View(model);
        }

        public ActionResult Create(string id)
        {
            var categoryId = int.Parse(id);
            var category = db.Categories.Find(categoryId);


            var model = new ContentViewModel
            {
                ContentTypes = db.ContentTypes.Select(m =>
                        new SelectListItem { Text = m.Name, Value = m.ContentTypeId.ToString() }).ToList(),
                CategoryId = categoryId,
                Category = category,
                CategoryName = category.Name,
                Categories = (category.Root == null) ? db.Categories.Where(m => m.Root == categoryId).Select(m =>
                        new SelectListItem { Text = m.Name, Value = m.CategoryId.ToString() }).ToList() : null,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,SelectedCategoryId,ContentTypeId,Path,Comments,Month,Year")] ContentViewModel content)
        {
            if (ModelState.IsValid)
            {
                var contentModel = ConvertToModel(content);

                UpdateFileContent(contentModel);

                db.Contents.Add(contentModel);
                db.SaveChanges();
                return RedirectToAction("Files", new { id = content.CategoryId });
            }

            return View(content);
        }


        public FileResult DownloadFile(string id)
        {
            var contentId = int.Parse(id);
            var content = db.Contents.Where(m => m.ContentId == contentId).FirstOrDefault();
            var bytes = content.FileContent;
            var filename = content.Path;
            var ext = System.IO.Path.GetExtension(filename);
            return File(bytes, "application/" + ext, filename);
        }

        [HttpGet]
        [Route("FileManager/edit/{id}/{contentId}")]
        public ActionResult Edit(int? id, int? contentId)
        {
            var categoryId = id;
            if (id == null || contentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var content = db.Contents.Find(contentId);
            if (content == null)
            {
                return HttpNotFound();
            }

            var category = db.Categories.Find(id);
            if (category.Root == null)
            {
                category = content.Category;
                categoryId = categoryId.Value;
            }

            var contentViewModel = ConvertToViewModel(categoryId, content);

            return View(contentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("FileManager/edit/{id}/{contentId}")]
        public ActionResult Edit([Bind(Include = "CategoryId,ContentId,ContentTypeId,Path,Comments,Month,Year")] ContentViewModel content)
        {
            if (ModelState.IsValid)
            {
                var contentModel = ConvertToModel(content);

                UpdateFileContent(contentModel);

                db.Entry(contentModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Files", new { id = content.CategoryId });
            }

            return View(content);
        }

        [HttpGet]
        [Route("FileManager/delete/{id}/{contentId}")]
        public ActionResult Delete(int? id, int? contentId)
        {
            var categoryId = id;
            if (id == null || contentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var content = db.Contents.Find(contentId);
            if (content == null)
            {
                return HttpNotFound();
            }

            var contentViewModel = ConvertToViewModel(categoryId, content);

            return View(contentViewModel);
        }

        [HttpPost]
        [Route("FileManager/delete/{id}/{contentId}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int contentId)
        {
            var content = db.Contents.Find(contentId);
            var categoryId = content.CategoryId;
            db.Contents.Remove(content);
            db.SaveChanges();
            return RedirectToAction("Files", new { id = categoryId });
        }


        private ContentViewModel ConvertToViewModel(int? categoryId, Models.Content content)
        {
            var contentViewModel = new ContentViewModel
            {
                CategoryName = content.Category.Name,
                ContentId = content.ContentId,
                ContentTypeId = content.ContentTypeId,
                Path = content.Path,
                UploadDate = content.UploadDate,
                Comments = content.Comments,
                Year = content.Year,
                Month = content.Month,
                ContentTypes = db.ContentTypes.Select(m =>
                       new SelectListItem { Text = m.Name, Value = m.ContentTypeId.ToString() }).ToList(),
                CategoryId = categoryId
            };
            return contentViewModel;
        }

        private void UpdateFileContent(Models.Content contentModel)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                    }
                    contentModel.FileContent = fileData;
                    contentModel.Path = file.FileName;
                }
            }
        }

        private Models.Content ConvertToModel(ContentViewModel content)
        {
            Content contentModel;
            if (content.ContentId != 0)
            {
                contentModel = db.Contents.Find(content.ContentId);              
            }
            else
            {
                contentModel = new Content
                {
                    ContentId = content.ContentId == 0 ? GetNewContentId() : content.ContentId,
                    CategoryId = content.SelectedCategoryId != null ? content.SelectedCategoryId : content.CategoryId,                  
                };
            }

            contentModel.Path = content.Path;
            contentModel.CategoryId = content.CategoryId;
            contentModel.Comments = content.Comments;
            contentModel.Year = content.Year;
            contentModel.Month = content.Month;
            contentModel.ContentTypeId = content.ContentTypeId;
            contentModel.UploadDate = DateTime.Now;

            return contentModel;
        }

        private int GetNewContentId()
        {
            var obj = db.Contents.OrderByDescending(m => m.ContentId).FirstOrDefault();
            var id = 0;
            if (obj == null)
            {
                id = 1;
            }
            else
            {
                id = obj.ContentId + 1;
            }
            return id;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}