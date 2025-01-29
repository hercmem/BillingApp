using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BillingApp.Models.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace BillingApp.Models
{
    [ModelMetadataType(typeof(AppUserMetadata))]
    public partial class AppUser
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }

    }

    [ModelMetadataType(typeof(ClientsMetadata))]
    public partial class Client
    {

    }

    [ModelMetadataType(typeof(BillsMetadata))]
    public partial class Bill
    {

    }

    [ModelMetadataType(typeof(PhonesMetadata))]
    public partial class Phone
    {

    }

    [ModelMetadataType(typeof(PhoneProgramsMetadata))]
    public partial class PhoneProgram
    {

    }

}
