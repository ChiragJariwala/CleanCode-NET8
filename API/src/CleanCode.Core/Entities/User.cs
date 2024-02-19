using CleanCode.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace CleanCode.Core.Entities
{
    /// <summary>
    /// It is used for evry user has uniqe identity. This may be use QRCode or ID card or Other Identity.
    /// </summary>
    public partial class User : BaseEntity
    {
        //public User()
        //{
        //    BillBatchAmountGivenByNavigations = new HashSet<BillBatch>();
        //    BillBatchAmountGivenToNavigations = new HashSet<BillBatch>();
        //    BillBatchCreatedByNavigations = new HashSet<BillBatch>();
        //    BillBatchModifiedByNavigations = new HashSet<BillBatch>();
        //    BillBatchVerifiedByNavigations = new HashSet<BillBatch>();
        //    BillEntryBillSpentByNavigations = new HashSet<BillEntry>();
        //    BillEntryCreatedByNavigations = new HashSet<BillEntry>();
        //    BillEntryModifiedByNavigations = new HashSet<BillEntry>();
        //    BillEntrySignedBySantNavigations = new HashSet<BillEntry>();
        //    VehicleAllottedToDrivers = new HashSet<VehicleAllottedToDriver>();
        //}

        public int UserId { get; set; }
        public string UserCode { get; set; }
        public int VolunteerId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public int RoleId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //public virtual Role Role { get; set; }
        //public virtual Volunteer Volunteer { get; set; }
        //public virtual ICollection<BillBatch> BillBatchAmountGivenByNavigations { get; set; }
        //public virtual ICollection<BillBatch> BillBatchAmountGivenToNavigations { get; set; }
        //public virtual ICollection<BillBatch> BillBatchCreatedByNavigations { get; set; }
        //public virtual ICollection<BillBatch> BillBatchModifiedByNavigations { get; set; }
        //public virtual ICollection<BillBatch> BillBatchVerifiedByNavigations { get; set; }
        //public virtual ICollection<BillEntry> BillEntryBillSpentByNavigations { get; set; }
        //public virtual ICollection<BillEntry> BillEntryCreatedByNavigations { get; set; }
        //public virtual ICollection<BillEntry> BillEntryModifiedByNavigations { get; set; }
        //public virtual ICollection<BillEntry> BillEntrySignedBySantNavigations { get; set; }
        //public virtual ICollection<VehicleAllottedToDriver> VehicleAllottedToDrivers { get; set; }
    }
}
