using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength (maximumLength:100)]
        [Required]
        public string HeaderLogo { get; set; }
        [StringLength(maximumLength: 100)]
        [Required]
        public string FooterLogo { get; set; }
        [StringLength(maximumLength: 100)]
        public string FacebookIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string TwitterIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string InstagramIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string DribbleIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string LocationIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string PhoneIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string MailIcon { get; set; }
        [StringLength(maximumLength:250)]
        public string FooterDesc { get; set; }
        [StringLength(maximumLength: 100)]
        public string Adress { get; set; }
        [StringLength(maximumLength: 50)]
        public string ContactMail { get; set; }
        [StringLength(maximumLength: 50)]
        public string SupportMail { get; set; }
        [StringLength(maximumLength: 150)]
        public string AboutImage { get; set; }
        [StringLength(maximumLength: 50)]
        public string AboutTitle { get; set; }
        [StringLength(maximumLength: 250)]
        public string AboutDesc { get; set; }
        [StringLength(maximumLength: 150)]
        public string AboutSubDesc { get; set; }
        [StringLength(maximumLength: 30)]
        public string AboutUrlText { get; set; }
        [StringLength(maximumLength: 100)]
        public string AboutUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string CopyRight { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string InstagramUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string DribbleUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string TwitterUrl { get; set; }


    }
}
