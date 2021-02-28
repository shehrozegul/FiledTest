using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FiledTest.Models.Models
{
    public class PaymentState
    {
        [Required]
        [Key]
        public long PaymentStateId { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public long PaymentId { get; set; }
        [ForeignKey(nameof(PaymentId))]
        public Payment Payment { get; set; }
    }
}
