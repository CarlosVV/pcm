using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectContentManager.Models
{
    public class ContentViewModel
    {
        public int ContentId { get; set; }

        [DisplayName("Categoría")]
        public int? CategoryId { get; set; }

        [DisplayName("Categoría")]
        public int? SelectedCategoryId { get; set; }

        [DisplayName("Categoría")]
        public string CategoryName { get; set; }

        [DisplayName("Tipo de Archivo")]
        public int? ContentTypeId { get; set; }

        [DisplayName("Ruta")]
        public string Path { get; set; }

        public byte[] FileContent { get; set; }

        [DisplayName("Fecha de Subida")]
        public DateTime? UploadDate { get; set; }

        [DisplayName("Año")]
        public int? Year { get; set; }

        [DisplayName("Mes")]
        public int? Month { get; set; }

        [DisplayName("Comentarios")]
        public string Comments { get; set; }

        [DisplayName("Categoría")]
        public Category Category { get; set; }

        [DisplayName("Tipo de archivo")]
        public ContentType ContentType { get; set; }

        [DisplayName("Tipos de Archivo")]
        public List<SelectListItem> ContentTypes { get; set; }

        [DisplayName("Categoría")]
        public List<SelectListItem> Categories { get; set; }
    }
}