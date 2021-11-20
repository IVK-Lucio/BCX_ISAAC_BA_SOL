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
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(128)]
        public string OtherNames { get; set; }

        [StringLength(128)]
        public string Sex { get; set; }

        [StringLength(128)]
        public string MaritalStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(128)]
        public string ResumeUrl { get; set; }

        [StringLength(128)]
        public string EmailAddress { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual Job Job { get; set; }
    }
}
