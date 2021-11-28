namespace BCX_ISAAC_BA_SOL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Job")]
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            JobApplications = new HashSet<JobApplication>();
            Questions = new HashSet<Question>();
        }

        public string Id { get; set; }

        [StringLength(128)]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Amount")]
        public string Amount { get; set; }

        [Display(Name = "Period")]
        public string Period { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [StringLength(128)]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(128)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [StringLength(128)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [StringLength(128)]
        [Display(Name = "Engagement Type")]
        public string EngagementType { get; set; }

        [StringLength(128)]
        [Display(Name = "Company Website")]
        public string CompanyWebsite { get; set; }

        [StringLength(128)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Question Time")]
        public int QuestionTime { get; set; }

        [Display(Name = "Active Status")]
        public bool? ActiveStatus { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Posted")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DatePosted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobApplication> JobApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
