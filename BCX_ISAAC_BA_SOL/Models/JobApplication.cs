namespace BCX_ISAAC_BA_SOL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobApplication")]
    public partial class JobApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobApplication()
        {
            Answers = new HashSet<Answer>();
        }

        public string Id { get; set; }

        [StringLength(128)]
        public string JobId { get; set; }

        [StringLength(128)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(128)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(128)]
        [Display(Name = "Other Names")]
        public string OtherNames { get; set; }

        [StringLength(128)]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [StringLength(128)]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(128)]
        [Display(Name = "Resume Url")]
        public string ResumeUrl { get; set; }

        [StringLength(128)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Status")]
        public bool? Status { get; set; }

        [Display(Name = "Time Answered")]
        public TimeSpan AnsweredTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Job Job { get; set; }
    }
}
