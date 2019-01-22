using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ghost_Db.Models
{
    public class GhostType
    {
        public int GhostTypeID { get; set; }

        // Ghost name
        [Required]
        public string Name { get; set; }

        // Specific Features
        [MaxLength(100)]
        public string Features { get; set; }

        // Short description of features and ghost
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
