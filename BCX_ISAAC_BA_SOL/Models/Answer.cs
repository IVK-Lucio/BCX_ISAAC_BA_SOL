namespace BCX_ISAAC_BA_SOL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        public string Id { get; set; }

        [StringLength(128)]
        public string QuestionId { get; set; }


        [Display(Name = "Answer Text")]
        public string AText { get; set; }

        [StringLength(128)]
        public string JobApplicationId { get; set; }


        public virtual JobApplication JobApplication { get; set; }

        public virtual Question Question { get; set; }
    }
}
