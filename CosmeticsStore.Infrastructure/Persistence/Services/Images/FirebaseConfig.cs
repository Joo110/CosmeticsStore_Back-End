using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Persistence.Services.Images
{
    public class FirebaseConfig
    {
        public required string CredentialsJson { get; set; }
        public required string Bucket { get; set; }
    }
}
