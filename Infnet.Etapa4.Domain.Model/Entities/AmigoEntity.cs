using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infnet.Etapa4.Domain.Model.Entities
{
    [Table("Amigo")]
    public class AmigoEntity
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public bool Parente { get; set; }

        [DisplayName("Foto")]
        public string ImageUri { get; set; }


    }
}
