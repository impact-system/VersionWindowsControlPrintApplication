using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrackWiseModel.Models
{
    public class TrackWise
    {
        [Key]
        public int HID { get; set; }
        public int SId { get; set; }
        public int Tid { get; set; }
        public int PRNO { get; set; }
        public string Header { get; set; }
        public string PdfDocument { get; set; }
        public string StateName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}