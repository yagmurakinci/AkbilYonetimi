using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkbilYonetimiEntityLayer.Entities
{
    class Kullanici:IIDProperty, IKayitTarihiProperty
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public DateTime DogumTarihi { get; set; }
        public DateTime KayitTarihi { get ; set ; }
    }
}
