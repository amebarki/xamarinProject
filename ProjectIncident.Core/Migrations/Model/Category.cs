using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectIncident.Core.Model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Label { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Category Parent { get; set; }

        [InverseProperty(nameof(Parent))]
        public List<Category> Childs { get; set; }
        
        public Category()
        {
        }
    }
}
