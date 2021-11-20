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
        public string Position { get; set; }

        public string Description { get; set; }

        [StringLength(128)]
        public string CompanyName { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [StringLength(128)]
        public string Designation { get; set; }

        [StringLength(128)]
        public string EngagementType { get; set; }

        [StringLength(128)]
        public string CompanyWebsite { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public bool? ActiveStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobApplication> JobApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
