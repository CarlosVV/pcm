namespace ProjectContentManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContentId { get; set; }

        public int? CategoryId { get; set; }

        public int? ContentTypeId { get; set; }

        public string Path { get; set; }

        public byte[] FileContent { get; set; }

        public DateTime? UploadDate { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }

        [StringLength(4000)]
        public string Comments { get; set; }

        public virtual Category Category { get; set; }

        public virtual ContentType ContentType { get; set; }
    }
}
